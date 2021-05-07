using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayVector : MonoBehaviour
{

    public Vector2 displayVector;
    public SpriteRenderer sprite;

    public float MaxOut;
    public float MaxIn;


    // Update is called once per frame
    void Update()
    {
        sprite.size = new Vector2(0.64f, Mathf.Log(1 + Mathf.Lerp(0, MaxOut, Mathf.InverseLerp(0, MaxIn, displayVector.magnitude))));
        float rotation = Mathf.Atan2(-1 * displayVector.x, displayVector.y) * Mathf.Rad2Deg;
        sprite.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}