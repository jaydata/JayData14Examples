using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductApi
{
    public class Product
    {
        public Product()
        {
            this.Values = new string[0];
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTimeOffset? LastOrderDate { get; set; }

        public string[] Values { get; set; }

        public virtual Category Category { get; set; }
        
        public int? CategoryId { get; set; }
    }
}