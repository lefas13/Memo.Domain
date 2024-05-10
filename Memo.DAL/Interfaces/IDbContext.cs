using Memo.Domain.Models;
using Memo.Domain;

namespace Memo.DAL.Interfaces
{
    public interface IDbContext
    {
        List<Vegetable> Vegetable { get; }
        List<Domain.Type> Type { get; }
        List<Planting> Planting { get; }
        List<Harvest> Harvest { get; }

        void SaveChanges();

    }
}

