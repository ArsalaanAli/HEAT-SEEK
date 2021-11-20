using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake() {
        camTransform = this.transform;
    }

    void Update() {

        if (shakeDuration > 0) {
            originalPos = camTransform.localPosition;
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }

    public void triggerShake() {
        shakeDuration = 0.5f;
    }
}