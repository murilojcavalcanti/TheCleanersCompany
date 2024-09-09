using API.Data.DTOs.Usuario;
using API.Data.Entities;
using AutoMapper;

namespace API.Profiles
{
    public class UsuarioProfile:Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDTO,User>();
            
        }
    }
}
