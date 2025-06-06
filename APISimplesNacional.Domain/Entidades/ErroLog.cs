
namespace APISimplesNacional.Infra.Entidades
{
    public class ErroLog
    {
        public int Id { get; set; }
        public DateTime DataOcorrencia { get; set; } = DateTime.UtcNow;
        public string Mensagem { get; set; } = string.Empty;
        public string StackTrace { get; set; } = string.Empty;
        public string Origem { get; set; } = string.Empty;
        public int StatusCode { get; set; }
    }

}
