using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip locknLoadClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player p = collision.gameObject.GetComponent<Player>();
            p.OnGunPickup();
            audioSource.PlayOneShot(locknLoadClip);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject,locknLoadClip.length);
        }
    }
}
