using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States { Menu, Game, Win, Lose, LevelSelect };

public class StateController : MonoBehaviour {

    public States currentState;
    public int currentLevel;

    [SerializeField]
    GameObject[] levelObjects;

    [SerializeField]
    States startingState;

    public GameObject loadedLevelObject;

    [SerializeField]
    GameObject[] uiObjects;

    AudioSource a;
    [SerializeField]
    AudioClip coinSound;

    World w;

	// Use this for initialization
	void Start () {
        a = GetComponent<AudioSource>();
        ChangeState(startingState);
        w = GetComponent<World>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Change the game state.
    /// </summary>
    /// <param name="state">State to change to.</param>
    public void ChangeState(States state) {
        ResetCameraPosition();

        //Change UI Behavior
        for (int i = 0; i < uiObjects.Length; i++)
                uiObjects[i].SetActive((int)state == i);

        //Level object cleanup
        if(currentState == States.Game) {
            if (state != States.Lose && state != States.Win && loadedLevelObject) {
                Destroy(loadedLevelObject);
                w.ClearObjects();
            }
        } else
            a.PlayOneShot(coinSound);

        if ((currentState == States.Lose || currentState == States.Win) && loadedLevelObject) {
            w.ClearObjects();
            Destroy(loadedLevelObject);
        }

        currentState = state;

       //Debug.Log("Changed game state to " + state);
    }

    /// <summary>
    /// Overload for Canvas buttons
    /// </summary>
    /// <param name="state">Integer state name</param>
    public void ChangeState(int state) {
        ChangeState((States)state);
    }

    /// <summary>
    /// Loads a level object.
    /// </summary>
    /// <param name="level">The index of the level object to load.</param>
    public void LoadLevel(int level) {
        ChangeState(States.Game);

        GameObject levelObj = Instantiate(levelObjects[level], Vector3.zero, Quaternion.identity);
        loadedLevelObject = levelObj;
        levelObj.GetComponent<LevelData>().InitializeLevelData();

        currentLevel = level;

        //Debug.Log("Level " + currentLevel + " was loaded.");
    }

    /// <summary>
    /// Loads the next level.
    /// </summary>
    public void LoadNextLevel() {
        if(currentLevel + 1 < levelObjects.Length)
            LoadLevel(currentLevel + 1);
    }

    /// <summary>
    /// Reload the current level.
    /// </summary>
    public void ResetLevel() {
        LoadLevel(currentLevel);

        //Debug.Log("Level " + currentLevel + " was reloaded.");
    }

    /// <summary>
    /// Quit the app from a Canvas UI button.
    /// </summary>
    public void QuitApp() {
        Application.Quit();

        //Debug.Log("App was quit.");
    }

    void ResetCameraPosition() {
        Camera.main.transform.position = new Vector3(0f, 0f, Camera.main.transform.position.z);
    }
}