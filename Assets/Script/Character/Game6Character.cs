using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game6Character : MonoBehaviour {

    private float runSpeed = -0.01f;     //スクロール速度
    private float recoverSpeed = 1f;      //回復力
    private float currentPos;       //現在地

    public bool isScroll;       //キャラクターをスクロールするかどうか

    public GameObject questionManager;

    // Use this for initialization
    void Start () {
        isScroll = true;
	}
	
	// Update is called once per frame
	void Update () {

        if(isScroll) {
            //少しずつ後ろに下がる
            transform.Translate(this.runSpeed, 0, 0);

            //アニメーション切り替えのためのcurrentPosを指定する
            currentPos = transform.position.x;
            gameObject.GetComponent<Animator>().SetFloat("currentPos", currentPos);
        }
    }

    //正解したら少し前にでる
    public void Recover() {
        transform.Translate(this.recoverSpeed, 0, 0);
    }

    //ワニにぶつかったらゲームオーバー
    private void OnCollisionEnter2D(Collision2D collision) {
        questionManager.GetComponent<RunningQuestionManager>().GameOver();
    }
}
