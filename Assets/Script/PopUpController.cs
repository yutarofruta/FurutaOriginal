using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpController : MonoBehaviour {

    public GameObject[] levelButton;        //1~5までのレベルのボタン
    public GameObject gameManager;      //GameManager

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    //表示するレベルの設定
    public void LevelButtonSet(int levelButtonNum) {

        //現在押されているgameNameを取得する
        string gameName = gameManager.GetComponent<GameManager>().gameName;

        //levelButtonNum個だけレベルを表示する
        for (int i = 0; i < levelButtonNum; i++) {
            levelButton[i].SetActive(true);

            //押されたゲーム名に対応するclearLevelをGameManagerから読み込み、それ以下のボタンを触れるようにする
            if(i < GameManager.openLevelDic[gameName]) {
                levelButton[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    //ポップアップ上の戻るボタンを押すと、一度レベルのボタンをリセットする
    public void AllButtonFalse() {
        for(int i = 0; i < levelButton.Length; i++) {
            levelButton[i].SetActive(false);
        }
    }


}
