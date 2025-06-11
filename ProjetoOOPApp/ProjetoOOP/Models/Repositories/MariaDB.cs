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
