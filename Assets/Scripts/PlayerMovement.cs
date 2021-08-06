using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private Camera mainCam;
    
    private Vector2 target;

    private void Start()
    {
        // At the start. set the target to the player position to allow inputs.
        target = this.transform.position;
    }

    void FixedUpdate()
    {
        /*
         * As long as the target is not the position of the player (this.transform)
         * keep moving towards the target.
         *
         * Upon hitting the target movement orders can be made.
         */
        if (Equals(target, (Vector2) transform.position))
        {
            // Take the inputs from the vertical and horizontal axis.
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            
            // Round the values from the axis to either 1 for positive or -1 for negative
            float xMovement = Mathf.Sign(moveX) * Mathf.Ceil(Mathf.Abs(moveX));
            float yMovement = Mathf.Sign(moveY) * Mathf.Ceil(Mathf.Abs(moveY));
            
            // Only alter the target and rotation while a key is pressed (temporary solution)
            if (Input.anyKey)
            {
                // This should prevent the player from moving diagonally
                if (Math.Abs(xMovement) > 0 && Math.Abs(yMovement) > 0)
                {
                    xMovement = moveX > moveY ? xMovement : 0;
                    yMovement = moveX < moveY ? yMovement : 0;
                }
                // Set the coordinates of the new target.
                target.x += xMovement;
                target.y += yMovement;
                
                // Prevent the Sprite from rotating due to acos(0) resulting in PI/2
                float xRot = xMovement == 0 ? 0 : Mathf.Acos(xMovement);
                // Rotate using the inputs (Euler is in degrees, so conversion is needed)
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * ( xRot + Mathf.Asin(yMovement) ) -90 );
            }
        }
        else
        {
            // Boundary Detection
            Vector3 camPos = mainCam.transform.position;
            if (target.x > camPos.x - 9f && target.x < camPos.x + 9f && target.y > camPos.y - 5f && target.y < camPos.y + 5f)
            {
                // Move the player towards the target.
                // Move a bit on the z-Axis to display above the floor tiles.
                transform.position = (Vector3) Vector2.MoveTowards(transform.position, target, speed) +
                                     new Vector3(0, 0, -0.1f);
            }
            else
            {
                target = transform.position;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("test");
    }
}
