using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum_Snackis.Data;
using Forum_Snackis.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum_Snackis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsertsController : ControllerBase
    {
        private readonly Forum_SnackisContext _context;

        public InsertsController(Forum_SnackisContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Insert>>> GetInsertsController()
        {
            return await _context.Inserts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Insert>> GetInsert(int id)
        {
            var insert = await _context.Inserts.FindAsync(id);

            if (insert == null)
            {
                return NotFound();
            }

            return insert;
        }

        
    }
}
