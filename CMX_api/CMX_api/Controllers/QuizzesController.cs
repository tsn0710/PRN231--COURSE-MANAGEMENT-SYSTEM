using CMX_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMX_api.Controllers
{
    public class QuizzesController : ODataController
    {
        private readonly CmxContext _context;

        public QuizzesController(CmxContext context)
        {
            _context = context;
        }

        [HttpGet("odata/Quizzes/GetQuizzes")]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<QuizDTO>>> GetQuizsAsync()
        {
            if (_context.Quizzes == null)
            {
                return NotFound();
            }
            var a = await _context.Quizzes.Include("Course").Include("Questions").Include("QuizAttendances").Include("QuizAttendances.Student").ToListAsync();
            List<QuizDTO> quizzes = new List<QuizDTO>();
            foreach(var item in a) { 
                quizzes.Add(item.GetQuizDTO());
            }
            return Ok(quizzes);
        }
        [HttpGet("odata/Quizzes/GetQuizByKey/{key}")]
        public async Task<ActionResult<QuizDTO>> GetQuizAsync([FromRoute] int key)
        {
            if (_context.Quizzes == null)
            {
                return NotFound();
            }
            var quiz = await _context.Quizzes.Include("Course").Include("Questions").Include("QuizAttendances").Include("QuizAttendances.Student").Where(o => o.QuizId == key).FirstAsync();

            if (quiz == null)
            {
                return NotFound();
            }

            return quiz.GetQuizDTO();
        }
        [HttpPost("odata/Quiz/AddQuiz")]
        public async Task<ActionResult<Quiz>> PostQuizAsync([FromBody] Quiz quiz)
        {
            if (_context.Quizzes == null)
            {
                return Problem("Entity set 'eStoreContext.Orders'  is null.");
            }

            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetQuiz", new { key = quiz.QuizId }, quiz);
        }
        [HttpPut("odata/Quiz/EditQuiz/{key}")]
        public async Task<IActionResult> PutQuizAsync([FromRoute] int key, [FromBody] Quiz quiz)
        {
            if (key != quiz.QuizId)
            {
                return BadRequest();
            }

            _context.Entry(quiz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizExists(key))
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
        [HttpDelete("odata/Quiz/DeleteQuiz/{key}")]
        public async Task<IActionResult> DeleteQuizAsync([FromRoute] int key)
        {
            if (_context.Quizzes == null)
            {
                return NotFound();
            }
            Quiz quiz = await _context.Quizzes.Include("Questions").Include("QuizAttendances").Where(o=>o.QuizId==key).FirstAsync();
            if (quiz == null)
            {
                return NotFound();
            }
            _context.Questions.RemoveRange(quiz.Questions);
            _context.QuizAttendances.RemoveRange(quiz.QuizAttendances);
                _context.Quizzes.Remove(quiz);
                await _context.SaveChangesAsync();
           

            return NoContent();
        }
        private bool QuizExists(int id)
        {
            return (_context.Quizzes?.Any(e => e.QuizId == id)).GetValueOrDefault();
        }
        [HttpPost("odata/Quiz/SubmitQuiz")]
        public async Task<ActionResult<Quiz>> SubmitQuizAsync([FromBody] List<String> b)
        {
            List<String> a = await _context.Questions.Where(q => q.QuizId==Int32.Parse(b[1])).OrderBy(a=>a.QuestionId).Select(a=>a.CorrectOption).ToListAsync();
            if (a.Count != b.Count - 2)
            {
                return BadRequest("lech so luong cau tra loi");
            }
            int NumberOfCorrectAnswer = 0;
            for(int i = 0; i < a.Count; i++)
            {
                if (a[i].Equals(b[i + 2]))
                {
                    NumberOfCorrectAnswer++;
                }
            }
            var qa = _context.QuizAttendances.Where(a => a.QuizId == Int32.Parse(b[1]) && a.StudentId == Int32.Parse(b[0])).FirstOrDefault();
            if (qa == null)
            {
                _context.QuizAttendances.Add(new QuizAttendance() { QuizId = Int32.Parse(b[1]), StudentId = Int32.Parse(b[0]), Score = NumberOfCorrectAnswer });
                await _context.SaveChangesAsync();
                return Ok("added");
            }
            else
            {
                qa.Score = NumberOfCorrectAnswer;
                _context.Entry(qa).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("edited");
            }
            
        }
    }
}
