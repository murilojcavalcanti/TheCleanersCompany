using API.Data.DTOs.Categoria;
using API.Data.Entities;
using AutoMapper;

namespace API.Profiles
{
    public class CategoriaProfile:Profile
    {
        public CategoriaProfile()
        {
            CreateMap<CreateCategoriaDTO,Categoria>();
            CreateMap<Categoria, ReadCategoriaDTO>().ForMember(categoria=>categoria.Servico,opt=>opt.MapFrom(Categoria=>Categoria.Servicos));
            CreateMap<UpdateCategoriaDTO,Categoria>();
        }

    }
}
