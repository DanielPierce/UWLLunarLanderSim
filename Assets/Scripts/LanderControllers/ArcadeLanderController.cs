using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeLanderController : LanderController
{

    protected const float arcadeModeRotationSpeed = 50f;

    public override void OnPhysicsUpdate()
    {
        internalRotation = Mathf.Round((-1 * (body.rotation - 90)) / 5) * 5;
        record.internalRotation = internalRotation;

        sprite.transform.rotation = Quaternion.AngleAxis(Mathf.Round(body.rotation / 5) * 5, Vector3.forward);
        sprite.transform.position = this.transform.position;
    }

    public override void RotateLeft()
    {
        // Rotate counterclockwise in the Z plane here
        body.rotation += arcadeModeRotationSpeed * Time.deltaTime;
    }

    public override void RotateRight()
    {
        // Rotate clockwise in the Z plane here
        body.rotation -= arcadeModeRotationSpeed * Time.deltaTime;
    }
}
