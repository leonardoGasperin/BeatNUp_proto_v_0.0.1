using Core.Contract;
using Infracstructure.Repository;
using UnityEngine;

namespace Configuration
{
    public class DependencyInjectionConfig : MonoBehaviour
    {
        void Awake()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            ServiceLocator.Register<ICombatRepository>(new CombatRepository());
            ServiceLocator.Register<IMovementRepository>(new MovementRepository());
        }
    }
}
