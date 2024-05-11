using Memo.DAL.Interfaces;
using Memo.Domain.Models;

namespace Memo.DAL.Repositories;

public class HarvestRepository(IDbContext dbContext) : IBaseRepository<Harvest>
{
    private readonly IDbContext _dbContext = dbContext;

    public List<Harvest> ReadAll()
    {
        return _dbContext.Harvest;
    }

    public bool Create(Harvest harvest)
    {
        _dbContext.Harvest.Add(harvest);
        _dbContext.SaveChanges();
        return true;
    }

    public Harvest Read(int id)
    {
        if (id > 0 && id < _dbContext.Harvest.Count)
        {
            Harvest harvestToRead = _dbContext.Harvest.ElementAt(id);
            return harvestToRead;
        }
        else
        {
            throw new IndexOutOfRangeException("Выход за пределы при попытке чтения из базы данных");
        }
    }

    public bool Update(Harvest oldHarvest, Harvest newHarvest)
    {
        List<Harvest> harvestsToUpdate = _dbContext.Harvest!;
        Harvest? harvestToUpdate = null;
        foreach (Harvest harvest in harvestsToUpdate)
        {
            if (harvest == oldHarvest)
            {
                harvestToUpdate = harvest;
                break;
            }
        }
        if (harvestToUpdate != null)
        {
            harvestToUpdate.HarvestTime = newHarvest.HarvestTime;

            _dbContext.SaveChanges();
            return true;
        }
        else
        {
            throw new ArgumentNullException($"Овощь не найден в базе данных для обновления:{harvestToUpdate}");
        }
    }

    public bool Delete(Harvest harvest)
    {
        bool isRemove = _dbContext.Harvest.Remove(harvest);
        _dbContext.SaveChanges();
        return isRemove;
    }

}
