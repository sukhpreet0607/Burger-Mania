using BurgerManiaServer.Data;
using BurgerManiaServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace BurgerManiaServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly BurgerManiaContext _context;

        public UserController(BurgerManiaContext context)
        {
            _context = context;
        }

        // GET: api/UserController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Index()
        {
            return await _context.Users.ToListAsync();
        }

        // POST: api/UserController/AddUser
        [HttpPost("AddUser")]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (DbException err)
            {
                throw err;
            }
            return CreatedAtAction("AddUser", new { id = user.UserId }, user);
        }

        [HttpGet("GetUserById")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user==null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch(DbException err)
            {
                throw err;
            }
        }

        [HttpPut("UpdateUserById")]
        public async Task<ActionResult<User>> UpdateUserById(int id,User updatedUser)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user!=null)
                {
                    _context.Users.Entry(user).State = EntityState.Detached;
                    _context.Users.Entry(updatedUser).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return updatedUser;
                }
                else
                {
                    return NotFound();
                }
            }
            catch (DbException err)
            {
                throw err;
            }
        }

        [HttpDelete("RemoveUser")]
        public async Task<ActionResult> RemoveUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }
}
