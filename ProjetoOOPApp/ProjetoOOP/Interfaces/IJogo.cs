using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoOOP.Enums;
using ProjetoOOP.Models;

namespace ProjetoOOP.Interfaces;
public interface IJogo
{
    public int GetId();
    public string GetData();
    public string GetLocal();
    public string GetCampo();
    public int GetJogadoresPorTime();
    public int GetMaxTimes();
    public Time GetTime(int index);
    public void SetId(int id);
    public void SetData(string data);
    public void SetLocal(string local);
    public void SetCampo(string campo);
    public void SetJogadoresPorTime(int jogadores);
    public void SetMaxTimes(int maxTimes);
    public void SetTimes(Time time1, Time time2);
}
