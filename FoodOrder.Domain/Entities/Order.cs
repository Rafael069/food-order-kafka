using FoodOrder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrder.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }

        public List<string> Items { get; private set; }

        public string Customer { get; private set; }

        public OrderStatus Status { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? LastUpdatedAt { get; private set; }

        public Order(List<string> items, string customer)
        {
            Id = Guid.NewGuid();
            Items = items;
            Customer = customer;
            Status = OrderStatus.Created;
            CreatedAt = DateTime.UtcNow;
        }


        // Regra de negócio dentro da entidade
        public void Cancel()
        {
            Status = OrderStatus.Cancelled;
            LastUpdatedAt = DateTime.UtcNow;
        }
    }
}
