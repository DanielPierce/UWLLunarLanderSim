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
        internalRotation = Mathf.Round((-1 * (body.rotation - 90)) / degreeIncrement) * degreeIncrement;
        ApplyCounterTorque();

        record.internalRotation = internalRotation;
        sprite.transform.position = this.transform.position;
        sprite.transform.rotation = Quaternion.AngleAxis(body.rotation, Vector3.forward);
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
