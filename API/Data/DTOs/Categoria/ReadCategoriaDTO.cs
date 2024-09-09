using API.Data.DTOs.Serviço;

namespace API.Data.DTOs.Categoria
{
    public class ReadCategoriaDTO
    {
        public string? Nome { get; set; }
        public ICollection<ReadServicoDTO> Servico { get; set; }
    }
}
