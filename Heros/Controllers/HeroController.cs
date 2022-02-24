using Heros.Data;
using Heros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Heros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly DataContext _context;

        public HeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Hero>>> GetListHeros()
        {
            return Ok(await _context.Heroes.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Hero>> GetHero(int id)
        {
            var hero = await _context.Heroes.FindAsync(id);

            return (hero != null)
                ? Ok(hero)
                : BadRequest("hero with id : " + id + " does not exist");
        }


        [HttpPost]
        public async Task<ActionResult<List<Hero>>> PostHero(Hero hero)
        {
            _context.Heroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.Heroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Hero>>> UpdateHero(Hero request)
        {
            var dbHero = await _context.Heroes.FindAsync(request.Id);

            if (dbHero == null)
                return BadRequest("The hero: " + request.name + " does not exist");

            dbHero.name = request.name;
            await _context.SaveChangesAsync();

            return Ok(await _context.Heroes.ToListAsync());
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<List<Hero>>> DeleteHero(int id)
        {
            var dbHero = await _context.Heroes.FindAsync(id);

            if (dbHero == null)
                return BadRequest("hero with id : " + id + " does not exist");

            _context.Heroes.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.Heroes.ToListAsync());
        }
    }
}