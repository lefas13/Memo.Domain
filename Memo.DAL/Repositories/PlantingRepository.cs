using Memo.DAL.Interfaces;
using Memo.Domain;

namespace Memo.DAL.Repositories;

public class PlantingRepository(IDbContext dbContext) : IBaseRepository<Planting>
{
    private readonly IDbContext _dbContext = dbContext;

    public List<Planting> ReadAll()
    {
        return _dbContext.Planting;
    }

    public bool Create(Planting planting)
    {
        _dbContext.Planting.Add(planting);
        _dbContext.SaveChanges();
        return true;
    }

    public Planting Read(int id)
    {
        if (id > 0 && id < _dbContext.Planting.Count)
        {
            Planting plantingToRead = _dbContext.Planting.ElementAt(id);
            return plantingToRead;
        }
        else
        {
            throw new IndexOutOfRangeException("Выход за пределы при попытке чтения из базы данных");
        }
    }

    public bool Update(Planting oldPlanting, Planting newPlanting)
    {
        Planting? plantingToUpdate = _dbContext.Planting.Find(s => s == oldPlanting);

        if (plantingToUpdate != null)
        {
            plantingToUpdate.PlantingTime = newPlanting.PlantingTime;

            _dbContext.SaveChanges();
            return true;
        }
        else
        {
            throw new ArgumentNullException($"Овощь не найден в базе данных для обновления:{plantingToUpdate}");
        }
    }

    public bool Delete(Planting planting)
    {
        bool isRemove = _dbContext.Planting.Remove(planting);
        _dbContext.SaveChanges();
        return isRemove;
    }

}
