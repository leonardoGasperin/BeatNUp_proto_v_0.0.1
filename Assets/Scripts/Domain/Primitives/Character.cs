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

        void Update()
        {
            if (isLive && healthPoint <= 0)
            {
                healthPoint = 0;
                isLive = false;
                Debug.Log("HP " + gameObject.name + ": " + healthPoint);
                Debug.Log(gameObject.name + " is live: " + isLive);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var objectCollisionLayer = collision.gameObject.layer;

            if (CombatRules.CanDoDamage(objectCollisionLayer, gameObject.layer, isAttacking))
            {
                RecivieDamage(collision.gameObject.GetComponent<Character>());
            }
        }

        private void RecivieDamage(Character target)
        {
            if (target != null && target.isLive && isAttacking)
            {
                target.healthPoint = target.combat.TakeDamage(target.healthPoint, damage);
                isAttacking = !isAttacking;
            }
        }
    }
}
