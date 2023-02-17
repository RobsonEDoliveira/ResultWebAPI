using AutoMapper;
using Donus.Application.DataContract.Request.Mailing;
using Donus.Application.DataContract.Response.Mailing;
using Donus.Application.Interfaces;
using Donus.Domain.Interfaces.Services;
using Donus.Domain.Models;
using Donus.Domain.Validations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donus.Application.Applications
{
    public class MailingApplication : IMailingApplication
    {
        private readonly IMailingService _mailingService;
        private readonly IMapper _mapper;

        // CONSTRUTOR
        public MailingApplication(IMailingService MailingService, IMapper mapper)
        {
            _mailingService = MailingService;
            _mapper = mapper;
        }

        // ATUALIZAR MAILING
        public async Task<Response> AtualizarMailing(UpdateMailingRequest request)
        {
            var clientModel = _mapper.Map<MailingModel>(request);

            return await _mailingService.AtualizarMailing(clientModel);
        }

        // CRIAR NOVO MAILING
        public async Task<Response> CriarMailing(CreateMailingRequest Mailing)
        {
            try
            {
                var MailingModel = _mapper.Map<MailingModel>(Mailing);

                return await _mailingService.CriarMailing(MailingModel);
            }
            catch (Exception ex)
            {
                var response = Report.Create(ex.Message);

                return Response.Unprocessable(response);
            }
        }

        // DELETAR MAILING
        public async Task<Response> DeletarMailing(string mailingId)
        {
            return await _mailingService.DeletarMailing(mailingId);
        }

        // LISTAR MAILING POR FILTRO
        public async Task<Response<List<MailingResponse>>> ListarMailingPorFiltro(string mailingId, string nome)
        {
            Response<List<MailingModel>> mailing = await _mailingService.ListarMailingPorFiltro(mailingId, nome);

            if (mailing.Report.Any())
                return Response.Unprocessable<List<MailingResponse>>(mailing.Report);

            var response = _mapper.Map<List<MailingResponse>>(mailing.Data);

            return Response.OK(response);
        }

        // RETORNAR MAILING POR ID
        public async Task<Response<MailingResponse>> RetornarMailingID(string mailingId)
        {
            Response<MailingModel> mailing = await _mailingService.RetornarMailingID(mailingId);

            if (mailing.Report.Any())
                return Response.Unprocessable<MailingResponse>(mailing.Report);

            var response = _mapper.Map<MailingResponse>(mailing.Data);

            return Response.OK(response);
        }
    }
}
