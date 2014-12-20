using ProductApi;
using System;
//using ProductService.Models;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using WebApi.Models;
namespace ProductService.Controllers
{
    public class CategoriesController : ODataController
    {
        
        private bool ProductExists(int key)
        {
            return true;
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        [EnableQuery]
        public IQueryable<Category> Get()
        {
            return ProductDb.Instance.Categories.AsQueryable();
        }
        [EnableQuery]
        public SingleResult<Category> Get([FromODataUri] int key)
        {
            IQueryable<Category> result = Get().Where(p => p.CategoryId == key);
            return SingleResult.Create(result);
        }

        // POST: odata/Categories
        public IHttpActionResult Post(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            category.CategoryId = ProductDb.Instance.Categories.Count() + 1;
            category.CreationDate = DateTime.Now;
            ProductDb.Instance.Categories.Add(category);
            return Created(category);
        }
        //[EnableQuery]
        //public IQueryable<Product> GetProducts([FromODataUri] int key)
        //{
        //    return ProductDb.Instance.Categories.Where(m => m.CategoryId == key)
        //                             .SelectMany(m => m.Products).AsQueryable();
        //}
    }
}