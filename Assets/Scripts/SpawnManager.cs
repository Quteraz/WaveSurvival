using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public delegate void HitHandler(int WeaponDamage);

public class SpawnManager : MonoBehaviour {
    [SerializeField] float limit = 2.5f;
    [SerializeField] EnemyController[] enemies;
    [SerializeField] TextMeshProUGUI waveText;

    private List<GameObject> spawnpoints =  new List<GameObject>();
    private List<List<EnemyController>> enemyPool = new List<List<EnemyController>>();

    private int wave = 1;
    private int poolSize = 10;
    private int enemyCounter = 0;
    private bool waveSpawning;

    public event HitHandler onHit;
    
    // Start is called before the first frame update
    void Start() {
        GetSpawnpoints();
        CreateEnemyPool();

        // start the waves
        StartCoroutine(SpawnWave());
    }

    // Update is called once per frame
    void Update() {
    }

    IEnumerator SpawnWave() {
        // signals that a wave is currently spawning
        waveText.text = "Wave: " + wave;
        waveSpawning = true;



        // rules: spawn random enemies, max wavecount/2 red and max wavecount/4 green
            int maxRed = wave/2;
            int maxGreen = wave/4;

            int numRed = 0;
            int numGreen = 0;

            int waveLength = (int)(Mathf.Pow(1.1f, wave) + wave);

        for (int i = 0; i < waveLength; i++) {
            // wait for 5 seconds before spawning next enemy
            yield return new WaitForSeconds(5);
            // Spawn random enemy at random spawnpoint
            int index = Random.Range(0, enemies.Length);
            int spawnIndex = Random.Range(0, spawnpoints.Count);

            if (index == 1 && numRed < maxRed) {
                // Red spawns in a cluster of 3
                for (int j=0;j<3;j++) {  
                    SpawnEnemy(index, spawnIndex);
                }
                numRed++;
            } else if (index == 2 && numGreen < maxGreen) {
                SpawnEnemy(index, spawnIndex);
                numGreen++;
            } else {
                SpawnEnemy(0, spawnIndex);
            }
            Debug.Log("Enemy spawned: " + enemyCounter);
        }
        // end of wave
        Debug.Log("end of wave");
        wave++;
        waveSpawning = false;
    }

    void SpawnEnemy (int index, int spawnIndex) {
        Vector3 spawnOffset = new Vector3(Random.Range(- limit, limit), 0, Random.Range(- limit, limit));
        
        EnemyController enemy = getEnemy(index);
        if (enemy != null) {
            Vector3 spawnPos = spawnpoints[spawnIndex].transform.position;
            enemy.gameObject.transform.position = spawnPos + spawnOffset;
            enemy.gameObject.SetActive(true);
            enemy.EnemyKilled -= EnemyCounter;
            enemy.EnemyKilled += EnemyCounter;
            enemyCounter++;
        }
    }

    EnemyController getEnemy (int index) {
        for (int i = 0; i < poolSize; i++) {
            if (!enemyPool[index][i].gameObject.activeInHierarchy) {
                return enemyPool[index][i];
            }
        }
        return null;
    }

    void EnemyCounter () {
        enemyCounter--;
        Debug.Log("enemy killed: " + waveSpawning + " : " + enemyCounter);
        if (!waveSpawning && enemyCounter == 0) {
            StartCoroutine(SpawnWave());
        }
    }

    void CreateEnemyPool() {
        for (int i = 0; i < enemies.Length; i++) {
            enemyPool.Add(new List<EnemyController>());
            for (int j = 0; j < poolSize; j++) {
                EnemyController enemy = Instantiate(enemies[i], transform);
                enemy.gameObject.SetActive(false);
                enemyPool[i].Add(enemy);
            }
        }
    }

    void GetSpawnpoints () {
        int len = transform.childCount;
        for (int i = 0; i < len; i++) {
            spawnpoints.Add(transform.GetChild(i).gameObject);
        }
    }
}
