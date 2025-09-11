using UnityEngine;

public class SnakeSpawner : MonoBehaviour
{
    [SerializeField] 
    private GameObject snakePrefab;

    [SerializeField] 
    private float spawnInterval = 2f; // seconds between spawns

    [SerializeField] 
    private Vector2 spawnAreaMin = new Vector2(-8f, -4f); // bottom-left corner

    [SerializeField] 
    private Vector2 spawnAreaMax = new Vector2(8f, 4f);  // top-right corner

    private float _nextSpawnTime;

    void Update()
    {
        if (Time.time >= _nextSpawnTime)
        {
            SpawnSnake();
            _nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnSnake()
    {
        // Pick a random position within the defined area
        float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPos = new Vector2(x, y);

        Instantiate(snakePrefab, spawnPos, Quaternion.identity);
    }
}