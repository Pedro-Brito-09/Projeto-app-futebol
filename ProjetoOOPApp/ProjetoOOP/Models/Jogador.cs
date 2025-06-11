using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoOOP.Enums;
using ProjetoOOP.Interfaces;

namespace ProjetoOOP.Models;
public class Jogador : IJogador
{
    private int Id;
    private string Nome;
    private int Idade;
    private Posicoes Posicao;

    public Jogador(int id, string nome, int idade, Posicoes posicao)
    {
        this.Id = id;
        this.Nome = nome;
        this.Idade = idade;
        this.Posicao = posicao;
    }

    public int GetId()
    {
        return this.Id;
    }
    public string GetNome()
    {
        return this.Nome;
    }

    public int GetIdade()
    {
        return this.Idade;
    }

    public Posicoes GetPosicao()
    {
        return this.Posicao;
    }

    public void SetId(int id)
    {
        this.Id = id;
    }
    public void SetNome(string nome)
    {
        this.Nome = nome;
    }

    public void SetIdade(int idade)
    {
        this.Idade = idade;
    }

    public void SetPosicao(Posicoes posicao)
    {
        this.Posicao = posicao;
    }
}
