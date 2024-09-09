using API.Data.DTOs.Serviço;
using API.Data.Entities;
using AutoMapper;

namespace API.Profiles
{
    public class ServicoProfile:Profile
    {
        public ServicoProfile()
        {
            CreateMap<CreateServicoDTO,Servico>();
            CreateMap<Servico,ReadServicoDTO>().ForMember(servico=>servico.Categoria,opt=>opt.MapFrom(Servico=>Servico.Categoria));
            CreateMap<UpdateServicoDTO,Servico>();
        }

    }
}
