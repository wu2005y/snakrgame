using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    public GameObject applePrefab;
    public Vector2Int spawnRange = new Vector2Int(9, 9);

    void Start()
    {
        SpawnApple();
    }

    public void SpawnApple()
    {
        Vector3 position = new Vector3(Random.Range(-spawnRange.x, spawnRange.x + 1), Random.Range(-spawnRange.y, spawnRange.y + 1), 0);
        Instantiate(applePrefab, position, Quaternion.identity);
    }
}
