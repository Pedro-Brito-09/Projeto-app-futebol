using ProjetoOOP.Models;
using ProjetoOOP.Enums;

MariaDB Database = new MariaDB();
Database.Connect();

GerenciadorPartidas gerenciador = new GerenciadorPartidas();

while (true)
{
    Console.WriteLine("\nMenu Principal:");
    Console.WriteLine("1 - Gerenciar Jogadores");
    Console.WriteLine("2 - Gerenciar Times");
    Console.WriteLine("3 - Gerenciar Partidas");
    Console.WriteLine("4 - Sair");

    var opcaoPrincipal = Console.ReadLine();

    try
    {
        switch (opcaoPrincipal)
        {
            case "1":
                GerenciarJogadores(Database);
                break;
            case "2":
                GerenciarTimes(gerenciador);
                break;
            case "3":
                GerenciarPartidas(Database, gerenciador);
                break;
            case "4":
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

static void GerenciarJogadores(MariaDB database)
{
    while (true)
    {
        Console.WriteLine("\nMenu de Jogadores:");
        Console.WriteLine("1 - Listar Jogadores");
        Console.WriteLine("2 - Adicionar Jogador");
        Console.WriteLine("3 - Editar Jogador");
        Console.WriteLine("4 - Remover Jogador");
        Console.WriteLine("5 - Voltar");

        var opcao = Console.ReadLine();

        try
        {
            switch (opcao)
            {
                case "1":
                    ListarJogadores(database);
                    break;
                case "2":
                    AdicionarJogador(database);
                    break;
                case "3":
                    EditarJogador(database);
                    break;
                case "4":
                    RemoverJogador(database);
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
}

static void GerenciarTimes(GerenciadorPartidas gerenciador)
{
    while (true)
    {
        Console.WriteLine("\nMenu de Times:");
        Console.WriteLine("1 - Listar Times");
        Console.WriteLine("2 - Adicionar Time");
        Console.WriteLine("3 - Remover Time");
        Console.WriteLine("4 - Voltar");

        var opcao = Console.ReadLine();

        try
        {
            switch (opcao)
            {
                case "1":
                    ListarTimes(gerenciador);
                    break;
                case "2":
                    AdicionarTime(gerenciador);
                    break;
                case "3":
                    RemoverTime(gerenciador);
                    break;
                case "4":
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
}
static void GerenciarPartidas(MariaDB Database, GerenciadorPartidas gerenciador)
{
    while (true)
    {
        Console.WriteLine("\nMenu de Partidas:");
        Console.WriteLine("1 - Iniciar nova partida");
        Console.WriteLine("2 - Registrar resultado");
        Console.WriteLine("3 - Ver histórico");
        Console.WriteLine("4 - Ver estatísticas");
        Console.WriteLine("5 - Voltar");

        var opcao = Console.ReadLine();

        try
        {
            switch (opcao)
            {
                case "1":
                    gerenciador.IniciarNovaPartida(Database);
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
}

static void ListarJogadores(MariaDB database)
{
    var jogadores = database.GetJogadores();
    Console.WriteLine("\nLista de Jogadores:");
    foreach (var jogador in jogadores)
    {
        Console.WriteLine($"ID: {jogador.GetId()}, Nome: {jogador.GetNome()}, Idade: {jogador.GetIdade()}, Posição: {jogador.GetPosicao()}");
    }
}

static void AdicionarJogador(MariaDB database)
{
    Console.Write("Nome do jogador: ");
    string nome = Console.ReadLine();
    Console.Write("Idade do jogador: ");
    int idade = int.Parse(Console.ReadLine());
    Console.WriteLine("Posições disponíveis: 1-Goleiro, 2-Defesa, 3-Ataque");
    Console.Write("Posição do jogador: ");
    Posicoes posicao = (Posicoes)int.Parse(Console.ReadLine());

    Jogador novoJogador = new Jogador(0, nome, idade, posicao);
    database.AddJogador(novoJogador);
    Console.WriteLine("Jogador adicionado com sucesso!");
}

static void EditarJogador(MariaDB database)
{
    ListarJogadores(database);
    Console.Write("ID do jogador a editar: ");
    int id = int.Parse(Console.ReadLine());

    Console.Write("Novo nome: ");
    string nome = Console.ReadLine();
    Console.Write("Nova idade: ");
    int idade = int.Parse(Console.ReadLine());
    Console.WriteLine("Posições disponíveis: 1-Goleiro, 2-Defesa, 3-Ataque");
    Console.Write("Nova posição: ");
    Posicoes posicao = (Posicoes)int.Parse(Console.ReadLine());

    Jogador jogadorEditado = new Jogador(id, nome, idade, posicao);
    database.EditarJogador(id, jogadorEditado);
    Console.WriteLine("Jogador editado com sucesso!");
}

static void RemoverJogador(MariaDB database)
{
    ListarJogadores(database);
    Console.Write("ID do jogador a remover: ");
    int id = int.Parse(Console.ReadLine());
    database.RemoveJogador(id);
    Console.WriteLine("Jogador removido com sucesso!");
}

static void ListarTimes(GerenciadorPartidas gerenciador)
{
    var times = gerenciador.GetTimes();
    Console.WriteLine("\nLista de Times:");
    foreach (var time in times)
    {
        Console.WriteLine($"Nome: {time.GetNome()}, Vitórias: {time.GetVitorias()}, Empates: {time.GetEmpates()}, Derrotas: {time.GetDerrotas()}");
    }
}

static void AdicionarTime(GerenciadorPartidas gerenciador)
{
    Console.Write("Nome do time: ");
    string nome = Console.ReadLine();

    Time novoTime = new Time(nome);
    gerenciador.AdicionarTime(novoTime);
    Console.WriteLine("Time adicionado com sucesso!");
}

static void RemoverTime(GerenciadorPartidas gerenciador)
{
    ListarTimes(gerenciador);
    Console.Write("Nome do time a remover: ");
    string nome = Console.ReadLine();
    gerenciador.RemoverTime(nome);
    Console.WriteLine("Times com esse nome foram removidos com sucesso!");
}