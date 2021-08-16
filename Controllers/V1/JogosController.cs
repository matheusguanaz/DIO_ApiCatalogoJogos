using ApiCatalogoJogos.Exceptions;
using ApiCatalogoJogos.InputModels;
using ApiCatalogoJogos.Services;
using ApiCatalogoJogos.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IJogoService jogoService;

        public JogosController(IJogoService jogoService)
        {
            this.jogoService = jogoService;
        }
        /// <summary>
        /// Busca todos os jogos de forma paginada
        /// </summary>
        /// <param name="pagina"> Indica qual a página está sendo consultada, minimo 1</param>
        /// <param name="quantidade">indica a quantidade máxima que irá aparecer por página, min 1, max 50 </param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var jogos = await jogoService.Obter(pagina, quantidade);

            if (jogos.Count() == 0)
                return NoContent();

            return Ok(jogos);
        }
        /// <summary>
        /// Busca um único jogo através do id
        /// </summary>
        /// <param name="idJogo">Guid que representa o jogo</param>
        /// <returns></returns>
        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid idJogo)
        {
            var jogo = await jogoService.Obter(idJogo);

            if (jogo == null)
                return NoContent();

            return Ok(jogo);
        }

        /// <summary>
        /// Insere um jogo no catálogo
        /// </summary>
        /// <param name="jogoInputModel">um JSON que representa o jogo, contendo nome, produtora e preço</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                var jogo = await jogoService.Inserir(jogoInputModel);

                return Ok(jogo);
            }
            catch(JogoJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }

        }
        /// <summary>
        /// Alterar um jogo no catálogo
        /// </summary>
        /// <param name="idJogo"></param>
        /// <param name="jogoInputModel"></param>
        /// <returns></returns>
        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute]Guid idJogo, [FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                await jogoService.Atualizar(idJogo, jogoInputModel);
                return Ok();
            }
            catch(JogoNaoCadastradoException ex)
            {
                return NotFound("Este jogo não existe em nossa base");
            }
        }
        /// <summary>
        /// Alterar apenas o preço de um jogo
        /// </summary>
        /// <param name="idJogo"></param>
        /// <param name="preco"></param>
        /// <returns></returns>
        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute]Guid idJogo,[FromRoute] double preco)
        {
            try
            {
                await jogoService.Atualizar(idJogo, preco);
                return Ok();
            }
            catch(JogoNaoCadastradoException ex)
            {
                return NotFound("Este jogo não consta em nossa base");
            }
        }
        /// <summary>
        /// Remover um jogo do catálogo
        /// </summary>
        /// <param name="idJogo"></param>
        /// <returns></returns>
        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> ExcluirJogo(Guid idJogo)
        {
            try
            {
                await jogoService.Excluir(idJogo);
                return Ok();
            }
            catch(JogoNaoCadastradoException ex)
            {
                return NotFound("Este jogo não consta em nossa base");
            }
        }



    }
}
