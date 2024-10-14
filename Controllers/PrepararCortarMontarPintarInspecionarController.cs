using AsincronoSincronoManufatura.Dto;
using AsincronoSincronoManufatura.Util;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AsincronoSincronoManufatura.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PrepararCortarMontarPintarInspecionarController : ControllerBase
    {
        private readonly EtapaProducaoDto _maquinaProducao;

        public PrepararCortarMontarPintarInspecionarController()
        {
            _maquinaProducao = new EtapaProducaoDto();
        }

        [HttpGet("ProduzirPecaSincrono")]
        public IActionResult ProduzirPecaSincrono()
        {
            Console.WriteLine("------ Inicio ---------");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<EtapaProducaoDto> listaEtapas = new List<EtapaProducaoDto>();

            Task<EtapaProducaoDto> tarefaPreparar = new EtapaProducaoDto().PrepararMaterial(false);
            listaEtapas.Add(tarefaPreparar.Result);
            Task<EtapaProducaoDto> tarefaCorte = new EtapaProducaoDto().CortarMaterial(false);
            listaEtapas.Add(tarefaCorte.Result);
            Task<EtapaProducaoDto> tarefaMontagem = new EtapaProducaoDto().MontarPeca(false);
            listaEtapas.Add(tarefaMontagem.Result);
            Task<EtapaProducaoDto> tarefaPintura = new EtapaProducaoDto().PintarPeca(false);
            listaEtapas.Add(tarefaPintura.Result);
            Task<EtapaProducaoDto> tarefaInspecao = new EtapaProducaoDto().InspecionarQualidade(false);
            listaEtapas.Add(tarefaInspecao.Result);

            long tempoTotalMs = stopwatch.ElapsedMilliseconds;

            string texto = $"Produção Síncrona:\n" +
                           $"Tempo de Preparação do Material 5s: {tarefaPreparar.Result.TempoGastoMs} ms\n" +
                           $"Tempo de Corte do Material 7s: {tarefaCorte.Result.TempoGastoMs} ms\n" +
                           $"Tempo de Montagem da Peça 8s: {tarefaMontagem.Result.TempoGastoMs} ms\n" +
                           $"Tempo de Pintura da Peça 6s: {tarefaPintura.Result.TempoGastoMs} ms\n" +
                           $"Tempo de Inspeção de Qualidade 4s: {tarefaInspecao.Result.TempoGastoMs} ms\n" +
                           $"Tempo Total de Produção 30s: {tempoTotalMs} ms";

            Console.WriteLine("------ Fim ---------");

            ResultadoOperacao resultado = new ResultadoOperacao().Adicionar(texto, listaEtapas);

            return Ok(resultado);
        }


        [HttpGet("ProduzirPecaAssincrono")]
        public async Task<IActionResult> ProduzirPecaAssincrono()
        {
            Console.WriteLine("------ Inicio ---------");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<EtapaProducaoDto> listaEtapas = new List<EtapaProducaoDto>();

            Task<EtapaProducaoDto> tarefaPreparar = new EtapaProducaoDto().PrepararMaterial(true);
            Task<EtapaProducaoDto> tarefaCorte = new EtapaProducaoDto().CortarMaterial(true);
            Task<EtapaProducaoDto> tarefaMontagem = new EtapaProducaoDto().MontarPeca(true);
            Task<EtapaProducaoDto> tarefaPintura = new EtapaProducaoDto().PintarPeca(true);
            Task<EtapaProducaoDto> tarefaInspecao = new EtapaProducaoDto().InspecionarQualidade(true);

            await Task.WhenAll(tarefaPreparar, tarefaCorte, tarefaMontagem, tarefaPintura, tarefaInspecao);

            listaEtapas.Add(tarefaPreparar.Result);
            listaEtapas.Add(tarefaCorte.Result);
            listaEtapas.Add(tarefaMontagem.Result);
            listaEtapas.Add(tarefaPintura.Result);
            listaEtapas.Add(tarefaInspecao.Result);
 


            stopwatch.Stop();
            long tempoTotalMs = stopwatch.ElapsedMilliseconds;

            string texto = $"Produção Assíncrona:\n" +
                                $"Todas as etapas foram executadas de forma paralela.\n" +
                                $"Tempo Total de Produção: {tempoTotalMs} ms";


            Console.WriteLine("------ Fim ---------");

            ResultadoOperacao resultado = new ResultadoOperacao().Adicionar(texto, listaEtapas);

            return Ok(resultado);
        }


        [HttpGet("ProduzirPecaAssincrono2")]
        public async Task<IActionResult> ProduzirPecaAssincrono2()
        {

            Console.WriteLine("------ Inicio ---------");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<EtapaProducaoDto> listaEtapas = new List<EtapaProducaoDto>();

            Task<EtapaProducaoDto> tarefaPreparar = new EtapaProducaoDto().PrepararMaterial(true);
            listaEtapas.Add(tarefaPreparar.Result);
            Task<EtapaProducaoDto> tarefaCorte = new EtapaProducaoDto().CortarMaterial(true);
            listaEtapas.Add(tarefaCorte.Result);
            Task<EtapaProducaoDto> tarefaMontagem = new EtapaProducaoDto().MontarPeca(true);
            listaEtapas.Add(tarefaMontagem.Result);
            Task<EtapaProducaoDto> tarefaPintura = new EtapaProducaoDto().PintarPeca(true);
            listaEtapas.Add(tarefaPintura.Result);
            Task<EtapaProducaoDto> tarefaInspecao = new EtapaProducaoDto().InspecionarQualidade(true);
            listaEtapas.Add(tarefaInspecao.Result);


            stopwatch.Stop();
            long tempoTotalMs = stopwatch.ElapsedMilliseconds;

            string texto = $"Produção Assíncrona (em série):\n" +
                               $"Tempo de Preparação do Material: {tarefaPreparar.Result.TempoGastoMs} ms\n" +
                               $"Tempo de Corte do Material: {tarefaCorte.Result.TempoGastoMs} ms\n" +
                               $"Tempo de Montagem da Peça: {tarefaMontagem.Result.TempoGastoMs} ms\n" +
                               $"Tempo de Pintura da Peça: {tarefaPintura.Result.TempoGastoMs} ms\n" +
                               $"Tempo de Inspeção de Qualidade: {tarefaInspecao.Result.TempoGastoMs} ms\n" +
                               $"Tempo Total de Produção: {tempoTotalMs} ms";

            Console.WriteLine("------ Fim ---------");

            ResultadoOperacao resultado = new ResultadoOperacao().Adicionar(texto, listaEtapas);

            return Ok(resultado);
        }

    }
}
