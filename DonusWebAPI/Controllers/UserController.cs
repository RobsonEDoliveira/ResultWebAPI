using Donus.Application.DataContract.Request.Usuario;
using Donus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Donus.Api.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioApplication _UsuarioApplication;

        public UsuarioController(IUsuarioApplication UsuarioApplication)
        {
            _UsuarioApplication = UsuarioApplication;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// /// <param name="userId">The user identifier.</param>
        /// <param name="name">The pagination filter.</param>
        /// <returns>The paginated HTTP response with list of users.</returns>
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] string userId, [FromQuery] string name)
        {
            var response = await _UsuarioApplication.ListarUsuarioPorFILTRO(userId, name);

            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        /// <summary>
        /// Creates new user. Only users with admin role can access this endpoint.
        /// </summary>
        /// <param name="request">The request data.</param>
        /// <returns>The HTTP response indicating if this request was successful or not.</returns>
        // POST api/<UsuarioController>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] CreateUsuarioRequest request)
        {
            var response = await _UsuarioApplication.CriarUsuario(request);

            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <param name="request">The request data.</param>
        /// <returns>The result containing user info and authorization token, if authentication was successful.
        /// </returns>
        // DELETE api/<UsuarioController>/5
        [HttpPost("auth")]
        [AllowAnonymous]
        public async Task<ActionResult> Auth([FromBody] AuthRequest request)
        {
            var response = await _UsuarioApplication.AuthAsync(request);

            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }
    }
} 
