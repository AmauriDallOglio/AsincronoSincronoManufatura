using AsincronoSincronoManufatura.Dto;
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

            Task<EtapaProducaoDto> tarefaPreparar = _maquinaProducao.PrepararMaterial(false);
            Task<EtapaProducaoDto> tarefaCorte =   _maquinaProducao.CortarMaterial(false);
            Task<EtapaProducaoDto> tarefaMontagem = _maquinaProducao.MontarPeca(false);
            Task<EtapaProducaoDto> tarefaPintura = _maquinaProducao.PintarPeca(false);
            Task<EtapaProducaoDto> tarefaInspecao = _maquinaProducao.InspecionarQualidade(false);


            listaEtapas.Add(tarefaPreparar.Result);
            listaEtapas.Add(tarefaCorte.Result);
            listaEtapas.Add(tarefaMontagem.Result);
            listaEtapas.Add(tarefaPintura.Result);
            listaEtapas.Add(tarefaInspecao.Result);

            long tempoTotalMs = stopwatch.ElapsedMilliseconds;

            string resultado = $"Produção Síncrona:\n" +
                                $"Tempo de Preparação do Material 5s: {tarefaPreparar.Result.TempoGastoMs} ms\n" +
                                $"Tempo de Corte do Material 7s: {tarefaCorte} ms\n" +
                                $"Tempo de Montagem da Peça 8s: {tarefaMontagem} ms\n" +
                                $"Tempo de Pintura da Peça 6s: {tarefaPintura} ms\n" +
                                $"Tempo de Inspeção de Qualidade 4s: {tarefaInspecao} ms\n" +
                                $"Tempo Total de Produção 30s: {tempoTotalMs} ms";


            Console.WriteLine("------ Fim ---------");

            return Ok(listaEtapas);

        }

        [HttpGet("ProduzirPecaAssincrono")]
        public async Task<IActionResult> ProduzirPecaAssincrono()
        {
            Console.WriteLine("------ Inicio ---------");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<EtapaProducaoDto> listaEtapas = new List<EtapaProducaoDto>();

            Task<EtapaProducaoDto> tarefaPreparar = _maquinaProducao.PrepararMaterial(true);
            Task<EtapaProducaoDto> tarefaCorte =    _maquinaProducao.CortarMaterial(true);
            Task<EtapaProducaoDto> tarefaMontagem = _maquinaProducao.MontarPeca(true);
            Task<EtapaProducaoDto> tarefaPintura =  _maquinaProducao.PintarPeca(true);
            Task<EtapaProducaoDto> tarefaInspecao = _maquinaProducao.InspecionarQualidade(true);

            await Task.WhenAll(tarefaPreparar, tarefaCorte, tarefaMontagem, tarefaPintura, tarefaInspecao);

            listaEtapas.Add(tarefaPreparar.Result);
            listaEtapas.Add(tarefaCorte.Result);
            listaEtapas.Add(tarefaMontagem.Result);
            listaEtapas.Add(tarefaPintura.Result);
            listaEtapas.Add(tarefaInspecao.Result);
 


            stopwatch.Stop();
            long tempoTotalMs = stopwatch.ElapsedMilliseconds;

            string resultado = $"Produção Assíncrona:\n" +
                                $"Todas as etapas foram executadas de forma paralela.\n" +
                                $"Tempo Total de Produção: {tempoTotalMs} ms";


            Console.WriteLine("------ Fim ---------");

            return Ok(listaEtapas);
        }


        [HttpGet("ProduzirPecaAssincrono2")]
        public async Task<IActionResult> ProduzirPecaAssincrono2()
        {

            Console.WriteLine("------ Inicio ---------");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Task<EtapaProducaoDto> tarefaPreparar = _maquinaProducao.PrepararMaterial(true);
            Task<EtapaProducaoDto> tarefaCorte =    _maquinaProducao.CortarMaterial(true);
            Task<EtapaProducaoDto> tarefaMontagem = _maquinaProducao.MontarPeca(true);
            Task<EtapaProducaoDto> tarefaPintura =  _maquinaProducao.PintarPeca(true);
            Task<EtapaProducaoDto> tarefaInspecao = _maquinaProducao.InspecionarQualidade(true);





            stopwatch.Stop();
            long tempoTotalMs = stopwatch.ElapsedMilliseconds;

            string resultado = $"Produção Assíncrona (em série):\n" +
                               $"Tempo de Preparação do Material: {tarefaPreparar.Result} ms\n" +
                               $"Tempo de Corte do Material: {tarefaCorte.Result} ms\n" +
                               $"Tempo de Montagem da Peça: {tarefaMontagem.Result} ms\n" +
                               $"Tempo de Pintura da Peça: {tarefaPintura.Result} ms\n" +
                               $"Tempo de Inspeção de Qualidade: {tarefaInspecao.Result} ms\n" +
                               $"Tempo Total de Produção: {tempoTotalMs} ms";

            Console.WriteLine("------ Fim ---------");

            return Ok(resultado);
        }

    }
}
