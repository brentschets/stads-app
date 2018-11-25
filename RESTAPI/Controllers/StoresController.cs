﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTAPI.Data;
using RESTAPI.Models;

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly RESTAPIContext _context;

        public StoresController(RESTAPIContext context)
        {
            _context = context;
        }

        // GET: api/Stores
        [HttpGet]
        public IEnumerable<Store> GetStore()
        {
            return _context.Store.Include(s => s.Address).Include(s => s.Category);
        }

        // GET: api/Stores/Popular/10
        [HttpGet("Popular/{limit}")]
        public IActionResult GetPopular([FromRoute] int limit)
        {
            var res = _context.Store.Include(s => s.Address).Include(s => s.Category).OrderByDescending(s => s.Visited)
                .Take(limit).ToList();
            return Ok(res);
        }

        // GET: api/Stores/ByCategory/5
        [HttpGet("ByCategory/{id}")]
        public IActionResult GetByCategory([FromRoute] int id)
        {
            var res = _context.Store.Include(s => s.Category).Where(s => s.Category.CategoryId == id);
            return Ok(res);
        }
    }
}