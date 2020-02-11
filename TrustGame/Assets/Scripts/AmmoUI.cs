using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AmmoUI : MonoBehaviour
{
    [SerializeField] Image[] images = new Image[5];

    public void OnAmmoChanged(int ammo) 
    {
        for(int i = 0; i < images.Length; i++) 
        {
            images[i].enabled = (ammo>i);
        }
    }

    private void Start()
    {
        OnAmmoChanged(0);
    }
}
