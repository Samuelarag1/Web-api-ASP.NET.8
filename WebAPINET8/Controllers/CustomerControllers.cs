using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPINET8.Database;
using WebAPINET8.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPINET8.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerControllers : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomerControllers(ApplicationDbContext context)
        {
            _context = context;
        }



        // GET: api/<CustomerControllers>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var customers = await _context.Customers.ToListAsync();
            if (customers == null || customers.Count == 0)
                return NotFound("No customers found.");
            return Ok(customers);
        }


        // GET api/<CustomerControllers>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _context.Customers.FindAsync(id);

            if (result == null)
                return NotFound("No hay usuario en la base de datos");
            return Ok(result);


        }

        // POST api/<CustomerControllers>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            _context.SaveChanges();
            return Ok(customer);
        }

        // PUT api/<CustomerControllers>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
        {
            var customerFind = _context.Customers.SingleOrDefault(x => x.Id == id);
            if(customerFind == null)
                return NotFound();


                customerFind.Email = customer.Email;
                customerFind.Name = customer.Name;
                _context.Attach(customerFind);
                await _context.SaveChangesAsync();


            return Ok(customerFind);
        }

        // DELETE api/<CustomerControllers>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customerFind = _context.Customers.SingleOrDefault(x => x.Id == id);
            if (customerFind == null)
                return NotFound();

            _context.Customers.Remove(customerFind);
            await _context.SaveChangesAsync();

            return Ok(customerFind);
        }
    }
}
