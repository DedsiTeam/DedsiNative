namespace Dedsi.Entities;

public interface IAggregateRoot : IEntity;

public interface IAggregateRoot<TKey> : IEntity<TKey>, IAggregateRoot;
public class AggregateRoot: Entity, IAggregateRoot;

public class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>;

public class AggregateRootString: AggregateRoot<string>;
