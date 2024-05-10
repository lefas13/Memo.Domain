using Memo.Domain.ViewModels;

namespace Memo.Domain;

public class Type
{
    private static int _id = 1;
    public int Id { get; set; }
    public string TypeV { get; set; }

    public Type(string type)
    {
        Id = _id;
        TypeV = type;
        _id++;
    }


    public Type()
    {
        Id = _id;
        TypeV = string.Empty;
        _id++;
    }

    public override string ToString()
    {
        return $"({Id}, '{TypeV}')";
    }

    public static implicit operator Type(TypeViewModel variety)
    {
        return new Type
        {
            TypeV = variety.TypeV,
        };
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;

        if (obj is Type variety)
        {
            if (TypeV == variety.TypeV)
                return true;
            return false;
        }

        if (obj is null) return false;

        return false;
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}

