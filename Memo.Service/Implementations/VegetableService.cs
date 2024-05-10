using Memo.DAL.Interfaces;
using Memo.Domain.ViewModels;
using Memo.Domain;
using Memo.Service.Interfaces;

namespace Memo.Service.Implementations
{
    public class VegetableService(IBaseRepository<Vegetable> vegetableRepository) : IVegetableService
    {
        private readonly IBaseRepository<Vegetable> _vegetableRepository = vegetableRepository;

        public bool Create(VegetableViewModel vegetableViewModel)
        {
            try
            {
                Vegetable vegetable = vegetableViewModel;

                ArgumentNullException.ThrowIfNull(vegetable);

                return _vegetableRepository.Create(vegetable);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException($"[Create]:Объект Vegetable не найден:{vegetableViewModel}");
            }
            catch (Exception ex)
            {
                throw new Exception($"[Create]:{ex.Message}");
            }

        }

        public bool Delete(string name)
        {
            try
            {
                Vegetable? vegetable = _vegetableRepository.ReadAll().FirstOrDefault(vegetable => vegetable.Name == name);

                ArgumentNullException.ThrowIfNull(vegetable);

                return _vegetableRepository.Delete(vegetable); ;
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException($"[Delete]:Объект Vegetable не найден по названию: {name}");
            }
            catch (Exception ex)
            {
                throw new Exception($"[Delete]:{ex.Message}");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Vegetable? vegetable = _vegetableRepository.ReadAll().FirstOrDefault(vegetable => vegetable.Id == id);

                ArgumentNullException.ThrowIfNull(vegetable);

                return _vegetableRepository.Delete(vegetable); ;
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException($"[Delete]:Объект Vegetable не найден по id: {id}");
            }
            catch (Exception ex)
            {
                throw new Exception($"[Delete]:{ex.Message}");
            }
        }

        public bool Edit(int id, VegetableViewModel vegetableViewModel)
        {
            try
            {
                Vegetable? oldVegetable = _vegetableRepository.ReadAll().FirstOrDefault(vegetable => vegetable.Id == id);
                Vegetable newVegetable = vegetableViewModel;

                ArgumentNullException.ThrowIfNull(oldVegetable);
                ArgumentNullException.ThrowIfNull(newVegetable);

                return _vegetableRepository.Update(oldVegetable, newVegetable);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException($"[Edit]:Объект Vegetable не найден по:{ex.ParamName}");
            }
            catch (Exception ex)
            {
                throw new Exception($"[Edit]:{ex.Message}");
            }
        }

        public bool Edit(string name, VegetableViewModel vegetableViewModel)
        {
            try
            {
                Vegetable? oldVegetable = _vegetableRepository.ReadAll().FirstOrDefault(vegetable => vegetable.Name == name);
                ArgumentNullException.ThrowIfNull(oldVegetable);
                Vegetable newVegetable = vegetableViewModel;
                ArgumentNullException.ThrowIfNull(newVegetable);
                newVegetable.Id = oldVegetable.Id;

                return _vegetableRepository.Update(oldVegetable, newVegetable);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException($"[Edit]:Объект Vegetable не найден по:{ex.ParamName}");
            }
            catch (Exception ex)
            {
                throw new Exception($"[Edit]:{ex.Message}");
            }
        }


        public VegetableViewModel Get(int id)
        {
            try
            {
                Vegetable? vegetable = _vegetableRepository.ReadAll().FirstOrDefault(vegetable => vegetable.Id == id);

                ArgumentNullException.ThrowIfNull(vegetable);

                VegetableViewModel vegetableViewModel = vegetable;

                return vegetableViewModel;
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException($"[Get]:Объект Vegetable не найден по id:{id}");
            }
            catch (Exception ex)
            {
                throw new Exception($"[Get]:{ex.Message}");
            }
        }

        public List<VegetableViewModel> GetAll()
        {
            try
            {
                List<Vegetable> vegetable = _vegetableRepository.ReadAll();
                List<VegetableViewModel> vegetableViewModels = [];
                for (int i = 0; i < vegetable.Count; i++)
                {
                    ArgumentNullException.ThrowIfNull(vegetable[i]);
                    vegetableViewModels.Add(vegetable[i]);
                }

                return vegetableViewModels;
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException($"[GetAll]:Объект Vegetable не найден");
            }
            catch (Exception ex)
            {
                throw new Exception($"[GetAll]:{ex.Message}");
            }
        }

        public VegetableViewModel GetByName(string name)
        {
            try
            {
                Vegetable? vegetable = _vegetableRepository.ReadAll().FirstOrDefault(vegetable => vegetable.Name == name);

                ArgumentNullException.ThrowIfNull(vegetable);

                VegetableViewModel vegetableViewModel = vegetable;

                return vegetableViewModel;
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException($"[GetByName]:Объект Vegetable не найден по имени: {name}");
            }
            catch (Exception ex)
            {
                throw new Exception($"[GetByName]:{ex.Message}");
            }
        }
    }
}

