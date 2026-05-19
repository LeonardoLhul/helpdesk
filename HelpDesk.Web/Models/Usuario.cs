    namespace HelpDesk.Web.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string Perfil { get; set; } = "Cliente";
        public DateTime Criado { get; set; } = DateTime.Now;
        public List<Chamado> ChamadosCriados { get; set; } = new();
        public List<ComentarioChamado> Comentarios { get; set; } = new();
    }
}