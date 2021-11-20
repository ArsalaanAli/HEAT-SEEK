using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    private Transform target;
    public float rotSpeed;
    public float speed;
    public GameObject player, missileExplosion;
    public ParticleSystem trail;
    // Start is called before the first frame update
    void Start()
    {
        target = player.transform;

        Destroy(this.gameObject, 10f);
        trail.Stop();
        trail.Clear();
        trail.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //Changing rotation towards player
        Vector3 targetDirection = target.position - transform.position;
        float stepSize = rotSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, stepSize, 0);
        transform.rotation = Quaternion.LookRotation(newDirection);

        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnDestroy() {
        Instantiate(missileExplosion, transform.position, Quaternion.identity);
    }
}
