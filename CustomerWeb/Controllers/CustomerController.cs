using CustomerWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerContext _customerContext;

        public CustomerController(CustomerContext customerContext)
        {
            _customerContext = customerContext;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<IActionResult> GetCustomer()
        {
          var customer = await _customerContext.Customers.ToListAsync();
            return Ok(customer);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerContext.Customers.FindAsync(id);
            return Ok(customer);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Create ([FromBody] Customer customer)
        {
            await _customerContext.AddAsync(customer);
            await _customerContext.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update ([FromBody] Customer customer)
        {
            _customerContext.Customers.Update(customer);
            await _customerContext.SaveChangesAsync();
            return Ok();

        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerContext.Customers.FindAsync(id);
             _customerContext.Customers.Remove(customer);
            await _customerContext.SaveChangesAsync();
            return Ok();
        }
    }
}
