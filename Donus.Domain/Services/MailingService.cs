using Donus.Domain.Common;
using Donus.Domain.Interfaces.Repositories;
using Donus.Domain.Interfaces.Services;
using Donus.Domain.Models;
using Donus.Domain.Validations;
using Donus.Domain.Validations.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Donus.Domain.Services
{
    public class MailingService : IMailingService
    {
        private readonly IMailingRepository _MailingRepository;
        private readonly ITimeProvider _timeProvider;
        private readonly IGenerators _generators;

        // CONSTRUTOR
        public MailingService(IMailingRepository MailingRepository, ITimeProvider timeProvider, IGenerators generators)
        {
            _MailingRepository = MailingRepository;
            _timeProvider = timeProvider;
            _generators = generators;
        }

        // CRIAR NOVO MAILING
        public async Task<Response> CriarMailing(MailingModel mailing)
        {
            var response = new Response();

            var validation = new MailingValidation();
            var errors = validation.Validate(mailing).GetErrors();

            if (errors.Report.Count > 0)
                return errors;

            mailing.Id = _generators.Generate();
            mailing.DataCadastro = _timeProvider.utcDateTime();

            await _MailingRepository.CriarMailing(mailing);

            return response;
        }

        // DELETAR MAILING
        public async Task<Response> DeletarMailing(string mailingId)
        {
            var response = new Response();

            var exists = await _MailingRepository.ExisteMailingPorID(mailingId);

            if (!exists)
            {
                response.Report.Add(Report.Create($"Mailing {mailingId} não existe!"));
                return response;
            }

            await _MailingRepository.DeletarMailing(mailingId);

            return response;
        }

        // RETORNAR MAILING POR ID
        public async Task<Response<MailingModel>> RetornarMailingID(string mailingId)
        {
            var response = new Response<MailingModel>();

            var exists = await _MailingRepository.ExisteMailingPorID(mailingId);

            if (!exists)
            {
                response.Report.Add(Report.Create($"Mailing {mailingId} não existe!"));
                return response;
            }

            var data = await _MailingRepository.RetornarMailingID(mailingId);
            response.Data = data;
            return response;
        }

        // LISTAR MAILING POR FILTRO
        public async Task<Response<List<MailingModel>>> ListarMailingPorFiltro(string mailingId = null, string nome = null)
        {
            var response = new Response<List<MailingModel>>();

            if (!string.IsNullOrWhiteSpace(mailingId))
            {
                var exists = await _MailingRepository.ExisteMailingPorID(mailingId);

                if (!exists)
                {
                    response.Report.Add(Report.Create($"Mailing {mailingId} não existe!"));
                    return response;
                }
            }

            var data = await _MailingRepository.ListarMailingPorFiltro(mailingId, nome);
            response.Data = data;

            return response;
        }

        // ATUALIZAR MAILING
        public async Task<Response> AtualizarMailing(MailingModel mailing)
        {
            var response = new Response();

            var validation = new MailingValidation();
            var errors = validation.Validate(mailing).GetErrors();

            if (errors.Report.Count > 0)
                return errors;

            var exists = await _MailingRepository.ExisteMailingPorID(mailing.Id);

            if (!exists)
            {
                response.Report.Add(Report.Create($"Mailing {mailing.Id} não existe!"));
                return response;
            }

            await _MailingRepository.AtualizarMailing(mailing);

            return response;
        }
    }
}
