
//using ProductService.Models;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using WebApi.Models;

namespace ProductApi
{
    public class ProductsController : ODataController
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
        public IQueryable<Product> Get()
        {
            return ProductDb.Instance.Products.AsQueryable();
        }
        [EnableQuery]
        public SingleResult<Product> Get([FromODataUri] int key)
        {
            IQueryable<Product> result = Get().Where(p => p.Id == key);
            return SingleResult.Create(result);
        }


        public IHttpActionResult Post(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            product.Id = ProductDb.Instance.Products.Count() + 1;
            product.Category = ProductDb.Instance.Categories.Single(c => c.CategoryId == product.CategoryId);
            ProductDb.Instance.Products.Add(product);
            return Created(product);
        }
    }
}