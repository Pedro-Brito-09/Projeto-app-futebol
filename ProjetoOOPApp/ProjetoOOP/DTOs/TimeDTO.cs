namespace ProjetoOOP.DTOs
{
    public class TimeDTO
    {
        public string Nome { get; set; }
        public List<JogadorDTO> Jogadores { get; set; }

        public static TimeDTO FromModel(Models.Time time)
        {
            // Para simplicidade, assumindo que a classe Time tem uma lista de jogadores
            var jogadoresDTO = time.GetType().GetField("jogadores", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .GetValue(time) as List<Models.Jogador>;

            return new TimeDTO
            {
                Nome = time.GetNome(),
                Jogadores = jogadoresDTO?.Select(j => JogadorDTO.FromModel(j)).ToList() ?? new List<JogadorDTO>()
            };
        }

        public Models.Time ToModel()
        {
            // Como o modelo Time não possui construtor com jogadores, devemos criar e adicionar os jogadores posteriormente
            var time = new Models.Time(Nome);
            if (Jogadores != null)
            {
                foreach (var jogadorDTO in Jogadores)
                {
                    var jogador = jogadorDTO.ToModel();
                    time.AddJogador(jogador);
                }
            }
            return time;
        }
    }
}
