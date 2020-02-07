﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField, Range(1, 10)] int amount;
    [SerializeField] GameObject spawnedPrefab;
    [SerializeField] Transform[] spawnPositions;

    public void Trigger() 
    {
        for(int i = 0; i < amount; i++) 
        {
            if (spawnPositions.Length == 1) Instantiate(spawnedPrefab, spawnPositions[0].position, Quaternion.identity);
            else Instantiate(spawnedPrefab, spawnPositions[i].position, Quaternion.identity);
        }
    }
}
