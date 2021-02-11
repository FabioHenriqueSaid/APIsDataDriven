using ApiDataDriven.Api.Context;
using ApiDataDriven.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiDataDriven.Api.Controllers
{
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
        {
            var categories = await context.Categories.AsNoTracking().ToListAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> GetById(int id, [FromServices] DataContext context) 
        {
            var categories = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Ok(categories);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post( [FromServices] DataContext context, [FromBody] Category model)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            try
            {
                context.Categories.Add(model);
                await context.SaveChangesAsync();
                return Ok(new { message = "Categoria criada"});
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Não foi possível criar a categoria", e });

            }
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Category>>> Put(int id, [FromBody] Category model, [FromServices] DataContext context) 
        {
            try 
            {
                context.Entry<Category>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException) 
            {
                return BadRequest(new { message = "Este registro já foi atualizado"});
            }
            catch (Exception) 
            {
                return BadRequest(new { message = "Não foi possível atualizar" });  
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Category>>> Delete(
            int id,
            [FromServices] DataContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync( x => x.Id == id);
            if (category == null)
                return NotFound(new { message = "Categoria não encontrada" });

            try
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return Ok(new  { message = "Categoria removida !!! "});
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover a categoria" });
            }
        }
    }
}