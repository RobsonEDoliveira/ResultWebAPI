namespace Donus.Domain.Models
{
    // MODEL USUÁRIO
    public class UsuarioModel : EntityBase
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
    }
}
