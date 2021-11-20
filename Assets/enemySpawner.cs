using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject player;

    // Start is called before the first frame update
    public void Commence()
    {
        spawnEnemy(0, -12);
        InvokeRepeating("spawnEnemy", 10f, 10f);
    }

    private void spawnEnemy() {
        //Vector3 spawnPos = new Vector3(Random.Range(-240, 240), 0, Random.Range(-240, 240));
        Vector3 spawnPos = new Vector3(player.transform.position.x + randNeg(27), 0, player.transform.position.z + randNeg(70));
        GameObject instance = Instantiate(Enemy, spawnPos, Quaternion.identity);
        Debug.Log(player);
        instance.GetComponent<EnemyAI>().player = player;
    }
    private void spawnEnemy(int x, int z) {
        Vector3 spawnPos = new Vector3(x, 0, z);
        GameObject instance = Instantiate(Enemy, spawnPos, Quaternion.identity);
        Debug.Log(player);
        instance.GetComponent<EnemyAI>().player = player;
    }
    private int randNeg(int a) {
        int b = a * (Random.Range(-1, 1));
        if (b == 0) {
            return a;
        }
        return b;
    }

}
