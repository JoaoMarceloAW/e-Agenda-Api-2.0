using System.Diagnostics;

namespace eAgenda.ProgramacaoParalela.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Progrmação paralela
            //Programação Síncrona
            //-> tempo gasto de 6 segundos

            //Programação Assíncrona
            //-> tempo gastro de 3 segundos

            //ClasseComMetodosSincronos obj = new ClasseComMetodosSincronos();
            ClasseComMetodosAssincronos obj = new ClasseComMetodosAssincronos();

            Stopwatch cronometro = new Stopwatch();

            cronometro.Start();

            Task<string> demorou_1_segundo = obj.MetodoQueDemora_1_segundoAsync();
            Task<string> demorou_2_segundos = obj.MetodoQueDemora_2_segundosAsync();
            Task<string> demorou_3_segundos = obj.MetodoQueDemora_3_segundosAsync();

            Console.WriteLine(demorou_1_segundo.Result);
            Console.WriteLine(demorou_2_segundos.Result);
            Console.WriteLine(demorou_3_segundos.Result);

            cronometro.Stop();

            var tempoGasto = cronometro.Elapsed;

            Console.WriteLine("Tempo gasto no processamento síncrono: " + tempoGasto);

            Console.ReadLine();
        }
    }
}