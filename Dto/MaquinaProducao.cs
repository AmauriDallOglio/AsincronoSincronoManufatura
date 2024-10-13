using System.Diagnostics;

namespace AsincronoSincronoManufatura.Dto
{
    public class MaquinaProducao
    {

        public void PrepararMaterial()
        {
            Console.WriteLine("------ Inicio ---------");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Iniciando a preparação do material...");
            Thread.Sleep(5000); // Simula o tempo para preparar o material
            stopwatch.Stop();

            Console.WriteLine($"Material preparado. Tempo decorrido: {stopwatch.ElapsedMilliseconds} ms");
        }

        public void CortarMaterial()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Iniciando o corte do material...");
            Thread.Sleep(7000); // Simula o tempo para cortar o material
            stopwatch.Stop();

            Console.WriteLine($"Material cortado. Tempo decorrido: {stopwatch.ElapsedMilliseconds} ms");
        }

        public void MontarPeca()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Iniciando a montagem da peça...");
            Thread.Sleep(8000); // Simula o tempo para montar a peça
            stopwatch.Stop();

            Console.WriteLine($"Peça montada. Tempo decorrido: {stopwatch.ElapsedMilliseconds} ms");
        }

        public void PintarPeca()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Iniciando a pintura da peça...");
            Thread.Sleep(6000); // Simula o tempo para pintar a peça
            stopwatch.Stop();

            Console.WriteLine($"Peça pintada. Tempo decorrido: {stopwatch.ElapsedMilliseconds} ms");
        }

        public void InspecionarQualidade()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Iniciando a inspeção de qualidade...");
            Thread.Sleep(4000); // Simula o tempo para inspecionar a qualidade
            stopwatch.Stop();

            Console.WriteLine($"Inspeção de qualidade concluída. Tempo decorrido: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine("------- Fim --------");
        }

        public async Task PrepararMaterialAsync()
        {
            Console.WriteLine("------ Inicio ---------");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Iniciando a preparação do material (assíncrono)...");
            await Task.Delay(5000); // Simula o tempo para preparar o material de forma assíncrona
            stopwatch.Stop();

            Console.WriteLine($"Material preparado (assíncrono). Tempo decorrido: {stopwatch.ElapsedMilliseconds} ms");
        }

        public async Task CortarMaterialAsync()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Iniciando o corte do material (assíncrono)...");
            await Task.Delay(7000); // Simula o tempo para cortar o material de forma assíncrona
            stopwatch.Stop();

            Console.WriteLine($"Material cortado (assíncrono). Tempo decorrido: {stopwatch.ElapsedMilliseconds} ms");
        }

        public async Task MontarPecaAsync()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Iniciando a montagem da peça (assíncrono)...");
            await Task.Delay(8000); // Simula o tempo para montar a peça de forma assíncrona
            stopwatch.Stop();

            Console.WriteLine($"Peça montada (assíncrono). Tempo decorrido: {stopwatch.ElapsedMilliseconds} ms");
        }

        public async Task PintarPecaAsync()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Iniciando a pintura da peça (assíncrono)...");
            await Task.Delay(6000); // Simula o tempo para pintar a peça de forma assíncrona
            stopwatch.Stop();

            Console.WriteLine($"Peça pintada (assíncrono). Tempo decorrido: {stopwatch.ElapsedMilliseconds} ms");
        }

        public async Task InspecionarQualidadeAsync()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Iniciando a inspeção de qualidade (assíncrono)...");
            await Task.Delay(4000); // Simula o tempo para inspecionar a qualidade de forma assíncrona
            stopwatch.Stop();

            Console.WriteLine($"Inspeção de qualidade concluída (assíncrono). Tempo decorrido: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine("------- Fim --------");
        }

    }
}
