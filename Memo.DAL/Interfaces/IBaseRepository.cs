namespace Memo.DAL.Interfaces;

public interface IBaseRepository<T>
{
    List<T> ReadAll();
    bool Create(T entity);
    T Read(int id);
    bool Update(T oldEntity, T newEntity);
    bool Delete(T entity);
}

