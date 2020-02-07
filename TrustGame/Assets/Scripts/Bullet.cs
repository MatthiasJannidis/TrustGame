using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip bloodimpact;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                audioSource.PlayOneShot(bloodimpact);
                StartCoroutine(SetInactive(bloodimpact.length));
            } else
            {
                SetInactive(0);
            }
            
        }
    }
    IEnumerator SetInactive(float length)
    {
        yield return new WaitForSeconds(length);
        gameObject.SetActive(false);
    }
}
