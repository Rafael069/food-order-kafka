using System;
using System.Collections.Generic;
using System.Text;
using FoodOrder.Domain.Entities;


// Banco FAKE

namespace FoodOrder.Infrastructure.Persistence
{
    

    public class InMemoryOrderStore
    {
        private readonly Dictionary<Guid, Order> _orders = new();

        public void Add(Order order)
        {
            _orders[order.Id] = order;
        }

        public Order? GetById(Guid id)
        {
            return _orders.TryGetValue(id, out var order) ? order : null;
        }

        public List<Order> GetAll()
        {
            return _orders.Values.ToList();
        }
    }

}
