using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        //Products
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        bool SaveAll();
        
        //Orders
        IEnumerable<Order> GetAllOrders(bool includeItems);
        Order GetOrderById(int id);

        //Add orders
        void AddEntity(object model);
    }
}
