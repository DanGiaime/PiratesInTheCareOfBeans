using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public static GameObject[] uiTools;
    public static Text timerText;

    [SerializeField]
    RectTransform wheel;
    [SerializeField]
    float wheelSpeed;
    float rot;

    // Use this for initialization
    void Start () {
        uiTools = new GameObject[4];
        uiTools = GameObject.FindGameObjectsWithTag("Tool UI");

        timerText = GameObject.FindGameObjectWithTag("Timer Text").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        rot = (rot + wheelSpeed) % 360;

        if(wheel.gameObject.activeSelf)
            wheel.rotation = Quaternion.Euler(0, 0, rot);
	}
}
