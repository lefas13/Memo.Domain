namespace Memo.Domain.ViewModels
{
    public class TypeViewModel(string type)
    {
        public string TypeV { get; set; } = type;

        public TypeViewModel() : this("") { }

        public static implicit operator TypeViewModel(Type variety)
        {
            return new TypeViewModel
            {
                TypeV = variety.TypeV,
            };
        }
    }
}

