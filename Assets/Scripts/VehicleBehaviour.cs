using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class VehicleBehaviour : MonoBehaviour
{
    private Vector2 target, startPos;
    public float speed = 0.08f;
    private void Start()
    {
        var position = transform.position;
        target = new Vector2(position.x,position.y + ((Math.Sign(position.y)*-1)*11f));
        startPos = position;
        StartCoroutine(VehicleMoving());
    }

    IEnumerator VehicleMoving()
    {
        while (true)
        {
            if (Equals(target, (Vector2) transform.position))
            {
                transform.position = (Vector3)startPos + new Vector3(0, 0, -0.1f);
            }

            transform.position = (Vector3) Vector2.MoveTowards(transform.position, target, speed) + new Vector3(0, 0, -0.1f);
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
    }
}
