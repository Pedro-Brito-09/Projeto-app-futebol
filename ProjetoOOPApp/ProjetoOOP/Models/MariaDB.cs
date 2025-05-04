using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using ProjetoOOP.Enums;
using ProjetoOOP.Interfaces;

namespace ProjetoOOP.Models;

public class MariaDB
{
    private MySqlConnection connection = new MySqlConnection("Server=localhost;User Id=root; Password=; Database=erpdobaba");

    public void Connect()
    {
        connection.Open();
    }

    public List<Jogador> GetJogadores()
    {
        string query = "SELECT * FROM jogadores";
        using var cmd = new MySqlCommand(query, connection);
        using var reader = cmd.ExecuteReader();
        List<Jogador> jogadores = new List<Jogador>();

        while (reader.Read())
        {
            Jogador jogador = new Jogador((int)reader["id"], (string)reader["nome"], (int)reader["idade"], (Posicoes)Enum.Parse(typeof(Posicoes), (string)reader["posicao"], true));
            jogadores.Add(jogador);
        }

        return jogadores;
    }
    public void AddJogador(Jogador jogador)
    {
        string query = "INSERT INTO jogadores (nome, idade, posicao) VALUES ('" + jogador.GetNome() + "', '" + jogador.GetIdade() + "', '" + jogador.GetPosicao().ToString() + "') RETURNING id";
        using var cmd = new MySqlCommand(query, connection);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            jogador.SetId((int)reader["id"]);
        }
    }

    public void RemoveJogador(int id)
    {
        string query = "DELETE FROM jogadores WHERE id = " + id;
        using var cmd = new MySqlCommand(query, connection);
        using var reader = cmd.ExecuteReader();

        Console.WriteLine("Jogador removido com sucesso!");
    }

    public List<Jogo> GetJogos()
    {
        string query = "SELECT * FROM jogos";
        using var cmd = new MySqlCommand(query, connection);
        using var reader = cmd.ExecuteReader();
        List<Jogo> jogos = new List<Jogo>();

        while (reader.Read())
        {
            Jogo jogo = new Jogo((int)reader["id"], (string)reader["data"], (string)reader["local"], (string)reader["campo"], (int)reader["jogadores"], (int)reader["maxTimes"]);
            jogos.Add(jogo);
        }

        return jogos;
    }
    public void AddJogo(Jogo jogo)
    {
        string query = "INSERT INTO jogos (data, local, campo, jogadores, maxTimes) VALUES ('" + jogo.GetData() + "', '" + jogo.GetLocal() + "', '" + jogo.GetCampo() + "', '" + jogo.GetJogadoresPorTime() + "', '" + jogo.GetMaxTimes() + "') RETURNING id";
        using var cmd = new MySqlCommand(query, connection);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            jogo.SetId((int)reader["id"]);
        }
    }

    public void RemoveJogo(int id)
    {
        string query = "DELETE FROM jogos WHERE id = " + id;
        using var cmd = new MySqlCommand(query, connection);
        using var reader = cmd.ExecuteReader();

        while (reader.Read()) {}
    }
}
