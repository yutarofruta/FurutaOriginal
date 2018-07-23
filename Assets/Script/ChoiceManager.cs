using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour {

    private ChoiceController[] sprites;
    public GameObject questionManager;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeSpritesIsTouchable(bool b) {

        //動かすオブジェクトをすべて読み込み
        sprites = GetComponentsInChildren<ChoiceController>();

        //読み込んだ全ての動かすオブジェクトに対し、Touchableを変更
        for (int i = 0; i < sprites.Length; i++) {
            sprites[i].isTouchable = b;
        }
    }
}
