﻿using CursoMVC.Models;
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
    public class CategoriasController : ControllerBase
    {

        private readonly Context _context;

        public CategoriasController(Context context)
        {
            _context = context;
        }

        // GET: api/<CategoriasController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()        {

            return  await _context.Categoria.ToListAsync();
        }

        // GET api/<CategoriasController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria =  await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        } 

        // POST api/<CategoriasController>
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            _context.Categoria.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoria", new { id = categoria.Id }, categoria);
        }

        // PUT api/<CategoriasController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult>  PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return BadRequest();

            }
            _context.Entry(categoria).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
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

        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e=> e.Id == id);
        }

        // DELETE api/<CategoriasController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> DeleteCategoria(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();

            }
            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();

            return categoria;
        }
    }
}
