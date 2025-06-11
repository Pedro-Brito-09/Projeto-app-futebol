using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoOOP.Enums;
using ProjetoOOP.Interfaces;

namespace ProjetoOOP.Models;
public class Jogo : IJogo
{
    private int Id;
    private string Data;
    private string Local;
    private string Campo;
    private int Jogadores;
    private int MaxTimes;
    private List<Time> Times;
    public Jogo(int id, string data, string local, string campo, int jogadores, int maxTimes)
    {
        this.Id = id;
        this.Data = data;
        this.Local = local;
        this.Campo = campo;
        this.Jogadores = jogadores;
        this.MaxTimes = maxTimes;
    }

    public int GetId()
    {
        return this.Id;
    }
    public string GetData()
    {
        return this.Data;
    }

    public string GetLocal()
    {
        return this.Local;
    }

    public string GetCampo()
    {
        return this.Campo;
    }

    public int GetJogadoresPorTime()
    {
        return this.Jogadores;
    }

    public int GetMaxTimes()
    {
        return this.MaxTimes;
    }

    public Time GetTime(int index)
    {
        return this.Times[index];
    }

    public void SetId(int id)
    {
        this.Id = id;
    }
    public void SetData(string data)
    {
        this.Data = data;
    }

    public void SetLocal(string local)
    {
        this.Local = local;
    }

    public void SetCampo(string campo)
    {
        this.Campo = campo;
    }

    public void SetJogadoresPorTime(int jogadores)
    {
        this.Jogadores = jogadores;
    }

    public void SetMaxTimes(int maxTimes)
    {
        this.MaxTimes = maxTimes;
    }

    public void SetTimes(Time time1, Time time2)
    {
        this.Times = new List<Time> { time1, time2 };
    }
}