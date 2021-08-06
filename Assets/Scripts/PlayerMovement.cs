using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 target;

    private void Start()
    {
        target = this.transform.position;
    }

    void FixedUpdate()
    {
        if (Equals(target, (Vector2) transform.position))
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            float xMovement = Mathf.Sign(moveX) * Mathf.Ceil(Mathf.Abs(moveX));
            float yMovement = Mathf.Sign(moveY) * Mathf.Ceil(Mathf.Abs(moveY));

            

            if (Input.anyKey)
            {
                target.x += xMovement;
                target.y += yMovement;

                float xRot = xMovement == 0 ? 0 : Mathf.Acos(xMovement);
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * ( xRot + Mathf.Asin(yMovement) ) -90 );
            }
        }
        else
        {
            transform.position = (Vector3)Vector2.MoveTowards(transform.position, target, 0.08f) + new Vector3(0,0,-0.1f);
        }
    }
}
