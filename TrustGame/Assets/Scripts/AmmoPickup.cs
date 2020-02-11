using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    bool pickedUp = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !pickedUp)
        {
            Player p = collision.gameObject.GetComponent<Player>();
            if (p.addAmmo())
            {
                pickedUp = true;
                AudioSource source = GetComponent<AudioSource>();
                source.Play();
                GetComponent<SpriteRenderer>().enabled = false;
                Destroy(gameObject, source.clip.length);
            }
        }
    }
}
