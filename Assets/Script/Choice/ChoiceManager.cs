using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour {

    private ChoiceController[] choices;
    public GameObject questionManager;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeSpritesIsTouchable(bool b) {

        //動かすオブジェクトをすべて読み込み
        choices = GetComponentsInChildren<ChoiceController>();

        //読み込んだ全ての動かすオブジェクトに対し、Touchableを変更
        for (int i = 0; i < choices.Length; i++) {
            choices[i].isTouchable = b;
        }
    }
}
