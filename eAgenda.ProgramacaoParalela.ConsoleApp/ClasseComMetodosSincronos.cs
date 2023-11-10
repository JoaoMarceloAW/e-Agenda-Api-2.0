namespace eAgenda.ProgramacaoParalela.ConsoleApp
{
    public class ClasseComMetodosSincronos
    {
        public ClasseComMetodosSincronos()
        {
        }

        public string MetodoQueDemora_1_segundo()
        {
            //vai acontecer coisas...

            //Thread == Processo
            Thread.Sleep(1000);

            return "Demorou um 1 segundo";
        }

        public string MetodoQueDemora_2_segundos()
        {
            Thread.Sleep(2000);

            return "Demorou um 2 segundos";
        }

        public string MetodoQueDemora_3_segundos()
        {
            Thread.Sleep(3000);

            return "Demorou um 3 segundos";
        }
    }
}