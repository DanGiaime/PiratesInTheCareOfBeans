using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tools : MonoBehaviour {

    [SerializeField]
    Image[] toolUIs; 
    [SerializeField]
    Sprite[] toolUIBacks;

    [SerializeField]
    Image reticle;

    public int toolSelected = -1;

    StateController sc;
    UIController uic;

	// Use this for initialization
	void Start () {
        sc = FindObjectOfType<StateController>();
        uic = FindObjectOfType<UIController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            UseSelectedTool();
        }
    }

    public void UseSelectedTool()
    {
        switch (toolSelected)
        {
            //Bean bag
            case 0:

                break;

            //Bean projectile
            case 1:

                break;

            //Box
            case 2:

                break;

            //Bean Bomb
            case 3:

                break;
        }
    }

    /// <summary>
    /// Switch to the specified tool.
    /// </summary>
    /// <param name="tool">Tool index to switch to.</param>
    public void SwitchTool(int tool) {
        uic.SwitchReticle(tool);

        if(toolSelected == tool) {
            SwitchTool(-1);
            return;
        }

        for (int i = 0; i < toolUIs.Length; i++)
        {
            if (i == tool)
            {
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
