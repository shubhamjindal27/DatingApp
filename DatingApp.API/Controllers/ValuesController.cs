using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Controllers
{
	//http:localhost:5000/api/values
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		private readonly AppDbContext _context;

		public ValuesController(AppDbContext context)
		{
			_context = context;
		}

		// GET api/values
		[AllowAnonymous]
		[HttpGet]
		//Asynchronous code
		public async Task<IActionResult> GetValues()
		//public IActionResult GetValues() //Synchronous code
		{
			//var values = _context.Values.ToList();
			var values = await _context.Values.ToListAsync();

			return Ok(values);
		}
		//public ActionResult<IEnumerable<string>> Get()
		//{
		//	//throw new Exception("Test Exception");
		//	return new string[] { "value1", "value2" };
		//}

		// GET api/values/5
		[AllowAnonymous]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetValue(int id)
		{
			var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);

			return Ok(value);
		}
		//public ActionResult<string> Get(int id)
		//{
		//	return "value";
		//}

		// POST api/values
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
