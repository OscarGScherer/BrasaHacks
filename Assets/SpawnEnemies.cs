using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int number;
    public int radius;
    private List<GameObject> enemies = new List<GameObject>();
    // Start is called before the first frame update

    private void Start()
    {
        Spawn();
    }
    public void Spawn()
    {
        for(int i = 0; i<number; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab,
                new Vector2(transform.position.x + Random.Range(radius * -1, radius),
                transform.position.y + Random.Range(radius * -1, radius)), transform.rotation);
            enemies.Add(enemy);
        }
    }

    public void ReSpawn()
    {
        for (int i = 0; i < GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().killCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab,
                new Vector2(transform.position.x + Random.Range(radius * -1, radius),
                transform.position.y + Random.Range(radius * -1, radius)), transform.rotation);
            enemies.Add(enemy);
        }
    }
}
