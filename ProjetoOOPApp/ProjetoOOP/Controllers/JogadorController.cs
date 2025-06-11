using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ProjetoOOP.DTOs;
using ProjetoOOP.Models;
using ProjetoOOP.Repositories;

namespace ProjetoOOP.Controllers
{
    [ApiController]
    [Route("api/jogadores")]
    public class JogadorController : ControllerBase
    {
        private readonly MariaDBRepository _repository;

        public JogadorController()
        {
            _repository = new MariaDBRepository();
            _repository.Connect();
        }

        [HttpGet]
        public ActionResult<List<JogadorDTO>> GetAll()
        {
            var jogadores = _repository.GetJogadores()
                                       .Select(JogadorDTO.FromModel)
                                       .ToList();
            return Ok(jogadores);
        }

        [HttpPost]
        public ActionResult<JogadorDTO> Create([FromBody] JogadorDTO dto)
        {
            // Aqui você pode chamar validações adicionais se desejar
            var model = dto.ToModel();
            _repository.AddJogador(model);
            return CreatedAtAction(nameof(GetAll), JogadorDTO.FromModel(model));
        }
    }
}
