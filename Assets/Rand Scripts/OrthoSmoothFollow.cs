using UnityEngine;
using System.Collections;

public class OrthoSmoothFollow : MonoBehaviour
{

    public Transform target;
    private float smoothTime = 0.30003f;
    private float shakeDuration = 0;
    public float ShakeAmount;

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        Vector3 goalPos = target.position - transform.forward*20;
        goalPos.y = transform.position.y;
        transform.position = Vector3.SmoothDamp(transform.position, goalPos, ref velocity, smoothTime);
        if (shakeDuration > 0) {
            transform.position = transform.position + Random.insideUnitSphere * ShakeAmount;
            shakeDuration -= Time.deltaTime;
        }
    }

    public void triggerShake() {
        shakeDuration = 0.2f;
    }

}