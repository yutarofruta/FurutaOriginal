using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpController : MonoBehaviour {

    public GameObject[] levelButton;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LevelButtonSet(int levelButtonNum) {

        for (int i = 0; i < levelButtonNum; i++) {
            levelButton[i].SetActive(true);
        }
    }

    public void AllButtonFalse() {

        for(int i = 0; i < levelButton.Length; i++) {
            levelButton[i].SetActive(false);
        }
    }


}
