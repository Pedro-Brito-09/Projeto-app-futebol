namespace ProjetoOOP.DTOs
{
    public class JogadorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Posicao { get; set; }

        public static JogadorDTO FromModel(Models.Jogador model)
        {
            return new JogadorDTO
            {
                Id = model.GetId(),
                Nome = model.GetNome(),
                Idade = model.GetIdade(),
                Posicao = model.GetPosicao()
            };
        }

        public Models.Jogador ToModel()
        {
            return new Models.Jogador(Nome, Idade, Posicao);
        }
    }
}

