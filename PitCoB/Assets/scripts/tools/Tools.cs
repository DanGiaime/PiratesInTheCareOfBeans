﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tools : MonoBehaviour {

    [SerializeField]
    Image[] toolUIs; 
    [SerializeField]
    Sprite[] toolUIBacks;

    [SerializeField]
    GameObject[] toolObjects;

    [SerializeField]
    Image reticle;

    public int toolSelected = -1;

    StateController sc;
    UIController uic;
    public LevelData ld;

    [SerializeField]
    LayerMask skeletonMask;

	// Use this for initialization
	void Start () {
        sc = FindObjectOfType<StateController>();
        uic = FindObjectOfType<UIController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (toolSelected != -1 && ld && ld.toolCounts[toolSelected] > 0 && Input.GetMouseButtonDown(0) && uic.validToUse) {
            UseSelectedTool();
        }
    }

    public void UseSelectedTool()
    {
        Debug.Log("Used tool");
        ld.DecreaseToolCount(toolSelected);
        
        GameObject obj = Instantiate(toolObjects[toolSelected], Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);

        switch (toolSelected) {
            //Bean bag
            case 0:
                sc.GetComponent<World>().bags.Add(obj.GetComponent<Bag>());
                sc.GetComponent<World>().obstacles.Add(obj.GetComponent<Bag>());
                obj.GetComponent<Bag>().target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                obj.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y + 10f);
                break;

            //Projectile bean
            case 1:
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1f, skeletonMask);
                Debug.Log(hit.collider);
                if (hit.collider && hit.collider.GetComponent<Skeleton>()) {
                    hit.collider.GetComponent<Skeleton>().Die();
                }

                break;

            //Box
            case 2:
                sc.GetComponent<World>().boxes.Add(obj.GetComponent<Box>());
                sc.GetComponent<World>().obstacles.Add(obj.GetComponent<Box>());
                obj.GetComponent<Box>().target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                obj.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y + 10f);
                break;

            //Bean Bomb
            case 3:

                Instantiate(toolObjects[4], Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);

                obj.GetComponent<Bomb>().target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                sc.GetComponent<World>().obstacles.Add(obj.GetComponent<Bomb>());
                obj.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y + 70f);
                break;
        }

        if (ld.toolCounts[toolSelected] == 0)
            SwitchTool(-1);
    }

    /// <summary>
    /// Switch to the specified tool.
    /// </summary>
    /// <param name="tool">Tool index to switch to.</param>
    public void SwitchTool(int tool) {
        if (tool == -1 || (ld && ld.toolCounts[tool] > 0)) {
            uic.SwitchReticle(tool);

            if (toolSelected == tool) {
                SwitchTool(-1);
                return;
            }

            for (int i = 0; i < toolUIs.Length; i++) {
                if (i == tool) {
                    toolUIs[i].sprite = toolUIBacks[0];
                    toolUIs[i].rectTransform.sizeDelta =
                        new Vector2(toolUIBacks[0].textureRect.width, toolUIBacks[0].textureRect.height);
                } else {
                    toolUIs[i].sprite = toolUIBacks[1];
                    toolUIs[i].rectTransform.sizeDelta =
                        new Vector2(toolUIBacks[1].textureRect.width, toolUIBacks[1].textureRect.height);
                }
            }

            toolSelected = tool;
        }
    }
}
