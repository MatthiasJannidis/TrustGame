using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    float currentHealth = .5f;
    float goalHealth = 5.0f;
    [SerializeField] float healthRegenSpeed;
    [SerializeField] Image[] images;

    private void Start()
    {
        UpdateHearts();
        StartCoroutine(blinkFirstHeart());
    }

    IEnumerator blinkFirstHeart() 
    {
        while (true) 
        {
            yield return new WaitForSeconds(1.0f);
            images[0].enabled = !images[0].enabled;
        }
    }

    public void IncrementHealth(float health) 
    {
        currentHealth += health * healthRegenSpeed;
        UpdateHearts();
        if(currentHealth >= 1.0f)
        {
            StopAllCoroutines();
            images[0].enabled = true;
        }
        if (currentHealth >= goalHealth) { Debug.Log("You won!"); } //todo: win condition
    }

    void UpdateHearts() 
    {
        for (int i = 0; i < images.Length; i++)
        {
            float fillAmount = Mathf.Clamp(currentHealth, .0f, (float)(i + 1));
            fillAmount -= (float)i;
            images[i].fillAmount = Mathf.Clamp(fillAmount, .0f, 1.0f);
        }
    }
}
