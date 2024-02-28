using GCLab17_TacoAPI_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GCLab17_TacoAPI_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TacosController : ControllerBase
    {
        private FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();

        [HttpGet()]
        public IActionResult GetAll(bool? SoftShell = null) 
        {
            List<Taco> result = dbContext.Tacos.ToList();
            if (SoftShell != null)
            {
                result = result.Where(x => x.SoftShell == SoftShell).ToList();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
        {
            Taco result = dbContext.Tacos.Find(id);
            if (result == null)
            {
                return NotFound("Taco not found.");
            }
            return Ok(result);
        }
        [HttpPost()]
        public IActionResult AddTaco([FromBody] Taco newTaco)
        {
            newTaco.Id = 0;
            dbContext.Tacos.Add(newTaco);
            dbContext.SaveChanges();
            return Created($"/api/Books/{newTaco.Id}", newTaco);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTaco(int id)
        {
            Taco taco = dbContext.Tacos.Find(id);
            if (taco == null)
            {
                return NotFound();
            }
            dbContext.Tacos.Remove(taco);
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}
