namespace eAgenda.ProgramacaoParalela.ConsoleApp
{
    internal class ClasseComMetodosAssincronos
    {
        public ClasseComMetodosAssincronos()
        {
        }

        public async Task<string> MetodoQueDemora_1_segundoAsync()
        {
            //Thread == Processo

            return await Task.Run(() =>
            {
                Thread.Sleep(1000);

                return "Demorou um 1 segundo";
            });
        }

        public async Task<string> MetodoQueDemora_2_segundosAsync()
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(2000);

                return "Demorou um 2 segundos";
            });
        }

        public async Task<string> MetodoQueDemora_3_segundosAsync()
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(3000);

                return "Demorou um 3 segundos";
            });
        }
    }
}