using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _ctx;
        [Obsolete]
        private readonly IHostingEnvironment _hosting;

        [Obsolete]
        public DutchSeeder(DutchContext ctx, IHostingEnvironment hosting)
        {
            _ctx = ctx;
            _hosting = hosting;
        }

        public void Seed()
        {
            //Check if database exists
            _ctx.Database.EnsureCreated();

            if (!_ctx.Products.Any())
            {
                //Path to json file - configure file route [_hosting]
                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");

                //Read Json File
                var json = File.ReadAllText(filepath);

                //Retrieve data from the Json file
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);

                //Add all the products to the database
                _ctx.Products.AddRange(products);

                //Create sample data
                var order = _ctx.Orders.Where(o => o.Id == 1).FirstOrDefault();

                if (order != null)
                {
                    order.Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    };
                }
                _ctx.SaveChanges();
            }
        }
    }
}