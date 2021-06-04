using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HitHandler(int WeaponDamage);

public class SpawnManager : MonoBehaviour {
    [SerializeField] float limit = 45f;
    [SerializeField] EnemyController[] enemies;

    private CharacterController player;
    private List<List<EnemyController>> enemyPool = new List<List<EnemyController>>();

    private int wave = 1;
    private int poolSize = 10;
    private int enemyCounter = 0;
    private bool waveSpawning;

    public event HitHandler onHit;
    
    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("Player").GetComponent<CharacterController>();
        CreateEnemyPool();

        // start the waves
        SpawnWave();
    }

    // Update is called once per frame
    void Update() {
    }

    void SpawnWave () {
        // signals that a wave is currently spawning
        waveSpawning = true;

        // rules: spawn random enemies, max wavecount/2 red and max wavecount/4 green
            int maxRed = wave/2;
            int maxGreen = wave/4;

            int numRed = 0;
            int numGreen = 0;

            int waveLength = (int)(Mathf.Pow(1.1f, wave) + wave);

        for (int i = 0; i < waveLength; i++) {
            // when all enemies in current wave is killed
            int index = Random.Range(0, enemies.Length);

            if (index == 1 && numRed < maxRed) {
                // Red spawns in a cluster of 3
                for (int j=0;j<3;j++) {  
                    SpawnEnemy(index);
                }
            } else if (index == 2 && numGreen < maxGreen) {
                SpawnEnemy(index);
            } else {
                SpawnEnemy(0);
            }
        }

        // signals that the wave is done spawning
        wave++;
        waveSpawning = false;
    }

    void SpawnEnemy (int index) {
        Vector3 spawnPos = new Vector3(Random.Range(- limit, limit), 1, Random.Range(- limit, limit));
        
        EnemyController enemy = getEnemy(index);
        if (enemy != null) {
            enemy.gameObject.transform.position = spawnPos;
            enemy.gameObject.SetActive(true);
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
}
