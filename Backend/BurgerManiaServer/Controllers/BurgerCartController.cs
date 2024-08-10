//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using BurgerManiaServer.Data;
//using BurgerManiaServer.Models;

//namespace BurgerManiaServer.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class OrderController : ControllerBase
//    {
//        private readonly BurgerManiaContext _context;

//        public OrderController(BurgerManiaContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Order
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
//        {
//            return await _context.Order.ToListAsync();
//        }

//        // GET: api/Order/Cheesy/Veg
//        [HttpGet("{burger}/{category}")]
//        public async Task<ActionResult<Order>> GetOrder(string burger, string category)
//        {
//            var Order = await _context.Order
//                .FirstOrDefaultAsync(bc => bc.Burger == burger && bc.Category == category);
//            if (Order == null)
//            {
//                return NotFound();
//            }

//            return Order;
//        }

//        // PUT: api/Order/Cheesy/Veg
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{burger}/{category}")]
//        public async Task<IActionResult> PutOrder(string burger, string category, Order Order)
//        {
//            try
//            {
//                Order.TotalPrice = Order.Price * Order.Quantity;
//                _context.Entry(Order).State = EntityState.Modified;

//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!OrderExists(Order.ItemId))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Order
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Order>> PostOrder(Order Order)
//        {
//            try
//            {
//                Random rnd = new Random();
//                Order.ItemId = rnd.Next(100, 999);

//                switch (Order.Category)
//                {
//                    case "Veg":
//                        Order.Price = 100;
//                        break;
//                    case "Egg":
//                        Order.Price = 150;
//                        break;
//                    case "Chicken":
//                        Order.Price = 200;
//                        break;
//                    default:
//                        Order.Price = null;
//                        break;
//                }

//                Order.TotalPrice = Convert.ToInt32(Order.Price) * Order.Quantity;
//                Console.WriteLine(Order);
//                _context.Order.Add(Order);
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateException)
//            {
//                if (OrderExists(Order.ItemId))
//                {
//                    return Conflict();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return CreatedAtAction("GetOrder", new { id = Order.ItemId }, Order);
//        }

//        // DELETE: api/Order/5
//        [HttpDelete("{burger}/{category}")]
//        public async Task<IActionResult> DeleteOrder(string burger, string category)
//        {
//            var Order = await _context.Order
//                .FirstOrDefaultAsync(bc => bc.Burger == burger && bc.Category == category);
//            if (Order == null)
//            {
//                return NotFound();
//            }

//            //var Order = await _context.Order.FindAsync(item.);
//            //if (Order == null)
//            //{
//            //    return NotFound();
//            //}

//            _context.Order.Remove(Order);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool OrderExists(int id)
//        {
//            return _context.Order.Any(e => e.ItemId == id);
//        }
//    }
//}