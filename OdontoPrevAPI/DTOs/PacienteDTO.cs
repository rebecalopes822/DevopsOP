namespace OdontoPrevAPI.DTOs
{
    public class PacienteDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<SinistroDTO> Sinistros { get; set; }
    }
}
