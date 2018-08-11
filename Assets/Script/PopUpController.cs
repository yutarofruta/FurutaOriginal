using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopUpController : MonoBehaviour {

    public GameObject[] levelButton;        //1~5までのレベルのボタン
    public GameObject gameManager;      //GameManager

    private void Start() {
        gameManager = GameObject.Find("GameManager");
        gameObject.SetActive(false);
    }

    //GameManagerのPopUpSetActive()を呼び出す
    public void CallPopUpSetActive(string game) {
        gameManager.GetComponent<GameManager>().PopUpSetActive(game);
    }

    //表示するレベルの設定
    public void LevelButtonSet(int levelButtonNum) {

        //現在押されているgameNameを取得する
        string gameName = gameManager.GetComponent<GameManager>().gameName;

        //levelButtonNum個だけレベルを表示する
        for (int i = 0; i < levelButtonNum; i++) {
            levelButton[i].SetActive(true);

            //押されたゲーム名に対応するclearLevelをGameManagerから読み込み、それ以下のボタンを触れるようにする
            if (i < GameManager.openLevelDic[gameName]) {
                levelButton[i].GetComponent<Button>().interactable = true;
            }
            else {
                levelButton[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    //ポップアップ上の戻るボタンを押すと、一度レベルのボタンをリセットしポップアップを消す
    public void AllButtonFalse() {
        for (int i = 0; i < levelButton.Length; i++) {
            levelButton[i].SetActive(false);
        }
        gameObject.SetActive(false);
    }

    //押されたゲームの[level]レベルに遷移
    public void LoadGame(int level) {
        gameManager = GameObject.Find("GameManager");
        GameManager.levelNum = level;
        SceneManager.LoadScene(gameManager.GetComponent<GameManager>().gameName);
    }

}
