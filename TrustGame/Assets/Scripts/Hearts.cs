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
    bool[] heartsIncremented;

    private void Start()
    {
        heartsIncremented = new bool[images.Length];
        for(int i = 0; i < heartsIncremented.Length; i++) 
        {
            heartsIncremented[i] = false;
        }

        UpdateHearts();
        StartCoroutine(blinkFirstHeart());
    }

    IEnumerator blinkFirstHeart() 
    {
        while (true) 
        {
            yield return new WaitForSeconds(1.0f);
            images[0].enabled = !images[0].enabled || (currentHealth > .99f);
        }
    }

    public void IncrementHealth(float health) 
    {
        currentHealth += health * healthRegenSpeed;
        UpdateHearts();
        if (currentHealth >= goalHealth) { Debug.Log("You won!"); } //todo: win condition
    }

    void UpdateHearts() 
    {
        for (int i = 0; i < images.Length; i++)
        {
            float fillAmount = Mathf.Clamp(currentHealth, .0f, (float)(i + 1));
            fillAmount -= (float)i;
            fillAmount = Mathf.Clamp(fillAmount, .0f, 1.0f);
            images[i].fillAmount = fillAmount;
            if(fillAmount > .01f && !heartsIncremented[i]) 
            {
                heartsIncremented[i] = true;
                StartCoroutine(OnHeartFilled(i));
            }
        }
    }

    static readonly float heartFillRate = 2.0f;
    IEnumerator OnHeartFilled(int i) 
    {
        float endScale = 2.0f;
        float currentScale = 1.0f;
        while(currentScale < endScale)
        {
            currentScale += Time.deltaTime* heartFillRate;
            images[i].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * currentScale;
            yield return null;
        }
        endScale = 1.0f;
        while(currentScale > endScale) 
        {
            currentScale -= Time.deltaTime* heartFillRate; 
            images[i].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * currentScale;
            yield return null;
        }
        images[i].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
