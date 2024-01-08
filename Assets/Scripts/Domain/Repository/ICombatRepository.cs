namespace Domain.Repository
{
    public interface ICombatRepository
    {
        public int TakeDamage(int healthPoint, int dmg);
    }
}
