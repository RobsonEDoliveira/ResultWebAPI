using AutoMapper;
using Donus.Application.DataContract.Request.Mailing;
using Donus.Application.DataContract.Request.Usuario;
using Donus.Application.DataContract.Response.Mailing;
using Donus.Application.DataContract.Response.Usuario;
using Donus.Domain.Models;

namespace Donus.Application.Mapper
{
    public class Core : Profile
    {
        public Core()
        {
            ClientMap();
        }

        private void ClientMap()
        {
            // MAILING
            CreateMap<CreateMailingRequest, MailingModel>();
            CreateMap<UpdateMailingRequest, MailingModel>();
            CreateMap<MailingModel, MailingResponse>();

            // USER
            CreateMap<CreateUsuarioRequest, UsuarioModel>()
                .ForMember(target => target.PasswordHash, opt => opt.MapFrom(source => source.Password));
            CreateMap<UsuarioModel, UsuarioResponse>();
        }
    }
}
