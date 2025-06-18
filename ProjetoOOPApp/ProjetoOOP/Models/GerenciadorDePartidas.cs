using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoOOP.Enums;

namespace ProjetoOOP.Models;

public class GerenciadorPartidas
{
    private List<Time> timesDisponiveis = new List<Time>();
    private List<Partida> historicoPartidas = new List<Partida>();
    private ModosDePartida modoAtual;
    private Partida partidaAtual;
    private Time ultimoVencedor = null;
    private Dictionary<Time, int> jogosPorTime = new Dictionary<Time, int>();

    public void AdicionarTime(Time time)
    {
        timesDisponiveis.Add(time);
        jogosPorTime[time] = 0;
    }

    public void DefinirModoPartida(ModosDePartida modo)
    {
        modoAtual = modo;
        ultimoVencedor = null;
    }

    public void IniciarNovaPartida(MariaDB Database)
    {
        if (timesDisponiveis.Count < 2)
            throw new InvalidOperationException("Não há times suficientes para iniciar uma partida");

        Time timeA, timeB;

        if (modoAtual == ModosDePartida.GANHA_FICA)
        {
            timeA = ultimoVencedor ?? ObterTimeComMenosJogos();
            timeB = ObterTimeComMenosJogos(excluir: timeA);
        }
        else
        {
            if (ultimoVencedor == null)
            {
                timeA = ObterTimeComMenosJogos();
                timeB = ObterTimeComMenosJogos(excluir: timeA);
            }
            else
            {
                timeA = ultimoVencedor;
                timeB = ObterTimeComMenosJogos(excluir: timeA);
            }
        }

        timeA.LimparTime();
        timeB.LimparTime();

        PreencherTime(timeA, Database.GetJogadoresDisponiveis(timesDisponiveis));
        PreencherTime(timeB, Database.GetJogadoresDisponiveis(timesDisponiveis));

        partidaAtual = new Partida(timeA, timeB);
        Console.WriteLine($"Nova partida criada: {timeA.GetNome()} vs {timeB.GetNome()}");

        timeA.ListarJogadores();
        Console.WriteLine();
        timeB.ListarJogadores();
    }

    private void PreencherTime(Time time, List<Jogador> jogadoresDisponiveis)
    {
        foreach (Jogador jogador in jogadoresDisponiveis)
        {
            if (jogador.GetPosicao() == Posicoes.GOLEIRO)
            {
                time.AddJogador(jogador);
                break;
            }
        }

        int jogadoresPorTime = 5;
        int jogadoresAdicionados = time.GetQuantidadeJogadores();

        foreach (Jogador jogador in jogadoresDisponiveis)
        {
            if (jogadoresAdicionados >= jogadoresPorTime)
            {
                Console.WriteLine("CHEIO");
                break;
            }
            
            if (jogador.GetPosicao() != Posicoes.GOLEIRO && !time.GetJogadores().Contains(jogador))
            {
                Console.WriteLine("ADICIONANDO");
                time.AddJogador(jogador);
                jogadoresAdicionados++;
            }
        }
    }

    public void RegistrarResultado(int golsTimeA, int golsTimeB)
    {
        if (partidaAtual == null)
            throw new InvalidOperationException("Nenhuma partida em andamento");

        partidaAtual.GolsTimeA = golsTimeA;
        partidaAtual.GolsTimeB = golsTimeB;

        if (partidaAtual.Vencedor != null)
        {
            partidaAtual.Vencedor.AddVitoria();
            Time perdedor = partidaAtual.TimeA == partidaAtual.Vencedor ?
                            partidaAtual.TimeB : partidaAtual.TimeA;
            perdedor.AddDerrota();
        }
        else
        {
            partidaAtual.TimeA.AddEmpate();
            partidaAtual.TimeB.AddEmpate();
        }

        jogosPorTime[partidaAtual.TimeA]++;
        jogosPorTime[partidaAtual.TimeB]++;

        historicoPartidas.Add(partidaAtual);
        ultimoVencedor = partidaAtual.Vencedor;

        Console.WriteLine($"Resultado registrado: {partidaAtual}");
        partidaAtual = null;
    }

    private Time ObterTimeComMenosJogos(Time excluir = null)
    {
        var candidatos = timesDisponiveis.Where(t => excluir == null || t != excluir).ToList();

        if (!candidatos.Any())
            throw new InvalidOperationException("Não há times disponíveis");

        // Ordena por quantidade de jogos e pega o primeiro
        return candidatos.OrderBy(t => jogosPorTime[t]).First();
    }

    public void ExibirHistorico()
    {
        Console.WriteLine("\n=== Histórico de Partidas ===");
        foreach (var partida in historicoPartidas)
        {
            Console.WriteLine(partida);
            Console.WriteLine($"  Vencedor: {(partida.Vencedor?.GetNome() ?? "Empate")}");
        }
    }

    public void ExibirEstatisticasTimes()
    {
        Console.WriteLine("\n=== Estatísticas dos Times ===");
        foreach (var time in timesDisponiveis.OrderByDescending(t => t.GetVitorias()))
        {
            Console.WriteLine($"{time.GetNome()}: {time.GetVitorias()}V/{time.GetDerrotas()}D/{time.GetEmpates()}E");
        }
    }

    public List<Time> GetTimes()
    {
        return this.timesDisponiveis;
    }

    public void RemoverTime(string nome)
    {
        List<Time> timesParaRemover = new List<Time>();

        foreach (Time time in timesDisponiveis)
        {
            if (time.GetNome() == nome)
            {
                timesParaRemover.Add(time);
            }
        }

        foreach (Time time in timesParaRemover)
        {
            timesDisponiveis.Remove(time);
            jogosPorTime.Remove(time);
        }
    }

    public Partida PartidaAtual => partidaAtual;
    public IReadOnlyList<Partida> HistoricoPartidas => historicoPartidas.AsReadOnly();
}