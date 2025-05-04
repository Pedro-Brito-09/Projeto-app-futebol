using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoOOP.Models;

internal class Time
{
    private string nome { get; set; }
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


}
