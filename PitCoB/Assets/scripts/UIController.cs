﻿using System.Collections;
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

    [SerializeField]
    Sprite[] reticles;
    [SerializeField]
    Image reticle;

    [SerializeField]
    Canvas canvas;

    Camera cam;
    StateController sc;

    public bool validToUse;

    // Use this for initialization
    void Awake() {
        sc = FindObjectOfType<StateController>();

        uiTools = new GameObject[4];
        uiTools = GameObject.FindGameObjectsWithTag("Tool UI");

        timerText = GameObject.FindGameObjectWithTag("Timer Text").GetComponent<Text>();
    }

    void Start () {
        cam = Camera.main;
        SwitchReticle(-1);
    }
	
	// Update is called once per frame
	void Update () {
        rot = (rot + wheelSpeed) % 360;

        if(wheel.gameObject.activeSelf)
            wheel.rotation = Quaternion.Euler(0, 0, rot);

        if (sc.loadedLevelObject)
            validToUse = sc.loadedLevelObject.GetComponent<LevelData>().Over;

        if (reticle.gameObject.activeSelf)
        {
            if (!validToUse)
                reticle.color = new Color(1f, 1f, 1f, 0.5f);
            else
                reticle.color = new Color(1f, 1f, 1f, 1f);

            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
            Vector3 tfp = canvas.transform.TransformPoint(pos);
            reticle.transform.position = new Vector2(
                Mathf.Clamp(tfp.x, reticle.rectTransform.rect.width, cam.pixelWidth - reticle.rectTransform.rect.width),
                Mathf.Clamp(tfp.y, reticle.rectTransform.rect.height, cam.pixelHeight - reticle.rectTransform.rect.height));
        }
    }

    public void SwitchReticle(int ret)
    {
        if (ret == -1)
        {
            reticle.sprite = null;
            reticle.gameObject.SetActive(false);
            return;
        }

        for (int i = 0; i < reticles.Length; i++)
        {
            if (i == ret)
            {
                reticle.sprite = reticles[i];

                reticle.gameObject.SetActive(true);

                reticle.rectTransform.sizeDelta =
                    new Vector2(reticles[i].textureRect.width, reticles[i].textureRect.height);
            }
        }
    }
}