using GCLab17_TacoAPI_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GCLab17_TacoAPI_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        private FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();

        [HttpGet()]
        public IActionResult GetAll(string? sort = null)
        {
            List<Drink> result = dbContext.Drinks.ToList();
            if (sort != null)
            {
               if(sort.ToLower().Trim() == "ascending")
                {
                    result = result.OrderBy(d => d.Cost).ToList();
                }
               else if (sort.ToLower().Trim() == "descending")
                {
                    result = result.OrderByDescending(d => d.Cost).ToList();
                }
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Drink result = dbContext.Drinks.Find(id);
            if (result == null)
            {
                return NotFound("Drink not found.");
            }
            return Ok(result);
        }

        [HttpPost()]
        public IActionResult AddDrink([FromBody] Drink newDrink)
        {
            newDrink.Id = 0;
            dbContext.Drinks.Add(newDrink);
            dbContext.SaveChanges();
            return Created($"/api/Books/{newDrink.Id}", newDrink);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDrink([FromBody] Drink targetDrink, int id)
        {
            if (id != targetDrink.Id) { return BadRequest(); }
            if (!dbContext.Drinks.Any(b => b.Id == id)) { return NotFound(); }
            dbContext.Drinks.Update(targetDrink);
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}
