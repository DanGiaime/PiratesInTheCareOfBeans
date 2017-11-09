using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DynamicSpriteSorter : MonoBehaviour {

    [SerializeField]
    bool recalcOnUpdate;

    [SerializeField]
    int orderOverride;

    // Use this for initialization
    void Start() {
        Recalculate();
    }

    // Update is called once per frame
    void Update() {
        if (recalcOnUpdate)
            Recalculate();
    }

    void Recalculate() {
        transform.position = new Vector3(transform.position.x, transform.position.y, -orderOverride + transform.position.y);
    }
}
