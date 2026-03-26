using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Api.Data;
using ExpenseTracker.Api.Models;

namespace ExpenseTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
       
        public ExpenseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var expenses = await _context.Expenses.ToListAsync();
            return Ok(expenses);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Expense newExpense)
        {
            _context.Expenses.Add(newExpense);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetAll), new { id = newExpense.Id }, newExpense);
        }
      
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Expense updatedExpense)
        {
            if (id != updatedExpense.Id)
            {
                return BadRequest("URL ka ID aur Data ka ID match nahi ho raha.");
            }

            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound("Ye Expense database mein nahi mila.");
            }

             expense.Title = updatedExpense.Title;
            expense.Amount = updatedExpense.Amount;
            expense.Date = updatedExpense.Date;

            await _context.SaveChangesAsync();
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound("Ye Expense pehle se hi delete ho chuka hai ya exist nahi karta.");
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return NoContent(); 
        }
    }
}