using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RESTAPI.Data;
using RESTAPI.Models;

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly RESTAPIContext _context;

        public CategoriesController(RESTAPIContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public IEnumerable<Category> GetCategory()
        {
            return _context.Category;
        }
    }
}