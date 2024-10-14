using AsincronoSincronoManufatura.Dto;

namespace AsincronoSincronoManufatura.Util
{
    public class ResultadoOperacao
    {
        public string Mensagem { get; set; } = string.Empty;
        public List<EtapaProducaoDto> EtapaProducaoDtos { get; set; } = new List<EtapaProducaoDto>();

        public ResultadoOperacao() { }

        public ResultadoOperacao Adicionar(string mensagem, List<EtapaProducaoDto> lista)
        {
            Mensagem = mensagem;
            EtapaProducaoDtos = lista;
            return this; // Retorna a própria instância atualizada
        }
    }

}
