using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BurgerManiaServer.Data;
using BurgerManiaServer.Models;

namespace BurgerManiaServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BurgerCartController : ControllerBase
    {
        private readonly BurgerCartContext _context;

        public BurgerCartController(BurgerCartContext context)
        {
            _context = context;
        }

        // GET: api/BurgerCart
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BurgerCart>>> GetBurgerCart()
        {
            return await _context.BurgerCart.ToListAsync();
        }

        // GET: api/BurgerCart/Cheesy/Veg
        [HttpGet("{burger}/{category}")]
        public async Task<ActionResult<BurgerCart>> GetBurgerCart(string burger, string category)
        {
            var burgerCart = await _context.BurgerCart
                .FirstOrDefaultAsync(bc => bc.Burger == burger && bc.Category == category);
            if (burgerCart == null)
            {
                return NotFound();
            }

            return burgerCart;
        }

        // PUT: api/BurgerCart/Cheesy/Veg
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{burger}/{category}")]
        public async Task<IActionResult> PutBurgerCart(string burger, string category, BurgerCart burgerCart)
        {
            try
            {
                burgerCart.TotalPrice = burgerCart.Price * burgerCart.Quantity;
                _context.Entry(burgerCart).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BurgerCartExists(burgerCart.ItemId))
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

        // POST: api/BurgerCart
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BurgerCart>> PostBurgerCart(BurgerCart burgerCart)
        {
            try
            {
                Random rnd = new Random();
                burgerCart.ItemId = rnd.Next(100, 999);

                switch (burgerCart.Category)
                {
                    case "Veg":
                        burgerCart.Price = 100;
                        break;
                    case "Egg":
                        burgerCart.Price = 150;
                        break;
                    case "Chicken":
                        burgerCart.Price = 200;
                        break;
                    default:
                        burgerCart.Price = null;
                        break;
                }

                burgerCart.TotalPrice = Convert.ToInt32(burgerCart.Price) * burgerCart.Quantity;
                Console.WriteLine(burgerCart);
                _context.BurgerCart.Add(burgerCart);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BurgerCartExists(burgerCart.ItemId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBurgerCart", new { id = burgerCart.ItemId }, burgerCart);
        }

        // DELETE: api/BurgerCart/5
        [HttpDelete("{burger}/{category}")]
        public async Task<IActionResult> DeleteBurgerCart(string burger, string category)
        {
            var burgerCart = await _context.BurgerCart
                .FirstOrDefaultAsync(bc => bc.Burger == burger && bc.Category == category);
            if (burgerCart == null)
            {
                return NotFound();
            }

            //var burgerCart = await _context.BurgerCart.FindAsync(item.);
            //if (burgerCart == null)
            //{
            //    return NotFound();
            //}

            _context.BurgerCart.Remove(burgerCart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BurgerCartExists(int id)
        {
            return _context.BurgerCart.Any(e => e.ItemId == id);
        }
    }
}