using Memo.Domain.Models;

namespace Memo.Domain.ViewModels
{
    public class HarvestViewModel(int harvest)
    {
        public int HarvestTime { get; set; } = harvest;

        public HarvestViewModel() : this(0) { }

        public static implicit operator HarvestViewModel(Harvest harvest)
        {
            return new HarvestViewModel
            {
                HarvestTime = harvest.HarvestTime,
            };
        }
    }
}

