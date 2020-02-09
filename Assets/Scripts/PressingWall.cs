using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressingWall : MonoBehaviour
{
    public float distance = 10f;
    public float time = 2f;
    public float delay = 2f;
    public bool isMoving;

    private float stepIncrement;
    private Vector3 direction;
    private BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        if (isMoving)
        {
            stepIncrement = distance / (time / Time.fixedDeltaTime);
            StartCoroutine(WallMovement());
        }
    }

    private void Move(Vector3 direction)
    {
        transform.Translate(direction * stepIncrement);
    }


    IEnumerator WallMovement()
    {
        float moved = 0f;
        direction = Vector3.right;
        while (true)
        {
            while (moved < distance)
            {
                moved += stepIncrement;
                Move(direction);
                yield return new WaitForFixedUpdate();
            }
            moved = 0f;
            direction = -direction;
            yield return new WaitForSeconds(delay);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedGameObject = collision.gameObject;
        bool isPlayer = collidedGameObject.CompareTag("Player");

        if (!collidedGameObject.CompareTag("PressingWall"))
        {
            if (collidedGameObject.transform.parent && collidedGameObject.transform.parent.CompareTag("PressingWall"))
            {
                if (isPlayer)
                {
                    collidedGameObject.transform.parent = null;
                    GameManager.GetInstance().LifeDecrease();
                }
            }
            else
            {
                collidedGameObject.transform.parent = transform;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.transform.parent = null;
    }

    //Deals with a bug due to CharacterController not being able to be modified by physics. If user runs into the wall, he'll show up on top of the object, but at least he'll rot in hell 
    //(this was the hardest part of this whole thing omfg)
    private void OnCollisionStay(Collision collision)
    {
        GameObject collidedGameObject = collision.gameObject;
        if ((collision.collider.bounds.min.y > boxCollider.bounds.max.y) && collidedGameObject.CompareTag("Player"))
        {
            GameManager.GetInstance().LifeDecrease();
        }
    }


}

