using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelData : MonoBehaviour {

    public int[] toolCounts = new int[4];
    GameObject[] uiTools;

    [SerializeField]
    float timerMax;
    float timer;
    Text timerText;

    [SerializeField]
    int targetAlive;

    [SerializeField]
    float radiusX = 1f, radiusY = 1f;

    World w;
    StateController sc;

    [HideInInspector]
    public bool Over;

    [SerializeField]
    LayerMask mask;

    public void InitializeLevelData() {
        uiTools = UIController.uiTools;
        UpdateTools(true);

        timer = timerMax;
        timerText = UIController.timerText;

        sc = FindObjectOfType<StateController>();

        //Initialize the World
        w = FindObjectOfType<World>();
        InitWorld();

        Debug.Log("Level Data initialized.");
    }
	
	// Update is called once per frame
	void Update () {
        if (sc.currentState == States.Game) {
            if (timer > 0) {
                timer -= Time.deltaTime;

                //Draw the timer
                int minutes = Mathf.FloorToInt(timer / 60F);
                int seconds = Mathf.FloorToInt(timer - minutes * 60);
                timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);

                if (timer <= 0) {
                    //Level is over, evaluate win or loss.
                    timer = 0;
                    EndLevel(w.pirates.Count >= targetAlive);
                }
            }

            Over = CheckValidMouse();
        }
	}

    bool CheckValidMouse() {
        return (Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0f, mask));
    }

    void UpdateTools(bool hideZero = false) {
        for(int i = 0; i < uiTools.Length; i++) {
            if (hideZero && toolCounts[i] == 0)
                uiTools[i].SetActive(false);
            else
                uiTools[i].GetComponentInChildren<Text>().text = "" + toolCounts[i];
        }
    }

    void EndLevel(bool won = false) {
        w.ClearObjects();

        timerText.text = "0:00";
        if (won)
            sc.ChangeState(States.Win);
        else
            sc.ChangeState(States.Lose);
    }

    public void DecreaseToolCount(int tool) {
        toolCounts[tool]--;
        UpdateTools(false);
    }

    void InitWorld() {
        FindObjectOfType<Tools>().ld = this;

        //Reset the World.
        //Add this level's objects to the appropriate World lists.

        w.radiusX = radiusX;
        w.radiusY = radiusY;
        w.center = Vector2.zero;

        foreach(Skeleton s in GetComponentsInChildren<Skeleton>()) {
            w.skeletons.Add(s);
            s.world = w;
        }

        foreach(Pirate p in GetComponentsInChildren<Pirate>()) {
            w.pirates.Add(p);
            p.world = w;

        }

        foreach(Squid s in GetComponentsInChildren<Squid>()) {
            w.squids.Add(s);
            s.world = w;

        }

        foreach(Obstacle o in GetComponentsInChildren<Obstacle>()) {
            w.obstacles.Add(o);
        }

        Debug.Log("World initialized.");
    }
}
