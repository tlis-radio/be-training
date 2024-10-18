namespace Core.Domain.Model;

public abstract record AbstractIdentity : ValueObject;

public abstract record AbstractIdentity<T> : AbstractIdentity
{
    private readonly T _value;

    protected AbstractIdentity(T value)
    {
        Value = value;
    }

    public T Value
    {
        get => _value;
        private init
        {
            Validate(value);
            
            _value = value;
        }
    }

    protected virtual void Validate(T value)
    { }
}