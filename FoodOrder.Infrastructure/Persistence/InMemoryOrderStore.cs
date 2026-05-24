using FoodOrder.Domain.Entities;
using FoodOrder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;


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
            if (_orders.TryGetValue(id, out var order))
            {
                if (order.Status == OrderStatus.Cancelled)
                    return null;

                return order;
            }

            return null;
        }

        public List<Order> GetAll()
        {
            return _orders.Values
                .Where(x => x.Status != OrderStatus.Cancelled)
                .ToList();
        }
    }

}
