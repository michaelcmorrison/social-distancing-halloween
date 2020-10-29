using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject kidPrefab;
    public GameObject parentPrefab;

    public Transform[] kidSpawnPoints;
    public Transform[] parentSpawnPoints;

    public int maxKids;
    public int maxParents;
    
    public float kidSpawnRate;
    public float parentSpawnRate;

    public static List<Kid> Kids = new List<Kid>();
    public static List<Parent> Parents = new List<Parent>();
    
    private float _kidSpawnRefresh;
    private float _parentSpawnRefresh;
    
    private void Update()
    {
        if (Time.time > _kidSpawnRefresh && Kids.Count < maxKids)
        {
            SpawnKid();
        }

        if (Time.time > _parentSpawnRefresh && Parents.Count < maxParents)
        {
            SpawnParent();
        }
    }

    private void SpawnParent()
    {
        _parentSpawnRefresh = Time.time + parentSpawnRate;
        var parent = Instantiate(parentPrefab, GetRandomSpawnPoint(parentSpawnPoints).position, Quaternion.identity);
        parent.GetComponent<Parent>().moveSpeed = GetRandomSpeed();
    }

    private void SpawnKid()
    {
        _kidSpawnRefresh = Time.time + kidSpawnRate;
        var kid = Instantiate(kidPrefab, GetRandomSpawnPoint(kidSpawnPoints).position, Quaternion.identity);
        kid.GetComponent<Kid>().moveSpeed = GetRandomSpeed();
    }

    private Transform GetRandomSpawnPoint(Transform[] spawnPoints)
    {
        var rand = Random.Range(0, spawnPoints.Length);
        return spawnPoints[rand];
    }

    private float GetRandomSpeed()
    {
        var rand = Random.Range(4f, 6f);
        return rand;
    }
}
