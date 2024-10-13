using AsincronoSincronoManufatura.Util;
using System.ComponentModel;
using System.Diagnostics;

namespace AsincronoSincronoManufatura.Dto
{
    public class EtapaProducaoDto
    {
        public NomeEtapaProducao NomeEtapa { get; set; }
        public TempoEstimadoProducao TempoEstimadoMs { get; set; }
        public StatusEtapaProducao Status { get; set; }
        public long TempoGastoMs { get; set; }
        public bool Assincrono { get; set; }
        public string Mensagem { get; set; }

        private Stopwatch _stopwatch;

 
        public EtapaProducaoDto()
        {
            TempoEstimadoMs = 0;
            Status = StatusEtapaProducao.Pendente;
            TempoGastoMs = 0;
            Assincrono = false;
            Mensagem = string.Empty;
            _stopwatch = new Stopwatch();
        }

        public enum StatusEtapaProducao
        {
            Pendente,
            EmAndamento,
            Concluida,
            Falha
        }

        public enum NomeEtapaProducao
        {
            PrepararMaterial,
            CortarMaterial,
            MontarPeca,
            PintarPeca,
            InspecionarQualidade
        }


        public enum TempoEstimadoProducao
        {
            [Description("Preparar Material")]
            PrepararMaterial = 5000,   // 5 segundos

            [Description("Cortar Material")]
            CortarMaterial = 7000,     // 7 segundos

            [Description("Montar Peça")]
            MontarPeca = 8000,         // 8 segundos

            [Description("Pintar Peça")]
            PintarPeca = 6000,         // 6 segundos

            [Description("Inspecionar Qualidade")]
            InspecionarQualidade = 4000 // 4 segundos
        }



        public async Task<EtapaProducaoDto> PrepararMaterial(bool assincrono)
        {
 
            var tempoEstimado = TempoEstimadoProducao.PrepararMaterial;
            TempoEstimadoMs = tempoEstimado;
            var nomeEtapaProducao = NomeEtapaProducao.PrepararMaterial;
            NomeEtapa = nomeEtapaProducao;

            Status = StatusEtapaProducao.Pendente;
            Mensagem = "Preparação do material em andamento.";
            if (!assincrono)
            {
                ExecutarEtapa(tempoEstimado, nomeEtapaProducao);
            }
            else
            {
                await ExecutarEtapaAsync(tempoEstimado, nomeEtapaProducao);
            }

            return this;
        }

        public async Task<EtapaProducaoDto> CortarMaterial(bool assincrono)
        {
 
            var tempoEstimado = TempoEstimadoProducao.CortarMaterial;
            TempoEstimadoMs = tempoEstimado;
            var nomeEtapaProducao = NomeEtapaProducao.CortarMaterial;
            NomeEtapa = nomeEtapaProducao;

            Status = StatusEtapaProducao.Pendente;
            Mensagem = "Corte do material em andamento.";
            if (!assincrono)
            {
                ExecutarEtapa(tempoEstimado, nomeEtapaProducao);
            }
            else
            {
                await ExecutarEtapaAsync(tempoEstimado, nomeEtapaProducao);
            }

            return this;
        }

        public async Task<EtapaProducaoDto> MontarPeca(bool assincrono)
        {
  
            var tempoEstimado = TempoEstimadoProducao.MontarPeca;
            TempoEstimadoMs = tempoEstimado;
            var nomeEtapaProducao = NomeEtapaProducao.MontarPeca;
            NomeEtapa = nomeEtapaProducao;

            Status = StatusEtapaProducao.Pendente;
            Mensagem = "Montagem da peça em andamento.";

            if (!assincrono)
            {
                ExecutarEtapa(tempoEstimado, nomeEtapaProducao);
            }
            else
            {
                await ExecutarEtapaAsync(tempoEstimado, nomeEtapaProducao);
            }

            return this;
        }

        public async Task<EtapaProducaoDto> PintarPeca(bool assincrono)
        {
 

            var tempoEstimado = TempoEstimadoProducao.PintarPeca;
            TempoEstimadoMs = tempoEstimado;
            var nomeEtapaProducao = NomeEtapaProducao.PintarPeca;
            NomeEtapa = nomeEtapaProducao;


            Status = StatusEtapaProducao.Pendente;
            Mensagem = "Pintura da peça em andamento.";

            if (!assincrono)
            {
                ExecutarEtapa(tempoEstimado, nomeEtapaProducao);
            }
            else
            {
                await ExecutarEtapaAsync(tempoEstimado, nomeEtapaProducao);
            }

            return this;
        }

        public async Task<EtapaProducaoDto> InspecionarQualidade(bool assincrono)
        {
            var tempoEstimado = TempoEstimadoProducao.InspecionarQualidade;
            TempoEstimadoMs = tempoEstimado;
            var nomeEtapaProducao = NomeEtapaProducao.InspecionarQualidade;
            NomeEtapa = nomeEtapaProducao;
            
            Status = StatusEtapaProducao.Pendente;
            Mensagem = "Inspeção de qualidade em andamento.";
 

            if (!assincrono)
            {
                ExecutarEtapa(tempoEstimado, nomeEtapaProducao);
            }
            else
            {
                await ExecutarEtapaAsync(tempoEstimado, nomeEtapaProducao);
            }

            return this;
        }

        public long ExecutarEtapa( TempoEstimadoProducao tempoEstimado, NomeEtapaProducao nomeEtapaProducao)
        {
            _stopwatch.Restart();
            Assincrono = false;
            Status = StatusEtapaProducao.EmAndamento;
            string descricao = tempoEstimado.ObterDescricao();
            int valorNumerico = (int)tempoEstimado;

            Console.WriteLine($"Etapa: {descricao} / Tempo Estimado: {valorNumerico} ms / Iniciando...");
            Console.WriteLine($"Etapa: {descricao} / Tempo Estimado: {valorNumerico} ms / em andamento síncrona...");

            Thread.Sleep(((int)tempoEstimado));

            _stopwatch.Stop();
            TempoGastoMs = _stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Etapa: {descricao} / Tempo Estimado: {valorNumerico} ms / Concluída.Tempo decorrido: { TempoGastoMs} ms");

            Status = StatusEtapaProducao.Concluida;

            return TempoGastoMs;
        }

        public async Task<long> ExecutarEtapaAsync(TempoEstimadoProducao tempoEstimado, NomeEtapaProducao nomeEtapaProducao)
        {
            _stopwatch.Restart();
            Assincrono = true;
            Status = StatusEtapaProducao.EmAndamento;
            string descricao = tempoEstimado.ObterDescricao();
            int valorNumerico = (int)tempoEstimado;


            Console.WriteLine($"Etapa: {descricao} / Tempo Estimado: {valorNumerico} ms / Iniciando...");
            Console.WriteLine($"Etapa: {descricao} / Tempo Estimado: {valorNumerico} ms / em andamento assíncrona...");

            await Task.Delay(((int)tempoEstimado)); 

            _stopwatch.Stop();
            TempoGastoMs = _stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Etapa: {descricao} / Tempo Estimado: {valorNumerico} ms / Concluída. Tempo decorrido: {TempoGastoMs} ms");
            Status = StatusEtapaProducao.Concluida;

            return TempoGastoMs;
        }


    }
}
