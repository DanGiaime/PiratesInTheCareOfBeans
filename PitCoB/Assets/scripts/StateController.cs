using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States { Menu, Game, GameOver, LevelSelect };

public class StateController : MonoBehaviour {

    States currentState;

    [SerializeField]
    States startingState;

    public GameObject loadedLevelObject;

    [SerializeField]
    GameObject[] uiObjects;

	// Use this for initialization
	void Start () {
        ChangeState(startingState);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Change the game state.
    /// </summary>
    /// <param name="state">State to change to.</param>
    public void ChangeState(States state) {
        //Change UI Behavior
        for(int i = 0; i < uiObjects.Length; i++)
                uiObjects[i].SetActive((int)state == i);

        //Level object cleanup
        if(currentState == States.Game) {
            if (state != States.GameOver && loadedLevelObject)
                Destroy(loadedLevelObject);
        }

        if(currentState == States.GameOver && loadedLevelObject)
            Destroy(loadedLevelObject);

        currentState = state;
    }
}
