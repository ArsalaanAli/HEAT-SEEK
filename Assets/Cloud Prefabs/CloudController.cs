using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    public GameObject[] clouds = new GameObject[2];
    public Transform centerScreen;
    // Start is called before the first frame update
    void Start()
    {
        cloudGen();
        //InvokeRepeating("cloudGen", 0, 6f);
    }
    void cloudGen() {
        for (int i = -3; i <= 2; i++) {
            for (int j = -3; j <= 2; j++) {
                GameObject currentCloud = clouds[Random.Range(0, 2)];
                Vector3 cloudSpawnLocation = new Vector3(centerScreen.position.x + j * Random.Range(20, 50), Random.Range(-10f, 5f), centerScreen.position.z + i * Random.Range(20, 50));
                Instantiate(currentCloud, cloudSpawnLocation, Quaternion.identity);
            }

        }
        
    }
}
