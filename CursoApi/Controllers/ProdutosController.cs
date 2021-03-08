using CursoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CursoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly Context _context;

        public ProdutosController(Context context)
        {
            _context = context;
        }
        // GET: api/<ProdutosController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return await _context.Produto.Include("Categoria").ToListAsync();
        }

        // GET api/<ProdutosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProdutos(int id)
        {
            var produto = await _context.Produto.Include("Categoria").FirstOrDefaultAsync(x=> x.Id == id);
            if (produto == null)
            {
                return NotFound();
            }
            return produto;
        }

        // POST api/<ProdutosController>
        [HttpPost]
        public async Task<ActionResult<Produto>> PostCategoria(Produto produto)
        {
            _context.Produto.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduto", new { id = produto.Id }, produto);
        }

        // PUT api/<ProdutosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();

            }
            _context.Entry(produto).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Produto.Any(e => e.Id == id);
        }

        // DELETE api/<ProdutosController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Produto>> DeleteProduto(int id)
        {
            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
            {
                return NotFound();

            }
            _context.Produto.Remove(produto);
            await _context.SaveChangesAsync();

            return produto;
        }
    }
}
