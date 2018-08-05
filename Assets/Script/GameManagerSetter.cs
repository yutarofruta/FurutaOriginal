using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSetter : MonoBehaviour {

    public GameObject gameManagerPrefab;        //GameManagerのプレハブ
    private GameObject gameManager;     //インスタンス化したgameManager
    private bool isCreated;     //GameManagerオブジェクトが既に生成されたかどうか

    private void Start() {
        isCreated = GameManager.created;        //GameManagerが生成されているかどうか確認

        //もしまだ生成していなければGameManagerを生成する
        if (!isCreated) {
            gameManager = Instantiate(gameManagerPrefab) as GameObject;
        }
    }
}
