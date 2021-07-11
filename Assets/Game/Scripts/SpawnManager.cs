using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_EnemyPrefabs;
    [SerializeField] private List<GameObject> m_EnemySpawnLocations;
    private int m_Level;
    private int m_EnemiesLeft;

    void Start()
    {
        m_Level = 4;
        spawnEnemies();
    }

    private void spawnEnemies()
    {
        int randomEnemyIndex, randomEnemyLocaition;
        m_Level++;
        List<GameObject> spawnPoints = new List<GameObject>(m_EnemySpawnLocations);
        for (int i = 0; i < m_Level; i++)
        {
            if(spawnPoints.Count == 0)
            {
                break;
            }

            randomEnemyIndex = UnityEngine.Random.Range(0, m_EnemyPrefabs.Count);
            randomEnemyLocaition = UnityEngine.Random.Range(0, spawnPoints.Count);
            Instantiate(m_EnemyPrefabs[randomEnemyIndex], spawnPoints[randomEnemyLocaition].transform.position, m_EnemyPrefabs[randomEnemyIndex].transform.rotation);
            spawnPoints.Remove(spawnPoints[randomEnemyLocaition]);
        }

        m_EnemiesLeft = m_Level;
    }

    public void NotifyOnEnemyDeath()
    {
        m_EnemiesLeft--;

        if(m_EnemiesLeft == 0)
        {
            spawnEnemies();
        }
    }
}
