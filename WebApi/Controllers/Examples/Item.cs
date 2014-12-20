using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Controllers.Examples
{
    public class Item
    {
        public string ItemId { get; set; }
        public string Name { get; set; }
        public string Parent { get; set; }
    }
}