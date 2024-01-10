using Domain.Repository;
using UnityEngine;

namespace Domain.Primitive
{
    public class Character : MonoBehaviour
    {
        public ICombatRepository combat;
        public int healthPoint;
        public int damage;
        public bool isLive;
        public bool isAttacking;
    }
}
