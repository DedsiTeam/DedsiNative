namespace Dedsi.Entities;

public interface IEntity
{

}


public interface IEntity<TKey> : IEntity
{
    TKey Id { get; }
}

public abstract class Entity : IEntity;

public abstract class Entity<TKey> : Entity, IEntity<TKey>
{
    public virtual TKey Id { get; protected set; }
}