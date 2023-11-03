using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMX_api.Models;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;

namespace CMX_api.Controllers
{
    public class UsersController : ODataController
    {
        private readonly CmxContext _context;

        public UsersController(CmxContext context)
        {
            _context = context;
        }
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }
        [EnableQuery]
        public async Task<ActionResult<User>> GetUser([FromRoute] int key)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(key);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        public async Task<IActionResult> PutUser([FromRoute] int key, [FromBody] User user)
        {
            if (key != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(key))
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

        public async Task<ActionResult<User>> PostUser([FromBody] User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'CmxContext.Users'  is null.");
          }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { key = user.UserId }, user);
        }

        public async Task<IActionResult> DeleteUser([FromRoute] int key)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(key);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
