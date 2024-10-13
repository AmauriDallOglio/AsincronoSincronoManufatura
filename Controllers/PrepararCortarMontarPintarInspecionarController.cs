using AsincronoSincronoManufatura.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AsincronoSincronoManufatura.Controllers
{
 
    [ApiController]
    [Route("api/[controller]")]
    public class PrepararCortarMontarPintarInspecionarController : ControllerBase
    {
        private readonly MaquinaProducao _maquinaProducao;

        public PrepararCortarMontarPintarInspecionarController()
        {
            _maquinaProducao = new MaquinaProducao();
        }

        [HttpGet("ProduzirPecaSincrono")]
        public IActionResult ProduzirPecaSincrono()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Stopwatch tempoPreparar = Stopwatch.StartNew();
            _maquinaProducao.PrepararMaterial();
            tempoPreparar.Stop();

            Stopwatch tempoCorte = Stopwatch.StartNew();
            _maquinaProducao.CortarMaterial();
            tempoCorte.Stop();

            Stopwatch tempoMontagem = Stopwatch.StartNew();
            _maquinaProducao.MontarPeca();
            tempoMontagem.Stop();

            Stopwatch tempoPintura = Stopwatch.StartNew();
            _maquinaProducao.PintarPeca();
            tempoPintura.Stop();

            Stopwatch tempoInspecao = Stopwatch.StartNew();
            _maquinaProducao.InspecionarQualidade();
            tempoInspecao.Stop();

            stopwatch.Stop();
            long tempoTotalMs = stopwatch.ElapsedMilliseconds;

            string resultado = $"Produção Síncrona:\n" +
                                $"Tempo de Preparação do Material 5s: {tempoPreparar.ElapsedMilliseconds} ms\n" +
                                $"Tempo de Corte do Material 7s: {tempoCorte.ElapsedMilliseconds} ms\n" +
                                $"Tempo de Montagem da Peça 8s: {tempoMontagem.ElapsedMilliseconds} ms\n" +
                                $"Tempo de Pintura da Peça 6s: {tempoPintura.ElapsedMilliseconds} ms\n" +
                                $"Tempo de Inspeção de Qualidade 4s: {tempoInspecao.ElapsedMilliseconds} ms\n" +
                                $"Tempo Total de Produção 30s: {tempoTotalMs} ms";

            return Ok(resultado);
        }

        [HttpGet("ProduzirPecaAssincrono")]
        public async Task<IActionResult> ProduzirPecaAssincrono()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var tarefaPreparar = _maquinaProducao.PrepararMaterialAsync();
            var tarefaCorte = _maquinaProducao.CortarMaterialAsync();
            var tarefaMontagem = _maquinaProducao.MontarPecaAsync();
            var tarefaPintura = _maquinaProducao.PintarPecaAsync();
            var tarefaInspecao = _maquinaProducao.InspecionarQualidadeAsync();

            await Task.WhenAll(tarefaPreparar, tarefaCorte, tarefaMontagem, tarefaPintura, tarefaInspecao);

            stopwatch.Stop();
            long tempoTotalMs = stopwatch.ElapsedMilliseconds;

            string resultado = $"Produção Assíncrona:\n" +
                                $"Todas as etapas foram executadas de forma paralela.\n" +
                                $"Tempo Total de Produção: {tempoTotalMs} ms";

            return Ok(resultado);
        }
    }

   
}

