using System.Globalization;
using Memo.Domain.Models;
using Memo.Domain.ViewModels;

namespace Memo.Domain;

public class Vegetable
{
    private static int _id = 1;
    public int Id { get; set; } = _id;
    public string Name { get; set; }
    public Type? Type { get; set; }
    public double HeightSm { get; set; }
    public Planting? Planting { get; set; }
    public Harvest? Harvest { get; set; }

    public Vegetable(string name, double heightSm, Planting planting, Type type, Harvest harvest)
    {
        Id = _id;
        Name = name;
        HeightSm = heightSm;
        Planting = planting;
        Type = type;
        Harvest = harvest;
        _id++;
    }


    public Vegetable()
    {
        Name = string.Empty;
        HeightSm = 0.0;
        Planting = null;
        Type = null;
        Harvest = null;
        _id++;
    }

    public override string ToString()
    {
        return $"('{Name}', {Type?.Id}, {HeightSm.ToString().Replace(',','.')}, {Planting?.Id}, {Harvest?.Id})";
    }

    public static implicit operator Vegetable(VegetableViewModel vegetableViewModel)
    {
        return new Vegetable
        {
            Name = vegetableViewModel.Name,
            HeightSm = vegetableViewModel.HeightSm,
            Planting = new Planting(vegetableViewModel.PlantingTime),
            Type = new Type(vegetableViewModel.TypeName),
            Harvest = new Harvest(vegetableViewModel.HarvestTime)
        };
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;

        if (obj is Vegetable b)
        {
            if (Type == b.Type &&
                Planting == b.Planting &&
                HeightSm == b.HeightSm) return true;
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

