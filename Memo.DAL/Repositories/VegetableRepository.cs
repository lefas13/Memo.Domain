using Memo.DAL.Interfaces;
using Memo.Domain;

namespace Memo.DAL.Repositories;

public class VegetableRepository(IDbContext dbContext) : IBaseRepository<Vegetable>
{
    private readonly IDbContext _dbContext = dbContext;

    public List<Vegetable> ReadAll()
    {
        return _dbContext.Vegetable;
    }

    public bool Create(Vegetable vegetable)
    {
        _dbContext.Vegetable.Add(vegetable);
        _dbContext.SaveChanges();
        return true;
    }

    public Vegetable Read(int id)
    {
        if (id > 0 && id < _dbContext.Vegetable.Count)
        {
            Vegetable vegetableToRead = _dbContext.Vegetable.ElementAt(id);
            return vegetableToRead;
        }
        else
        {
            throw new IndexOutOfRangeException("Выход за пределы при попытке чтения из базы данных");
        }
    }

    public bool Update(Vegetable oldVegetable, Vegetable newVegetable)
    {
        Vegetable? vegetableToUpdate = _dbContext.Vegetable.Find(vegetable => vegetable == oldVegetable);

        if (vegetableToUpdate != null)
        {
            vegetableToUpdate.Name = newVegetable.Name;
            vegetableToUpdate.Id = newVegetable.Id;
            vegetableToUpdate.HeightSm = newVegetable.HeightSm;
            vegetableToUpdate.Planting = newVegetable.Planting;
            vegetableToUpdate.Type = newVegetable.Type;
            vegetableToUpdate.Harvest = newVegetable.Harvest;

            _dbContext.SaveChanges();
            return true;
        }
        else
        {
            throw new ArgumentNullException($"Овощь не найден в базе данных для обновления:{vegetableToUpdate}");
        }
    }

    public bool Delete(Vegetable vegetable)
    {
        bool isRemove = _dbContext.Vegetable.Remove(vegetable);
        _dbContext.SaveChanges();
        return isRemove;
    }

}

