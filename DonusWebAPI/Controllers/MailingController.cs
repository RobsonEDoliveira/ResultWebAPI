using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Donus.Application.DataContract.Request.Mailing;
using Donus.Application.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Donus.Api.Controllers
{
    [Route("api/mailing")]
    [ApiController]
    [Authorize]
    public class MailingController : ControllerBase
    {
        private readonly IMailingApplication _mailingApplication;

        public MailingController(IMailingApplication mailingApplication)
        {
            _mailingApplication = mailingApplication;
        }

        // LISTAR MAILING POR FILTRO
        // GET: api/<ClientController>
        /// <summary>
        /// Get all clients 
        /// </summary>
        /// <param name="mailingid"></param>
        /// <param name="nome"></param>
        /// <returns></returns>
        [HttpGet] 
        public async Task<ActionResult> Get([FromQuery] string mailingId, [FromQuery] string nome)
        {
            var response = await _mailingApplication.ListarMailingPorFiltro(mailingId, nome);

            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }

        // RETORNAR MAILING POR ID
        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var response = await _mailingApplication.RetornarMailingID(id);

            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }

        // CRIAR NOVO MAILING
        // POST api/<ClientController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateMailingRequest request)
        {
            var response = await _mailingApplication.CriarMailing(request);

            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }

        // ATUALIZAR MAILING
        // PUT api/<ClientController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateMailingRequest request)
        {
            var response = await _mailingApplication.AtualizarMailing(request);

            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }

        // DELETAR MAILING
        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var response = await _mailingApplication.DeletarMailing(id);

            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }
    }
}
