using Domain.Repository;
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
                Debug.Log("HP B: " + healthPoint);
                Debug.Log("Subject B is live: " + isLive);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var objectCollisionLayer = collision.gameObject.layer;

            if (
                gameObject.layer != objectCollisionLayer
                    && objectCollisionLayer == LayerMask.NameToLayer("Player")
                || objectCollisionLayer == LayerMask.NameToLayer("Enemy")
            )
            {
                var target = collision.gameObject.GetComponent<Character>();

                if (target != null && target.isLive && isAttacking)
                {
                    target.healthPoint = target.combat.TakeDamage(target.healthPoint, damage);
                    isAttacking = !isAttacking;
                }
            }
        }
    }
}
