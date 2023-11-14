using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject coin;
    [SerializeField] private float respawnTime;
    [SerializeField] private int bombMaxSpawnCount;
    [SerializeField] private int coinGenTime;

    private void Awake()
    {
        StartCoroutine(BombSpawn());
    }

    private IEnumerator BombSpawn()
    {
        yield return new WaitForSeconds(respawnTime);
        for (int i = 0; i < bombMaxSpawnCount; i++)
        {
            Instantiate(bomb, new Vector3(Random.Range(-8.35f, 8.35f), 6), quaternion.identity);
        }

        if (coinGenTime >= 10)
        {
            coinGenTime = 0;
            Instantiate(coin, new Vector3(Random.Range(-8.35f, 8.35f), -2), quaternion.identity);
        }
        coinGenTime++;
        StartCoroutine(BombSpawn());
    }
}
