using FoodOrder.Domain.Entities;
using FoodOrder.Infrastructure.Persistence;


public class OrderService
{
    private readonly InMemoryOrderStore _store;
    private readonly KafkaProducer _producer;

    public OrderService(InMemoryOrderStore store, KafkaProducer producer)
    {
        _store = store;
        _producer = producer;
    }

    //CREATE
    public async Task<Order> CreateOrder(List<string> items, string customer)
    {
        var order = new Order(items, customer);

        _store.Add(order);

        var evento = new
        {
            EventType = "OrderCreated",
            Data = order
        };

        await _producer.SendMessageAsync("tp-order", evento);

        return order;
    }

    public List<Order> GetAll()
    {
        return _store.GetAll();
    }

    public Order? GetById(Guid id)
    {
        return _store.GetById(id);
    }

    public async Task<bool> CancelOrder(Guid id)
    {
        var order = _store.GetById(id);

        if (order == null)
            return false;

        order.Cancel();

        var evento = new
        {
            EventType = "OrderCancelled",
            Data = order
        };

        await _producer.SendMessageAsync("tp-order", evento);

        return true;
    }


}