using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoPortifolio.Api.Data;
using ProjetoPortifolio.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjetoPortifolio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjetosController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CriarProjetos(Projeto projeto)
        {
            var novoProjeto = new Projeto
            {
                NomeAluno = projeto.NomeAluno,
                Titulo = projeto.Titulo,
                Descricao = projeto.Descricao,
               TecnologiaUtilizada = projeto.TecnologiaUtilizada
            };

            _context.Projetos.Add(novoProjeto);
            await _context.SaveChangesAsync();

            return Ok("Turma criada com sucesso!");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turma>>> GetTurmas(Turma Turma)
        {
            var ListaTurmas = await _context.Turmas.Select(a => new Turma
            {
                Aluno = Turma.Aluno,
                Professor = Turma.Professor,
                Projeto = Turma.Projeto,
            }).ToListAsync();
            return Ok(ListaTurmas);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarProjeto(int id, Projeto projeto)
        {
            var Projeto = await _context.Projetos.FindAsync(id);
            if (projeto == null)
            {
                return NotFound();
            }

           projeto.NomeAluno = projeto.NomeAluno;
            projeto.Titulo = projeto.Titulo;
            projeto.Descricao = projeto.Descricao;
            projeto.TecnologiaUtilizada = projeto.TecnologiaUtilizada;
            projeto.LinkAcesso = projeto.LinkAcesso;
            projeto.Imagem = projeto.Imagem;
            projeto.Video = projeto.Video;
            projeto.AlunoId = projeto.AlunoId;

            await _context.SaveChangesAsync();

            return Ok("Projeto atualizado com sucesso!");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirProjeto(int id)
        {
            var projeto = await _context.Projetos.FindAsync(id);
            if (projeto == null)
            {
                return NotFound();
            }

            _context.Projetos.Remove(projeto);
            await _context.SaveChangesAsync();

            return Ok("Projeto excluído com sucesso!");
        }

    }
}
