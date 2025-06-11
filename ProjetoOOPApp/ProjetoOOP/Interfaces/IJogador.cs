using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoOOP.Enums;

namespace ProjetoOOP.Interfaces;
public interface IJogador
{
    public int GetId();
    public string GetNome();
    public int GetIdade();
    public Posicoes GetPosicao();
    public void SetId(int id);
    public void SetNome(string nome);
    public void SetIdade(int idade);
    public void SetPosicao(Posicoes posicao);
}
