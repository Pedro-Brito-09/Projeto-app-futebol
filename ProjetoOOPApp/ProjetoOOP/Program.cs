using ProjetoOOP.Models;
using ProjetoOOP.Enums;

MariaDB Database = new MariaDB();
Database.Connect();

Jogador pedro = new Jogador(0, "Pedro", 18, Posicoes.GOLEIRO);
Database.AddJogador(pedro);

Jogador matheus = new Jogador(0, "Matheus", 24, Posicoes.ATAQUE);
Database.AddJogador(matheus);

Jogador gustavo = new Jogador(0, "Gustavo", 30, Posicoes.DEFESA);
Database.AddJogador(gustavo);

Jogo jogoNaFaculdade = new Jogo(0, "21/02/2102", "UNASP", "Society", 10, 2);
Database.AddJogo(jogoNaFaculdade);

Time timeA = new Time("Time A");
Time timeB = new Time("Time B");
Time timeC = new Time("Time C");
Time timeD = new Time("Time D");

GerenciadorPartidas gerenciador = new GerenciadorPartidas();
gerenciador.AdicionarTime(timeA);
gerenciador.AdicionarTime(timeB);
gerenciador.AdicionarTime(timeC);
gerenciador.AdicionarTime(timeD);

foreach (Jogador jogador in Database.GetJogadores())
{
    Console.WriteLine(jogador.GetId() + ", " + jogador.GetNome() + ", " + jogador.GetIdade() + ", " + jogador.GetPosicao());
}

foreach (Jogo jogo in Database.GetJogos())
{
    Console.WriteLine(jogo.GetId() + ", " + jogo.GetData() + ", " + jogo.GetLocal() + ", " + jogo.GetCampo() + ", " + jogo.GetJogadoresPorTime() + ", " + jogo.GetMaxTimes());
}

while (true)
{
    Console.WriteLine("\nMenu:");
    Console.WriteLine("1 - Iniciar nova partida");
    Console.WriteLine("2 - Registrar resultado");
    Console.WriteLine("3 - Ver histórico");
    Console.WriteLine("4 - Ver estatísticas");
    Console.WriteLine("5 - Sair");

    var opcao = Console.ReadLine();

    try
    {
        switch (opcao)
        {
            case "1":
                gerenciador.IniciarNovaPartida();
                break;
            case "2":
                if (gerenciador.PartidaAtual == null)
                {
                    Console.WriteLine("Nenhuma partida em andamento.");
                    break;
                }
                Console.Write($"Gols do {gerenciador.PartidaAtual.TimeA.GetNome()}: ");
                int golsA = int.Parse(Console.ReadLine());
                Console.Write($"Gols do {gerenciador.PartidaAtual.TimeB.GetNome()}: ");
                int golsB = int.Parse(Console.ReadLine());
                gerenciador.RegistrarResultado(golsA, golsB);
                break;
            case "3":
                gerenciador.ExibirHistorico();
                break;
            case "4":
                gerenciador.ExibirEstatisticasTimes();
                break;
            case "5":
                return;
            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }
}