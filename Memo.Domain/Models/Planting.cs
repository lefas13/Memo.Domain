using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memo.Domain.ViewModels;

namespace Memo.Domain;

public class Planting
{
    private static int _id = 1;
    public int Id { get; set; }
    public DateTime PlantingTime { get; set; }

    public Planting(DateTime planting)
    {
        Id = _id;
        PlantingTime = planting;
        _id++;

    }

    public Planting() : this(new())
    { }

    public override string ToString()
    {
        return $"({Id}, {PlantingTime})";
    }

    public static implicit operator Planting(PlantingViewModel planting)
    {

        return new Planting
        {
            PlantingTime = planting.Planting,
        };
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;

        if (obj is Planting other)
        {
            if (PlantingTime == other.PlantingTime)
                return true;
        }

        if (obj is null) return false;

        return false;
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}

