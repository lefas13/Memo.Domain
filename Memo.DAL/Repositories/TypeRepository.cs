using Memo.DAL.Interfaces;
using Memo.Domain;

namespace Memo.DAL.Repositories;

public class TypeRepository(IDbContext dbContext) : IBaseRepository<Memo.Domain.Type>
{
    private readonly IDbContext _dbContext = dbContext;

    public List<Memo.Domain.Type> ReadAll()
    {
        return _dbContext.Type;
    }

    public bool Create(Memo.Domain.Type type)
    {
        _dbContext.Type.Add(type);
        _dbContext.SaveChanges();
        return true;
    }

    public Memo.Domain.Type Read(int id)
    {
        if (id > 0 && id < _dbContext.Type.Count)
        {
            Memo.Domain.Type typeToRead = _dbContext.Type.ElementAt(id);
            return typeToRead;
        }
        else
        {
            throw new IndexOutOfRangeException("Выход за пределы при попытке чтения из базы данных");
        }
    }

    public bool Update(Memo.Domain.Type oldType, Memo.Domain.Type newType)
    {
        Memo.Domain.Type? typeToUpdate = _dbContext.Type.Find(s => s == oldType);

        if (typeToUpdate != null)
        {
            typeToUpdate.TypeV = newType.TypeV;

            _dbContext.SaveChanges();
            return true;
        }
        else
        {
            throw new ArgumentNullException($"Овощь не найден в базе данных для обновления:{typeToUpdate}");
        }
    }

    public bool Delete(Memo.Domain.Type type)
    {
        bool isRemove = _dbContext.Type.Remove(type);
        _dbContext.SaveChanges();
        return isRemove;
    }

}

