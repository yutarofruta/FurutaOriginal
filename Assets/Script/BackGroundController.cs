using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour {

    private float scrollSpeed = -0.03f;     // スクロール速度
    private float deadLine = -20;    // 背景終了位置
    private float startLine = 20f;    // 背景開始位置

    public bool isScroll = true;        //背景をスクロールするかどうか

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {

        // 背景を移動する
        if(isScroll) {
            transform.Translate(this.scrollSpeed, 0, 0);
        }

        // 画面外に出たら、画面右端に移動する
        if (transform.position.x < this.deadLine) {
            transform.position = new Vector2(this.startLine, 1.5f);
        }
    }

}
