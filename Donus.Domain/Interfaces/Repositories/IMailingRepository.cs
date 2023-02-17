using Donus.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Donus.Domain.Interfaces.Repositories
{
    // INTERFACE MAILING REPOSITÓRIO
    public interface IMailingRepository
    {
        Task CriarMailing(MailingModel mailing);
        Task AtualizarMailing(MailingModel mailing);
        Task DeletarMailing(string mailingId);
        Task<MailingModel> RetornarMailingID(string mailingId);
        Task<List<MailingModel>> ListarMailingPorFiltro(string mailingId = null, string nome = null);
        Task<bool> ExisteMailingPorID(string mailingId);
    }
}
