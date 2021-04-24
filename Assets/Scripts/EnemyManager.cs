using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField]
    private Enemy enemyPrototype;
    [SerializeField]
    private Target target;

    [SerializeField]
    private int numberOfWaves;
    [SerializeField]
    private int[] numberOfEnemies;
    [SerializeField]
    private float[] sporadicity, speedMod;

    private List<Enemy> enemies = new List<Enemy>();
    private int currentWave = 0;
    private bool waveStarted = true;

    private void Update() {

        // If waves remain
        if (currentWave < numberOfWaves) {

            // Spawn new wave if ready
            if((enemies.Count == 0) && waveStarted) {

                Debug.Log("Wave defeated.");
                waveStarted = false;
                StartCoroutine(PauseBetweenWaves());

            }
        }
    }

    private void CreateEnemy(Vector3 position, int damageModifier, float speed) {

        // Create enemy gameobject
        Enemy newEnemy = Instantiate(enemyPrototype, position, Quaternion.identity, transform);

        // Generate random string index
        int newIndex = Random.Range(0, StringTable.NumShallow - 1);

        // Set enemy variables
        newEnemy.StringIndex = newIndex;
        newEnemy.Target = target.transform.position;
        newEnemy.LettersPerHit = damageModifier;
        newEnemy.EnemyManager = this;
        
        // Add to list
        enemies.Add(newEnemy);
    }

    public void RemoveEnemy(Enemy enemy) {

        // Remove from list and destroy
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    private IEnumerator SpawnWave(int waveNumber) {

        // Loop through, spawning enemies
        for(int i = 0; i < numberOfEnemies[waveNumber]; i++) {

            // Random delay and speed
            float delay = Random.Range(0f, sporadicity[waveNumber]);
            float speed = Random.Range(1f, speedMod[waveNumber]);

            // Spawn enemy, then wait
            SpawnEnemy(speed);
            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator PauseBetweenWaves() {

        yield return new WaitForSeconds(1f);

        Debug.Log("Starting wave " + currentWave.ToString());
        
        yield return StartCoroutine(SpawnWave(currentWave));

        // Update variables
        currentWave++;
        waveStarted = true;
    }

    private void SpawnEnemy(float maxSpeed) {

        // Tune these values
        Vector2 spawnDistance = new Vector2(10f, 25f);
        float maxAngle = 30f;

        float speed = Random.Range(1f, maxSpeed);
        
        int orientation = (Random.Range(-1f, 1f) > 0f) ? 1 : -1;
        float spawnAngle = Random.Range(-maxAngle, maxAngle) * Mathf.Deg2Rad;
        Vector3 spawnPoint = orientation * Random.Range(spawnDistance.x, spawnDistance.y) *
            new Vector3(Mathf.Cos(spawnAngle), Mathf.Sin(spawnAngle), 0f);
        
        CreateEnemy(spawnPoint, 3, speed);
    }

}
