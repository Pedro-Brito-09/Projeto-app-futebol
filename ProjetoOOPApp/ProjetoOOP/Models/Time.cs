﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoOOP.Models;

public class Time
{
    private string nome;
    private int vitorias;
    private int derrotas;
    private int empates;
    private List<Jogador> jogadores = new List<Jogador>();
    
    public Time(string nome)
    {
        this.nome = nome;
    }

    public void AddJogador(Jogador jogador)
    {
        if (this.jogadores.Contains(jogador))
        {
            Console.WriteLine("O jogador " + jogador.GetNome() + " já está no time " + this.nome);
            return;
        }

        this.jogadores.Add(jogador);
    }

    public void RemoveJogador(Jogador jogador)
    {
        if (!this.jogadores.Contains(jogador))
        {
            Console.WriteLine("O jogador " + jogador.GetNome() + " não está no time " + this.nome);
            return;
        }

        this.jogadores.Remove(jogador);
    }

    public void ListarJogadores()
    {
        if (this.jogadores.Count == 0)
        {
            Console.WriteLine($"  Nenhum jogador no time {this.nome}.");
            return;
        }

        Console.WriteLine($"  Jogadores do time {this.nome}:");
        foreach (var jogador in this.jogadores)
        {
            Console.WriteLine($"  - Id: {jogador.GetId()} | {jogador.GetNome()} | Idade: {jogador.GetIdade()} | Posição: {jogador.GetPosicao()}");
        }
    }

    public void LimparTime()
    {
        this.jogadores.Clear();
    }

    public string GetNome()
    {
        return this.nome;
    }

    public int GetQuantidadeJogadores()
    {
        return this.jogadores.Count;
    }

    public void AddVitoria()
    {
        this.vitorias++;
    }

    public void AddDerrota()
    {
        this.derrotas++;
    }

    public void AddEmpate()
    {
        this.empates++;
    }

    public int GetVitorias()
    {
        return this.vitorias;
    }

    public int GetDerrotas()
    {
        return this.derrotas;
    }

    public int GetEmpates()
    {
        return this.empates;
    }

    public List<Jogador> GetJogadores()
    {
        return this.jogadores;
    }
}
