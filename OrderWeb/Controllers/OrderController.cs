using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OrderWeb.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMongoCollection<Order> _orderCollection;

        public OrderController()
        {
            var dbhost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbname = Environment.GetEnvironmentVariable("DB_NAME"); ;
            var connectionstring = $"mongodb://{dbhost}:27017/{dbname}";

            var mongoUrl = MongoUrl.Create(connectionstring);
            var mongoClient = new MongoClient(mongoUrl);
            var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            _orderCollection = database.GetCollection<Order>("order");
        }

        // GET: api/<OrderController>
        [HttpGet]
        
        public async Task<IActionResult> GetAllOrders()
        {
            var order =  await _orderCollection.Find(_ => true).ToListAsync();
            return Ok(order);
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
       
        public async Task<IActionResult> GetById(string id)
        {
            var order = await _orderCollection.Find(a => a.Id == id).FirstOrDefaultAsync();
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
             await _orderCollection.InsertOneAsync(order);
            return Ok();
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpadteOrder(string id,[FromBody] Order order)
        {
            //var filter = Builders<Order>.Filter.Eq(x => x.Id, order.Id);
            await _orderCollection.ReplaceOneAsync(x=> x.Id == id, order);
            return Ok();
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            await _orderCollection.DeleteOneAsync(x => x.Id == id);
            return Ok();
        }

    }
}
