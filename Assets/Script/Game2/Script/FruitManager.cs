using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour {

    private FruitController[] fruits;     
    public GameObject questionManager;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeSpritesIsTouchable(bool b) {

        //動かすオブジェクトをすべて読み込み
        fruits = GetComponentsInChildren<FruitController>();

        //読み込んだ全ての動かすオブジェクトに対し、Touchableを変更
        for (int i = 0; i < fruits.Length; i++) {
            fruits[i].isTouchable = b;
        }
    }
}
