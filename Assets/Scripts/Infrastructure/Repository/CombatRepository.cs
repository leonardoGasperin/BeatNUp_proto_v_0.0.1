using Domain.Repository;

namespace Infracstructure.Repository
{
    public class CombatRepository : ICombatRepository
    {
        public int TakeDamage(int healthPoint, int dmg)
        {
            return healthPoint - dmg;
        }
    }
}
