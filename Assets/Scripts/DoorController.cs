using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private void Start()
    {
        GameManager.GetInstance().lockedDoor = this;
    }

    public void Open()
    {
        StartCoroutine(MoveDoorUp());
    }

    private IEnumerator MoveDoorUp()
    {
        float distance = 20f;
        float stepIncrement = distance / (2f / Time.fixedDeltaTime);
        float increment = 0;

        while (increment < distance)
        {
            increment += stepIncrement;
            transform.Translate(Vector3.forward * stepIncrement);
            yield return new WaitForFixedUpdate();
        }
    }
}
