using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Api.Models;

namespace ExpenseTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Ye route banayega: api/Expense
    public class ExpenseController : ControllerBase
    {
        // Static list as a temporary database
        private static readonly List<Expense> Expenses = new List<Expense>
        {
            new Expense { Id = 1, Title = "Azure Subscription", Amount = 1500, Category = "Cloud", Date = DateTime.Now.AddDays(-2) },
            new Expense { Id = 2, Title = "Broadband Bill", Amount = 999, Category = "Utilities", Date = DateTime.Now.AddDays(-1) }
        };

        // GET: api/Expense
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Expenses);
        }

        // POST: api/Expense
        [HttpPost]
        public IActionResult Create(Expense newExpense)
        {
            newExpense.Id = Expenses.Count > 0 ? Expenses.Max(e => e.Id) + 1 : 1;
            Expenses.Add(newExpense);
            
            // Returns 201 Created status
            return CreatedAtAction(nameof(GetAll), new { id = newExpense.Id }, newExpense);
        }
    }
}