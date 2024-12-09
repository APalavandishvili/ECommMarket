namespace ECommMarket.Domain.Interfaces;

public interface IEntityBase<T>
{
    public T Id { get; set; }
}
