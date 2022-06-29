using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Spawn
{
    public GameObject prefab;
    public float weight;
}

public class CombatDirector : MonoBehaviour
{
    /// <summary>
    /// Class responsible for spawning enemies.
    /// This is based on our temperature
    /// </summary>
    
    [Header("Objects to Spawn")]
    [SerializeField] Spawn[] spawns = default;

    [Header("Spawner Area")]
    [SerializeField] Vector2 levelBounds = Vector2.one;

    public int tempR;
    public int howManyEnemies;
    public bool spawnDone;

    //Once we obtain the forecast information, we set our
    //enemy spawn amount.
    public void Update()
    {
        if (RealWorldWeather.current.main != null)
        {
            tempR = ((int)RealWorldWeather.current.temperature);
            howManyEnemies = tempR / 10;

            if(spawnDone == false)
            {
                SpawnPrefabs(howManyEnemies);
            }
        }


    }

    private void Start()
    {
        spawnDone = false;

    }

    
    public void SpawnPrefabs(int num)
    {
        for (int i = 0; i < num; i++)
        {
            SpawnPrefab();

            if(i == num-1)
                spawnDone = true;

        }

    }

    //We set our boolean to true in order to stop
    //enemies from continuing to spawn
    public void SpawnPrefab()
    {
        Vector3 origin = new Vector3(Random.Range(-levelBounds.x, levelBounds.x) + 70, 10, Random.Range(-levelBounds.y, levelBounds.y) + 40);
        Spawn spawn = GetRandomSpawn();
        GameObject enemy = Instantiate(spawn.prefab, origin, Quaternion.identity, transform);

    }

    public bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * 10;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

   
    Spawn GetRandomSpawn()
    {
        float sum = 0;
        float randomWeight = 0;
        foreach (Spawn spawn in spawns)
        {
            sum += spawn.weight;
        }
        do
        {
            if (sum == 0)
                return null;
            randomWeight = Random.Range(0, sum);
        }
        while (randomWeight == sum);
        foreach (Spawn spawn in spawns)
        {
            if (randomWeight < spawn.weight)
                return spawn;
            randomWeight -= spawn.weight;
        }
        return null;
    }

}