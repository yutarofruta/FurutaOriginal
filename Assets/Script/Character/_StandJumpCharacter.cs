using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandJumpCharacter : MonoBehaviour {

    Animator anim;
    public AnimatorStateInfo animInfo;

    public GameObject choiceManager;
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

        //spriteManagerとquestionManagerを取得
        choiceManager = GameObject.Find("ChoiceManager");
        questionManager = GameObject.Find("QuestionManager");

        //WAITから開始
        playerState = PlayerState.PLAY;
    }

    // Update is called once per frame
    void Update() {
        //Debug.Log(playerState);

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

    }

    //次のステイトに移る
    public void GoNextState() {
        if (playerState == PlayerState.PLAY) {
            playerState = PlayerState.CLEAR;

            //ジャンプのアニメーションを実行
            anim.SetTrigger("JumpTrigger");

            return;
        }
        else if (playerState == PlayerState.CLEAR) {
            playerState = PlayerState.PLAY;

            //Idleのアニメーションを実行
            anim.SetTrigger("PlayTrigger");

            return;
        }
    }

}
