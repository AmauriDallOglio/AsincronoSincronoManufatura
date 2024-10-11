using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsincronoSincronoManufatura.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaquinaProducaoPecasController : ControllerBase
    {
        private readonly MaquinaProducao _maquinaProducao;

        public MaquinaProducaoPecasController()
        {
            _maquinaProducao = new MaquinaProducao();
        }

        [HttpGet("ProduzirPecaSincrono")]
        public IActionResult ProduzirPecaSincrono()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Medindo o tempo de corte da peça
            Stopwatch tempoCorte = Stopwatch.StartNew();
            _maquinaProducao.CortarPeca();
            tempoCorte.Stop();

            // Medindo o tempo de montagem da peça
            Stopwatch tempoMontagem = Stopwatch.StartNew();
            _maquinaProducao.MontarPeca();
            tempoMontagem.Stop();

            stopwatch.Stop();
            long tempoTotalMs = stopwatch.ElapsedMilliseconds;

            string resultado = $"Produção Síncrona:\n" +
                               $"Tempo de Corte da Peça 7s: {tempoCorte.ElapsedMilliseconds} ms\n" +
                               $"Tempo de Montagem da Peça 8s: {tempoMontagem.ElapsedMilliseconds} ms\n" +
                               $"Tempo Total de Produção: {tempoTotalMs} ms";

            return Ok(resultado);
        }

        [HttpGet("ProduzirPecaAssincrono")]
        public async Task<IActionResult> ProduzirPecaAssincrono()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Medindo o tempo de corte e montagem da peça de forma assíncrona
            Stopwatch tempoCorte = Stopwatch.StartNew();
            var tarefaCorte = _maquinaProducao.CortarPecaAsync();
            tempoCorte.Stop();

            Stopwatch tempoMontagem = Stopwatch.StartNew();
            var tarefaMontagem = _maquinaProducao.MontarPecaAsync();
            tempoMontagem.Stop();

            await Task.WhenAll(tarefaCorte, tarefaMontagem);

            stopwatch.Stop();
            long tempoTotalMs = stopwatch.ElapsedMilliseconds;

            string resultado = $"Produção Assíncrona:\n" +
                               $"Tempo de Corte da Peça (Assíncrono) 7s: {tempoCorte.ElapsedMilliseconds} ms\n" +
                               $"Tempo de Montagem da Peça (Assíncrono) 8s: {tempoMontagem.ElapsedMilliseconds} ms\n" +
                               $"Tempo Total de Produção: {tempoTotalMs} ms";

            return Ok(resultado);
        }
    }

    public class MaquinaProducao
    {
        public void CortarPeca()
        {
            Thread.Sleep(7000); // Simula o tempo para cortar uma peça
        }

        public void MontarPeca()
        {
            Thread.Sleep(8000); // Simula o tempo para montar uma peça
        }

        public async Task CortarPecaAsync()
        {
            await Task.Delay(7000); // Simula o tempo para cortar uma peça de forma assíncrona
        }

        public async Task MontarPecaAsync()
        {
            await Task.Delay(8000); // Simula o tempo para montar uma peça de forma assíncrona
        }
    }
}
