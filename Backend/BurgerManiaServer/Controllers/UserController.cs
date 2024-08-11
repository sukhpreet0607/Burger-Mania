using BurgerManiaServer.Data;
using BurgerManiaServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;

namespace BurgerManiaServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly BurgerManiaContext _context;

        public UserController(BurgerManiaContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Index()
        {
            return await _context.Users.ToListAsync();
        }

        // POST: api/User/AddUser
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

        // GET: api/User/GetUser
        [HttpGet("GetUser/{username}/{password}")]
        public async Task<IActionResult> GetUser(string username, string password)
        {
            try
            {
                var user = await _context.Users.
                    FirstOrDefaultAsync(u => u.UserName == username && u.UserPassword == password);
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

        // PUT: api/User/UpdateUserById
        [HttpPut("UpdateUserById")]
        public async Task<IActionResult> UpdateUserById(int id,User updatedUser)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user!=null)
                {
                    _context.Users.Entry(user).State = EntityState.Detached;
                    _context.Users.Entry(updatedUser).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok(updatedUser);
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

        // DEL: api/User/RemoveUserById
        [HttpDelete("RemoveUserById")]
        public async Task<IActionResult> RemoveUserById(int id)
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
