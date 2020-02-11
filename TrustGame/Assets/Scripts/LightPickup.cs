using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPickup : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip pickUpClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player p = collision.gameObject.GetComponent<Player>();
            p.OnFlashlightPickup();
            audioSource.PlayOneShot(pickUpClip);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, pickUpClip.length);
        }
    }
}
