namespace Donus.Domain.Models
{
    // MODEL MAILING
    public class MailingModel : EntityBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Logradouro { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Genero { get; set; }
    }
}
