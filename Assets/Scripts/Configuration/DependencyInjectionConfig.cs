using Core.Contract;
using Core.Primitive;
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

        public Character CreateCharacter()
        {
            var combat = ServiceLocator.Resolve<ICombatRepository>();
            var movement = ServiceLocator.Resolve<IMovementRepository>();
            GameObject characterObject = new("Character");
            Character character = characterObject.AddComponent<Character>();

            return character;
        }
    }
}
