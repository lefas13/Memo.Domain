using Memo.Domain.ViewModels;
using Memo.Domain;
using System.ComponentModel;

namespace Memo.Domain.ViewModels
{
    public class VegetableViewModel(string name, double heightSm, PlantingViewModel plantings, TypeViewModel types, HarvestViewModel harvest)
    {
        [DisplayName("Сорт овоща")]
        public string Name { get; set; } = name;

        [DisplayName("Высота овоща, см")]
        public double HeightSm { get; set; } = heightSm;

        [DisplayName("Рекомендуемая дата посадки")]
        public DateTime PlantingTime { get; set; } = plantings.Planting;

        [DisplayName("Вид овоща")]
        public string TypeName { get; set; } = types.TypeV;

        [DisplayName("Время сбора урожая")]
        public int HarvestTime { get; set; } = harvest.HarvestTime;


        public VegetableViewModel() : this("", 0.0, new(), new(), new()) { }

        public static implicit operator VegetableViewModel(Vegetable vegetable)
        {
            ArgumentNullException.ThrowIfNull(vegetable.Type);
            ArgumentNullException.ThrowIfNull(vegetable.Planting);
            ArgumentNullException.ThrowIfNull(vegetable.Harvest);

            return new VegetableViewModel
            {
                Name = vegetable.Name,
                HeightSm = vegetable.HeightSm,
                PlantingTime = vegetable.Planting.PlantingTime,
                TypeName = vegetable.Type.TypeV,
                HarvestTime = vegetable.Harvest.HarvestTime,
            };

        }

    }
}

