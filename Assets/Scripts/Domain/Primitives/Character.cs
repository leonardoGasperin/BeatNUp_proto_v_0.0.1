using Domain.Repository;
using Domain.Rules;
using Infracstructure.Repository;
using UnityEngine;

namespace Domain.Primitive
{
    public class Character : MonoBehaviour
    {
        public ICombatRepository combat;
        public IMovementRepository movement;
        public int level;
        public int healthPoint;
        public int damage;
        public float movementSpeed;
        public bool isLive;
        public bool isAttacking;

        protected virtual void Start()
        {
            combat = new CombatRepository();
            movement = new MovementRepository();
        }

        protected virtual void Update()
        {
            if (isLive && healthPoint <= 0)
            {
                healthPoint = 0;
                isLive = false;
                Debug.Log("HP " + gameObject.name + ": " + healthPoint);
                Debug.Log(gameObject.name + " is live: " + isLive);
            }
        }

        public void DoDamage(Character target)
        {
            if (target != null && target.isLive)
            {
                target.healthPoint = target.combat.TakeDamage(target.healthPoint, damage);
                Debug.Log(target.gameObject.name + " recivied DMG: " + damage);
            }
        }
    }
}
