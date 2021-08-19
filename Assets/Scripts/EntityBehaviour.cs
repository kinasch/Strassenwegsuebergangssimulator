using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EntityBehaviour : MonoBehaviour
{
    public GameManaging gameManager;
    
    private Vector2 target, startPos;
    public float speed = 0.08f;
    private void Start()
    {
        var position = transform.position;
        target = new Vector2(position.x,position.y + ((Math.Sign(position.y)*-1)*11f));
        startPos = position;
        StartCoroutine(StuffMoving());
    }

    IEnumerator StuffMoving()
    {
        while (true)
        {
            if (Equals(target, (Vector2) transform.position))
            {
                transform.position = (Vector3)startPos + new Vector3(0, 0, -0.09f);
            }

            transform.position = (Vector3) Vector2.MoveTowards(transform.position, target, speed) + new Vector3(0, 0, -0.09f);
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (this.name.Contains("vehicle"))
        {
            gameManager.LoseGame();
        }
        else if(this.name.Contains("leaf"))
        {
            gameManager.playerOnLeaf++;
            other.gameObject.GetComponent<PlayerMovement>().moveWithLeaf = true;
            other.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(this.name.Contains("leaf"))
        {
            gameManager.playerOnLeaf--;
            other.gameObject.GetComponent<PlayerMovement>().moveWithLeaf = false;
            other.transform.parent = null; // Same problem when changing leaves as happened with variable above.
        }
    }
}
