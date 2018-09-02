using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandHappyCharacter : MonoBehaviour {

    Animator anim;
    AnimatorStateInfo animInfo;

    public GameObject questionManager;

    public enum PlayerState {
        PLAY,   //回答中
        CLEAR,   //正解アニメーション中
        WRONG,   //不正解アニメーション中
    }

    public PlayerState playerState;

    // Use this for initialization
    protected virtual void Start() {

        anim = GetComponent<Animator>();

        //questionManagerを取得
        questionManager = GameObject.Find("QuestionManager");

        //PLAYから開始
        playerState = PlayerState.PLAY;
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(playerState);

        switch (playerState) {

            case PlayerState.PLAY:

                PlayState();
                break;

            case PlayerState.CLEAR:

                ClearState();
                break;
        }
    }

    public void PlayState() {
        //IdleアニメーションができたらここからPlayする
    }

    public void ClearState() {

        //現在のアニメーションステイトを記録
        animInfo = anim.GetCurrentAnimatorStateInfo(0);

        //アニメーションが終わっていれば次のステイトに移る(次の問題のIdleに移る)
        if (!animInfo.IsName("GrapeHappy")) {
            GoNextState();
            Debug.Log("GoNextState");
        }
    }

    //次のステイトに移る
    public void GoNextState() {
        if (playerState == PlayerState.PLAY) {
            playerState = PlayerState.CLEAR;

            //喜ぶアニメーションを実行
            anim.SetTrigger("HappyTrigger");

            return;
        }
        else if (playerState == PlayerState.CLEAR) {
            playerState = PlayerState.PLAY;

            //重複しないように片側のキャラクターからのみGoNextQuestionを呼び出す
            if(gameObject.tag == "leftCharacter") {
                questionManager.GetComponent<SeparatingQuestionManager>().GoNextQuestion();
            }

            //Idleのアニメーションを実行
            //anim.SetTrigger("PlayTrigger");

            return;
        }
    }

}
