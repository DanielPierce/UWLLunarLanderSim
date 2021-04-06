using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeLanderController : LanderController
{

    protected const float arcadeModeRotationSpeed = 50f;
    protected const int degreeIncrement = 5;

    public override void Start()
    {
        base.Start();
    }
    public override void OnPhysicsUpdate()
    {
        if(IsLanded())
        {
            internalRotation = -1 * (body.rotation - 90);
            sprite.transform.rotation = Quaternion.AngleAxis(body.rotation, Vector3.forward);
        }
        else
        {
            internalRotation = Mathf.Round((-1 * (body.rotation - 90)) / degreeIncrement) * degreeIncrement;
            sprite.transform.rotation = Quaternion.AngleAxis(Mathf.Round(body.rotation / degreeIncrement) * degreeIncrement, Vector3.forward);
            body.angularVelocity = 0;
        }

        record.internalRotation = internalRotation;
        sprite.transform.position = this.transform.position;
    }

    public override void RotateLeft()
    {
        // Rotate counterclockwise in the Z plane here
        body.rotation += IsLanded() ? 0 : arcadeModeRotationSpeed * Time.deltaTime;
    }

    public override void RotateRight()
    {
        // Rotate clockwise in the Z plane here
        body.rotation -= IsLanded() ? 0 : arcadeModeRotationSpeed * Time.deltaTime;
    }
}
