using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public bool gameStart = false;
    private Vector3 pivot;
    private Rigidbody rb;
    public int speed;
    private float zRot = 0, rotSpeed = 110, yRot = 0, moveOffset;
    private Vector2 touchPosition;
    public int health = 15;
    public ParticleSystem[] particleArray = new ParticleSystem[4];
    public UnityEvent GameOver, DamageTaken;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++) {
            particleArray[i].Stop();
        }
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update(){
        if (gameStart) {
            movement();
        }
    }

    private void movement() {
        //if (Input.touchCount > 0) {
        touchPosition = Input.mousePosition;///Input.GetTouch(0).position;
        moveOffset = touchPosition.x - Screen.width / 2;
        moveOffset *= 0.01f;
        if (moveOffset > 1) {
            moveOffset = 1;
        }
        if (moveOffset < -1) {
            moveOffset = -1;
        }
        //}
        //0.15
        transform.position += transform.forward * speed * Time.deltaTime;
        pivot = transform.position;
        
        yRot += Time.deltaTime * rotSpeed * moveOffset;

        if (moveOffset < 0.35 && moveOffset > -0.35) {
            zRot *= 0.95f;
        }
        else {
            zRot -= 75 * Time.deltaTime * moveOffset;
            if (zRot > 45) {
                zRot = 45;
            }
            if (zRot < -45) {
                zRot = -45;
            }
        }
        

        /*
        if (Input.GetKey("left")) {
            //turns plane (direction)
            yRot -= Time.deltaTime * rotSpeed;

            //left rotation
            if (zRot < 45) {
                zRot += 75 * Time.deltaTime;
            }

        }
        else if (Input.GetKey("right")) {
            //turns plane (direction)
            yRot += Time.deltaTime * rotSpeed;

            if (zRot > -45) {
                zRot -= 75 * Time.deltaTime;
            }
        }
        
        if (zRot != 0) {
            if (zRot > 0) {
                zRot -= 75 * Time.deltaTime;
            }
            if (zRot < 0) {
                zRot += 75 * Time.deltaTime;
            }
        }
        */
        transform.localRotation = Quaternion.Euler(0, yRot, zRot);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "enemyBullet") {
            health -= 5;
            Destroy(other.gameObject);
            if (!checkIfDead()) {
                Debug.Log("shake");
                DamageTaken.Invoke();
            }
            else {
                Debug.Log("no shake");
            }
            checkForParticles();
            Debug.Log(health);
        }
    }

    private bool checkIfDead() {
        if (health <= 0) {
            GameOver.Invoke();
            rb.useGravity = true;
            rb.isKinematic = false;
            return true;
        }
        return false;
    }

    private void checkForParticles() {
        if (health == 20) {
            particleArray[0].Stop();
            particleArray[0].Clear();
            particleArray[0].Play();
        }
        else if (health == 15 && (!particleArray[1].isPlaying)) {
            particleArray[0].Stop();
            particleArray[1].Play();
        }
        else if (health == 10 && (!particleArray[2].isPlaying)) {
            particleArray[2].Play();
        }
        else if (health == 5 && (!particleArray[3].isPlaying)) {
            particleArray[3].Play();
        }
    }

    public void startGame() {
        gameStart = true;
    }



    void OnDrawGizmosSelected()
    {
        //Gizmos.DrawSphere(pivot, 0.9f);
    }
}
