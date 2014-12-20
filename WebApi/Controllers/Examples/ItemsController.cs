using System;
using System.Collections.Generic;
using System.IO;
//using ProductService.Models;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using WebApi.Models;
namespace WebApi.Controllers.Examples
{
    public class ItemsController : ODataController
    {

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        [EnableQuery]
        public IQueryable<Item> Get()
        {

            List<Item> items = new List<Item>();
            //Directory.GetFileSystemEntries()

            Func<string, string> Relativize = s =>
            {
                return "/" + s.Replace(HttpRuntime.AppDomainAppPath, string.Empty).Replace("\\", "/");
            };
            Action<string> listDirectory = null;
            listDirectory = (a) =>
            {
                var _items = Directory.GetFiles(a, "*.html")
                                      .Select(Relativize)
                                      .Select(f => new Item { 
                                          ItemId = f,
                                          Name = f.Split('/').Last().Replace(".html",""),
                                          Parent = f.Replace("/Examples/", string.Empty)
                                                    .Split('/').First()
                                      });
                items.AddRange(_items);
                Directory.GetDirectories(a).ToList().ForEach(listDirectory);
            };
            listDirectory(Path.Combine(HttpRuntime.AppDomainAppPath, "Examples"));
            return items.AsQueryable();
        }
        [EnableQuery]
        public SingleResult<Item> Get([FromODataUri] string key)
        {
            IQueryable<Item> result = Get().Where(p => p.ItemId == key);
            return SingleResult.Create(result);
        }



    }
}