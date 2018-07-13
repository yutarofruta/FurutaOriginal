using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour {

    private SpriteController[] sprites;

    // Use this for initialization
    void Start () {
        sprites = GetComponentsInChildren<SpriteController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeSpritesIsTouchable(bool b) {

        for (int i = 0; i < sprites.Length; i++) {
            sprites[i].isTouchable = b;
            Debug.Log(b);
        }

    }
}
