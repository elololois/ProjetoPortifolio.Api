using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoPortifolio.Api.Data;
using ProjetoPortifolio.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjetoPortifolio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunosController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CriarAlunos(Aluno alunos)
        {
            var novoAluno = new Aluno
            {
                Nome = alunos.Nome,
                Email = alunos.Email,
                Turma = alunos.Turma,
                Professor = alunos.Professor,
            };
             _context.Alunos.Add(novoAluno);
            await _context.SaveChangesAsync();

            return Ok("Turma criada com sucesso!");
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
        {
            var ListaAlunos = await _context.Alunos.Select(a => new Aluno
            {
                Nome = a.Nome,
                Email = a.Email,
                Turma = a.Turma,
                Professor = a.Professor,
            }).ToListAsync();
            return Ok(ListaAlunos);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAluno(int id, Aluno aluno)
        {
            var alunoExistente = await _context.Alunos.FindAsync(id);
            if (alunoExistente == null)
            {
                return NotFound();
            }

            alunoExistente.Nome = aluno.Nome;
            alunoExistente.Email = aluno.Email;
            alunoExistente.Turma = aluno.Turma;
            alunoExistente.Professor = aluno.Professor;

            await _context.SaveChangesAsync();

            return Ok("Aluno atualizado com sucesso!");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirAluno(int id)
        {
            var alunoExistente = await _context.Alunos.FindAsync(id);
            if (alunoExistente == null)
            {
                return NotFound();
            }

            _context.Alunos.Remove(alunoExistente);
            await _context.SaveChangesAsync();

            return Ok("Aluno excluído com sucesso!");
        }
    }
}
