using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMX_api.Models;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace CMX_api.Controllers
{
    public class QuestionsController : ODataController
    {
        private readonly CmxContext _context;

        public QuestionsController(CmxContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
          if (_context.Questions == null)
          {
              return NotFound();
          }
            return await _context.Questions.ToListAsync();
        }

        public async Task<ActionResult<Question>> GetQuestion([FromRoute] int key)
        {
          if (_context.Questions == null)
          {
              return NotFound();
          }
            var question = await _context.Questions.FindAsync(key);

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

        public async Task<IActionResult> PutQuestion([FromRoute]int key, [FromBody]Question question)
        {
            if (key != question.QuestionId)
            {
                return BadRequest();
            }

            _context.Entry(question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(key))
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

        public async Task<ActionResult<Question>> PostQuestion([FromBody]Question question)
        {
          if (_context.Questions == null)
          {
              return Problem("Entity set 'CmxContext.Questions'  is null.");
          }
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestion", new { key = question.QuestionId }, question);
        }

        public async Task<IActionResult> DeleteQuestion([FromRoute] int key)
        {
            if (_context.Questions == null)
            {
                return NotFound();
            }
            var question = await _context.Questions.FindAsync(key);
            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionExists(int id)
        {
            return (_context.Questions?.Any(e => e.QuestionId == id)).GetValueOrDefault();
        }
    }
}
