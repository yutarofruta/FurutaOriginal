using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game6Character : MonoBehaviour {

    // スクロール速度
    private float runSpeed;

    //回復力
    private float recoverSpeed = 0.5f;

    public GameObject questionManager;

    // Use this for initialization
    void Start () {
        runSpeed = -0.01f;
	}
	
	// Update is called once per frame
	void Update () {

        //少しづつ後ろに下がっていく
        transform.Translate(this.runSpeed, 0, 0);
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
