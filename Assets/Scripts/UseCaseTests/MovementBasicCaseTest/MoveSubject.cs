using Domain.Primitive;
using UnityEngine;

public class MoveSubject : Character
{
    private GameObject me;

    protected override void Start()
    {
        base.Start();
        me = gameObject;
    }

    protected override void Update()
    {
        base.Update();
        movement.MovementOnXAxis(me.transform, movementSpeed, 1);
    }
}
