using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nest;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElasticSearchDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IElasticClient _elasticClient;

        public UsersController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        
        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _elasticClient.SearchAsync<User>(s=>s
                .Index("users")
                .Query(q=>q.Term(t=>t.Name, id) || 
                          q.Match(m=>m.Field(f=>f.Name).Query(id))));

            return Ok(response.Documents.FirstOrDefault());
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User value)
        {
            var response = await _elasticClient.IndexAsync<User>(value, x => x.Index("users"));

            return Ok(response.Id);
        }

        
    }
}
