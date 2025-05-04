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

foreach (Jogador jogador in Database.GetJogadores())
{
    Console.WriteLine(jogador.GetId() + ", " + jogador.GetNome() + ", " + jogador.GetIdade() + ", " + jogador.GetPosicao());
}

foreach (Jogo jogo in Database.GetJogos())
{
    Console.WriteLine(jogo.GetId() + ", " + jogo.GetData() + ", " + jogo.GetLocal() + ", " + jogo.GetCampo() + ", " + jogo.GetJogadoresPorTime() + ", " + jogo.GetMaxTimes());
}