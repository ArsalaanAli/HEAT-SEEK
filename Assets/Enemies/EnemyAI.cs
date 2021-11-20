using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    private Transform target;
    public float rotSpeed = 1f, speed, zRot = 0;
    public int health = 10;
    private float timeToShoot = 5f, searchTime = 5f;
    public GameObject Missile, Explosion;
    private bool hasTarget = false, alive = true;
    private Vector3 searchPos;
    private Rigidbody rb;



    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody>();
        target = player.transform;
        searchPos = new Vector3(Random.Range(-240, 240), 0, Random.Range(-240, 240));
    }

    // Update is called once per frame
    void Update(){
        if (alive) {
            hasTarget = checkForTarget();
            if (hasTarget) {
                follow();
            }
            else {
                search();
            }
            shoot();
            //moving forward
        }
        transform.position += transform.forward * speed * Time.deltaTime;
        if (!alive) {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position + rb.velocity), Time.deltaTime);
        }

    }

    private void search() {
        searchTime -= Time.deltaTime;
        if (searchTime <= 0) {
            searchPos = new Vector3(Random.Range(-240, 240), 0, Random.Range(-240, 240));
            searchTime = 5f;
        }
        else {
            float stepSize = rotSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, searchPos, stepSize, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    private void follow() {
        //Changing rotation towards player
        Vector3 targetDirection = target.position - transform.localPosition;
        float stepSize = rotSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, stepSize, 0);
        newDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(newDirection);


        //Rolling Plane In Movement Direction
        float targetRotation = (180 / Mathf.PI) * Mathf.Atan2(targetDirection.x, targetDirection.z);
        targetRotation = (targetRotation < 0) ? targetRotation + 360 : targetRotation;
        float rotChange = targetRotation - transform.rotation.eulerAngles.y;
        if ((rotChange > 180 && rotChange < 355 || rotChange < -5) && zRot < 45) {
            zRot += 70 * Time.deltaTime;
        }
        else if (rotChange > 5 && rotChange < 180 && zRot > -45) {
            zRot -= 70 * Time.deltaTime;
        }
        else {
            if (zRot > 0) {
                zRot -= 70 * Time.deltaTime;
            }
            if (zRot < 0) {
                zRot += 70 * Time.deltaTime;
            }
        }

        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, zRot);

    }
    

    private void OnTriggerEnter(Collider other) {
        if (alive) {
            if (other.tag == "enemyBullet") {
                Destroy(other.gameObject);
                Instantiate(Explosion, transform.position, Quaternion.identity);
                alive = false;
                Destroy(this.gameObject, 5f);
                rb.useGravity = true;
                rb.isKinematic = false;
                FindObjectOfType<General>().addScore();
            }
        }
    }
    private void shoot() {
        timeToShoot -= Time.deltaTime;
        Vector3 targetDirection = target.position - transform.position;
        if(timeToShoot <= 0) {
            GameObject bullet = Instantiate(Missile, transform.position + transform.forward*2, Quaternion.LookRotation(targetDirection));
            bullet.GetComponent<MissileController>().player = player;
            timeToShoot = 5f;
        }

    }
    private bool checkForTarget() {
        if (Vector3.Distance(transform.position, target.transform.position) <= 100) {
            return true;
        }
        else {
            return false;
        }
    }


}
