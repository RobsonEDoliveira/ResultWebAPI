using Donus.Domain.Models;
using Donus.Domain.Validations.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Donus.Domain.Interfaces.Services
{
    // INTERFACE MAILING SERVICE
    public interface IMailingService
    {
        Task<Response> CriarMailing(MailingModel mailing);
        Task<Response> AtualizarMailing(MailingModel mailing);
        Task<Response> DeletarMailing(string mailingId);
        Task<Response<MailingModel>> RetornarMailingID(string mailingId);
        Task<Response<List<MailingModel>>> ListarMailingPorFiltro(string mailingId = null, string name = null);
    }
}
