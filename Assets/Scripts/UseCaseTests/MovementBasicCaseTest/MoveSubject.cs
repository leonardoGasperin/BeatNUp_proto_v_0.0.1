using Domain.Primitive;
using Infracstructure.Repository;
using UnityEngine;

public class MoveSubject : Character
{
    private GameObject me;
    private void Start()
    {
        combat = new CombatRepository();
        movement = new MovementRepository();
        me = this.gameObject;
    }

    public void Update()
    {
        me.transform.position = movement.MovementOnXAxis(me.transform.position, movementSpeed, 1);
    }
}
