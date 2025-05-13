using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoOOP.Interfaces;

namespace ProjetoOOP.Models;

public class Partida
{
    public Time TimeA { get; set; }
    public Time TimeB { get; set; }
    public int GolsTimeA { get; set; }
    public int GolsTimeB { get; set; }
    public DateTime Data { get; set; }

    public Time Vencedor => GolsTimeA > GolsTimeB ? TimeA :
                          (GolsTimeB > GolsTimeA ? TimeB : null);

    public Partida(Time timeA, Time timeB)
    {
        TimeA = timeA;
        TimeB = timeB;
        Data = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{TimeA.GetNome()} {GolsTimeA} x {GolsTimeB} {TimeB.} - {Data:dd/MM HH:mm}";
    }
}
