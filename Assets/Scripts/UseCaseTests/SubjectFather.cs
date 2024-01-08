using Domain.Repository;
using UnityEngine;

namespace Misc.Experimental
{
    public class SubjectFather : MonoBehaviour
    {
        public ICombatRepository combat;
        public int healthPoint;
        public bool isLive;
        public int damage;
    }
}
