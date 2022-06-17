namespace Slices.V1.Model;

/// <summary>
/// A container that holds a single value which may be absent/unset.
/// In case of latter, a reason (arbitrary string) may be specified.
/// </summary>
/// <typeparam name="T">The type of the value</typeparam>
public struct SfdoOptional<T>
{
    public bool IsSet { get; private set; } = false;
    public string? AbsenceReason { get; private set; } = null;

    private T? _value = default;
    public T Value
    {
        get
        {
            if (!IsSet)
            {
                throw new InvalidOperationException(
                    $"Cannot retrieve {nameof(SfdoOptional<T>)}.{nameof(Value)} when {nameof(IsSet)} is false"
                );
            }
            
            return _value!;
        }
    }

    public SfdoOptional()
    {
    }

    public void SetValue(T value)
    {
        _value = value;

        IsSet = true;
        AbsenceReason = null;
    }

    public void SetAbsent(string? reason)
    {
        _value = default;

        IsSet = false;
        AbsenceReason = reason;
    }
}

public static class SfdoOptional
{
    public static SfdoOptional<T> WithValue<T>(T value)
    {
        SfdoOptional<T> optional = new();
        optional.SetValue(value);

        return optional;
    }

    public static SfdoOptional<T> WithAbsent<T>(string? reason)
    {
        SfdoOptional<T> optional = new();
        optional.SetAbsent(reason);

        return optional;
    }
}