using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bean : MonoBehaviour {
    [SerializeField]
    float rotationSpeed, scaleSpeed;
    float zRot;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        zRot += rotationSpeed;

        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, scaleSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, zRot);
    }
}
