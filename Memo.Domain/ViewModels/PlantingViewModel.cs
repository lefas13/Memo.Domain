namespace Memo.Domain.ViewModels
{
    public class PlantingViewModel(DateTime planting)
    {
        public DateTime Planting { get; set; } = planting;

        public PlantingViewModel() : this(new()) { }

        public static implicit operator PlantingViewModel(Planting planting)
        {
            return new PlantingViewModel
            {
                Planting = planting.PlantingTime,
            };
        }
    }
}
