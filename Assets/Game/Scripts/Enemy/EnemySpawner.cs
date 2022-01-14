using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyBase enemyObjectSmall;
    [SerializeField] private EnemyBase enemyObjectMedium;
    [SerializeField] public Transform enemiesParent;
    [SerializeField] private List<Transform> enemyLocations;
    [SerializeField] public List<GameObject> enemies;
    
    public int enemyToSpawnCount = 0;
    private static EnemySpawner instance;
    public static EnemySpawner Instance => instance ?? (instance = FindObjectOfType<EnemySpawner>());
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        
    }

    public void SpawnEnemy()
    {
        enemyToSpawnCount = GameManager.Instance.currentWave % 3 == 1 ? enemyToSpawnCount + 5 : enemyToSpawnCount;
        enemyToSpawnCount = enemyToSpawnCount == enemyLocations.Count ? enemyLocations.Count : enemyToSpawnCount;
        for(int i = 0; i < enemyToSpawnCount; i++)
        {
            var spawnLocation = (Vector2)(transform.position+enemyLocations[i].position);
            spawnLocation.x = Random.Range(-3, 3);
            var enemyToSpawn = GameManager.Instance.currentWave > 5 ? enemyObjectMedium : enemyObjectSmall;
            EnemyBase spawnedEnemy = Instantiate(enemyToSpawn, spawnLocation, Quaternion.identity, enemiesParent);
            enemies.Add(spawnedEnemy.gameObject);
            StartCoroutine(MoveToPositionRoutine(spawnedEnemy, (Vector2)enemyLocations[i].position));
        }
    }

    private IEnumerator MoveToPositionRoutine(EnemyBase enemy,Vector2 targetPos)
    {
        while (true)
        {
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, targetPos, Time.deltaTime * 4f);
            var distanceLeft = ((Vector2)enemy.transform.position - targetPos).magnitude;

            if(distanceLeft < 0.01f)
            {
                Debug.Log("Moved to position");
                enemy.SetCenter(targetPos);
                enemy.startMovement = true;
                break;
            }
            yield return null;
        }
    }


}
