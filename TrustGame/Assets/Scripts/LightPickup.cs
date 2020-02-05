﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player p = collision.gameObject.GetComponent<Player>();
            p.OnFlashlightPickup();
            Destroy(gameObject);
        }
    }
}
