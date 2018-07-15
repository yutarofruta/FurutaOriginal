using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour {

    private SpriteController[] sprites;
    public GameObject questionManager;

    // Use this for initialization
    void Start () {

        //動かすオブジェクトをすべて読み込み
        sprites = GetComponentsInChildren<SpriteController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeSpritesIsTouchable(bool b) {

        //読み込んだ全ての動かすオブジェクトに対し、Touchableを変更
        for (int i = 0; i < sprites.Length; i++) {
            sprites[i].isTouchable = b;
        }
    }
}
