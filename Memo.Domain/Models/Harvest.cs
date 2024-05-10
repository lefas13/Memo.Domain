using Memo.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memo.Domain.Models
{
    public class Harvest
    {
        private static int _id = 1;
        public int Id { get; set; }
        public int HarvestTime { get; set; }

        public Harvest(int harvest)
        {
            Id = _id;
            HarvestTime = harvest;
            _id++;

        }

        public Harvest() : this(0)
        { }

        public override string ToString()
        {
            return $"({Id}, {HarvestTime})";
        }

        public static implicit operator Harvest(HarvestViewModel planting)
        {

            return new Harvest
            {
                HarvestTime = planting.HarvestTime,
            };
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;

            if (obj is Harvest other)
            {
                if (HarvestTime == other.HarvestTime)
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
}
