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
    float radius;

    World w;
    StateController sc;

    [HideInInspector]
    public bool Over;

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
        }
	}

    private void OnMouseEnter() { Over = true; }

    private void OnMouseExit() { Over = false; }

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

    void InitWorld() {
        //Reset the World.
        //Add this level's objects to the appropriate World lists.

        w.radius = radius;

        Debug.Log("World initialized.");
    }
}
