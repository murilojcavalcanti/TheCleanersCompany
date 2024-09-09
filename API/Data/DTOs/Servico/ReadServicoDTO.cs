using API.Data.DTOs.Categoria;

namespace API.Data.DTOs.Serviço
{
    public class ReadServicoDTO
    {
        public string Nome { get; set; }

        public ReadCategoriaDTO Categoria { get; set; }
    }
}
