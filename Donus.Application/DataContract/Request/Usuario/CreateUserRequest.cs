namespace Donus.Application.DataContract.Request.Usuario
{
    // REQUEST
    public class CreateUsuarioRequest
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
