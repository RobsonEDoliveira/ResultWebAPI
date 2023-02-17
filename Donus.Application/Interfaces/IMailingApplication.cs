using Donus.Application.DataContract.Request.Mailing;
using Donus.Application.DataContract.Response.Mailing;
using Donus.Domain.Validations.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Donus.Application.Interfaces
{
    public interface IMailingApplication
    {
        Task<Response> CriarMailing(CreateMailingRequest mailing);
        Task<Response> AtualizarMailing(UpdateMailingRequest mailing);
        Task<Response> DeletarMailing(string mailingId);
        Task<Response<MailingResponse>> RetornarMailingID(string mailingId);
        Task<Response<List<MailingResponse>>> ListarMailingPorFiltro(string mailingId = null, string name = null); 
    }
}
