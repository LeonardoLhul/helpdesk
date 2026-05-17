namespace HelpDesk.Web.Models
{
    public class Chamado
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Status { get; set; } = "Aberto";
        public string Prioridade { get; set; } = "Media";

        public string Categoria { get; set; } = string.Empty;
        public DateTime Criado { get; set; } = DateTime.Now;
        public DateTime? AtualizadoEm { get; set; }
        public DateTime? FinalizadoEm { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public List<ComentarioChamado> Comentarios { get; set; } = new();
    }
}