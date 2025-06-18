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

        string query = "USE erpdobaba; CREATE TABLE IF NOT EXISTS jogadores (id INT AUTO_INCREMENT PRIMARY KEY, nome VARCHAR(100), idade INT, posicao VARCHAR(20)); CREATE TABLE IF NOT EXISTS jogos (id INT AUTO_INCREMENT PRIMARY KEY, data VARCHAR(20), local VARCHAR(100), campo VARCHAR(50), jogadores INT, maxTimes INT);";
        using var cmd = new MySqlCommand(query, connection);
        using var reader = cmd.ExecuteReader();
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

    public void EditarJogador(int id, Jogador jogador)
    {
        string query = "UPDATE jogadores SET nome = '" + jogador.GetNome() + "', idade = " + jogador.GetIdade() + ", posicao = '" + jogador.GetPosicao().ToString() + "' WHERE id = " + id;
        using var cmd = new MySqlCommand(query, connection);
        using var reader = cmd.ExecuteReader();
    }

    public void RemoveJogador(int id)
    {
        string query = "DELETE FROM jogadores WHERE id = " + id;
        using var cmd = new MySqlCommand(query, connection);
        using var reader = cmd.ExecuteReader();
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

    public List<Jogador> GetJogadoresDisponiveis(List<Time> todosTimes)
    {
        List<Jogador> todosJogadores = GetJogadores();
        List<Jogador> jogadoresEmTimes = new List<Jogador>();

        foreach (Time time in todosTimes)
        {
            foreach (Jogador jogador in time.GetJogadores())
            {
                jogadoresEmTimes.Add(jogador);
            }
        }

        List<Jogador> jogadoresDisponiveis = new List<Jogador>();

        foreach (Jogador jogador in todosJogadores)
        {
            bool estaEmTime = false;
            foreach (Jogador jogadorTime in jogadoresEmTimes)
            {
                if (jogador.GetId() == jogadorTime.GetId())
                {
                    estaEmTime = true;
                    break;
                }
            }

            if (!estaEmTime)
            {
                Console.WriteLine(jogador.GetId());
                jogadoresDisponiveis.Add(jogador);
            }
        }

        return jogadoresDisponiveis;
    }
}
