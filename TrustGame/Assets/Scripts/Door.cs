using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject destroyedDoorPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")) 
        {
            Instantiate(destroyedDoorPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
