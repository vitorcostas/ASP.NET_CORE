

using LivroShop.Dados.Contexto;
using LivroShop.Modelos;
using LivroShop.Servicos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LivroShop.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LivrosController : ControllerBase
    {
        #region Campos

        private readonly LivroBdContexto contexto;
        #endregion

        #region Construtor
        public LivrosController(LivroBdContexto contexto)
        {
            this.contexto = contexto;
        }
        #endregion

        #region Metodos
        [HttpGet]
        [Authorize(Roles = RolesUsuario.Admin)]
        public async Task<IActionResult> Get()
        {
            var livros = await contexto.Livros.ToListAsync();
            return Ok(livros);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }
            var livro = await contexto.Livros.FindAsync(id);

            if (livro == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(livro);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Livro livro)
        {
            contexto.Livros.Add(livro);

            await contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudentById), new { id = livro.Id }, livro );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromRoute]int id, Livro livro)
        {
            if (id != livro.Id)
            {
                return BadRequest();
            }
            if (!VerifyLivroId(livro.Id))
            {
                return NotFound();
            }

            contexto.Entry(livro).State = EntityState.Modified;

            await contexto.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var livro = await contexto.Livros.FindAsync(id);

            if (!VerifyLivroId(id))
            {
                return NotFound();
            }

            contexto.Livros.Remove(livro);

            await contexto.SaveChangesAsync();

            return NoContent();
        }

        private bool VerifyLivroId(int id)
        {
            return contexto.Livros.Any(livro => livro.Id == id);
        }
        #endregion
    }
}
