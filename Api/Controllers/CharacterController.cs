#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Strago.Data;
using Strago.Models;
using Strago.Extensions;
namespace Strago.Api.Controllers;

[Route("api")]
[ApiController]
public class CharacterController : ControllerBase
{
    private readonly StragoDbContext _context;

    public CharacterController(StragoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Character>>> GetCharacters()
    {
        return await _context.Characters
                                .Include(x => x.Experience)
                                .ThenInclude(x => x.Skills).ToListAsync();
    }

    [HttpGet("{characterName}")]
    public async Task<ActionResult<Character>> GetCharacter(string characterName)
    {
        var character = await _context.Characters
                                        .Include(x => x.Experience)
                                        .ThenInclude(x => x.Skills)
                                        .Where(x => x.Name == characterName).FirstOrDefaultAsync();

        if (character == null)
        {
            return NotFound();
        }

        return character;
    }

    [HttpPut("{characterName}")]
    public async Task<IActionResult> PutCharacter([FromBody] string expString, string characterName, string date)
    {
        if (date == null) 
        {
            date = DateTime.Now.ToString("yyyy-MM-dd");
        }

        var circleString = expString.Substring(0, 11);
        circleString = circleString.Split(": ")[1];
        var circle = Int32.Parse(circleString);

        var characterLookup = await _context.Characters.Where(c => c.Name == characterName).FirstOrDefaultAsync();
        if (characterLookup != null) 
        {
            characterLookup.Circle = circle;
            _context.SaveChanges();

            var experience = await _context.Experience.FindAsync(characterLookup.Id);
            List<Skill> skills = new List<Skill>();
            var expArray = ApplicationExtensions.GetExpArray(expString);

            foreach(var exp in expArray)
            {
                if (exp.Length > 0)
                {
                    Skill skill = new Skill();
                    skill.ExperienceId = experience.Id;
                    skill.Name = exp.Split(":")[0];
                    skill.Rank = Int32.Parse(exp.Split(":")[1]);
                    skill.DateLogged = date;
                    skills.Add(skill);
                }
            }

            _context.Skills.AddRange(skills);
            await _context.SaveChangesAsync();

            return Ok("Yep.");
        }
        else
        {
            return BadRequest("Nope.");
        }
    }

    [HttpPost("{characterName}")]
    public async Task<ActionResult<string>> PostCharacter([FromBody] string expString, string characterName, string date)
    {    
        if (date == null) 
        {
            date = DateTime.Now.ToString("yyyy-MM-dd");
        }

        Character character = new Character();
        Experience experience = new Experience();
        List<Skill> skills = new List<Skill>();
        var circleString = expString.Substring(0, 11);
        circleString = circleString.Split(": ")[1];
        var circle = Int32.Parse(circleString);

        var expArray = ApplicationExtensions.GetExpArray(expString);

        var characterLookup = await _context.Characters.Where(c => c.Name == characterName).FirstOrDefaultAsync();

        if (characterLookup == null) 
        {
            character.Name = characterName;
            character.Circle = circle;
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            experience.CharacterId = character.Id;
            _context.Experience.Add(experience);
            await _context.SaveChangesAsync();

            foreach(var exp in expArray)
            {
                if (exp.Length > 0)
                {
                    Skill skill = new Skill();
                    skill.ExperienceId = experience.Id;
                    skill.Name = exp.Split(":")[0];
                    skill.Rank = Int32.Parse(exp.Split(":")[1]);
                    skill.DateLogged = date;
                    skills.Add(skill);
                }
            }

            _context.Skills.AddRange(skills);
            await _context.SaveChangesAsync();

            return Ok("Yep.");
        }
        else
        {
            return BadRequest("Nope.");
        }
    }

    [HttpDelete("{characterName}")]
    public async Task<IActionResult> DeleteCharacter(string characterName)
    {
        var character = await _context.Characters.Where(c => c.Name == characterName).FirstOrDefaultAsync();
        if (character == null)
        {
            return NotFound();
        }

        _context.Characters.Remove(character);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}