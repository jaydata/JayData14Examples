using ProductApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class ProductDb
    {
        public ProductDb()
        {
            this.Categories = new List<Category>();
            this.Products = new List<Product>();
            var c1 = new Category { CategoryId = 1, Name = "Beverages", Products = new List<Product>() };
            var p1 = new Product { Id = 1, Name = "Green tea", Category = c1, Values = new [] {"alma", "korte"} };
            var p2 = new Product { Id = 2, Name = "Black tea", Category = c1 };
            c1.Products.Add(p1);
            c1.Products.Add(p2);
            this.Categories.Add(c1);
            this.Products.Add(p1);
            this.Products.Add(p2);
            var c2 = new Category { CategoryId = 2, Name = "Food", Products = new List<Product>() };
            var p21 = new Product { Id = 3, Name = "Club sandwitch", Category = c2, Values = new[] { "apple", "lemon" } };
            var p22 = new Product { Id = 4, Name = "Small bag of snack", Category = c2 };
            c2.Products.Add(p21);
            c2.Products.Add(p22);
            this.Categories.Add(c2);
            this.Products.Add(p21);
            this.Products.Add(p22);
            this.Products.ForEach(p => p.CategoryId = p.Category.CategoryId);
            //this.Categories.Add();
        }

        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }

        static ProductDb()
        {
            Instance = new ProductDb();
        }
        public static ProductDb Instance { get; set; }
    }
}