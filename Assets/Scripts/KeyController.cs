using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public KeyScOb key;
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(1, 0, 0), 30 * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.GetInstance().KeyFind(key);
            Destroy(gameObject);
        }
    }
}
