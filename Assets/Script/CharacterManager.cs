using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

    Animator anim;
    AnimatorStateInfo animInfo;
    protected Rigidbody2D rigid2D;

    public GameObject choiceManager;
    public GameObject questionManager;

    //ジャンプ用のカウント
    protected int jumpNum = 0;

    public enum PlayerState {
        WAIT,   //問題準備アニメーション中
        PLAY,   //回答中
        CLEAR,   //正解アニメーション中
        LEAVE   //退場アニメーション中
    }

    public PlayerState playerState;

    // Use this for initialization
    protected virtual void Start () {

        anim = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();

        //spriteManagerとquestionManagerを取得
        choiceManager = GameObject.Find("ChoiceManager");
        questionManager = GameObject.Find("QuestionManager");

        //WAITから開始
        playerState = PlayerState.WAIT;
    }

    public virtual void WaitState() {

    }
    public virtual void PlayState() {

    }
    public virtual void ClearState() {

    }
    public virtual void LeaveState() {

    }

    // Update is called once per frame
    void Update () {
        //Debug.Log(playerState);

        switch (playerState) {
            case PlayerState.WAIT:

                WaitState();
                break;

            case PlayerState.PLAY:

                PlayState();                
                break;

            case PlayerState.CLEAR:

                ClearState();
                break;

            case PlayerState.LEAVE:

                LeaveState();
                break;
        }
    }

    public void GoNextState() {
        if (playerState == PlayerState.WAIT) {
            playerState = PlayerState.PLAY;

            //アイドルのアニメーションを実行
            anim.SetTrigger("IdleTrigger");
            
            return;
        }
        else if (playerState == PlayerState.PLAY) {
            playerState = PlayerState.CLEAR;

            //ジャンプのアニメーションを実行
            anim.SetTrigger("JumpTrigger");

            return;
        }
        else if (playerState == PlayerState.CLEAR) {
            playerState = PlayerState.LEAVE;

            //退場のアニメーションを実行
            anim.SetTrigger("LeaveTrigger");

            return;
        }
        else if (playerState == PlayerState.LEAVE) {
            playerState = PlayerState.WAIT;

            //入場のアニメーションを実行
            anim.SetTrigger("EnterTrigger");

            //次の問題に移る
            GoNextQuestion();
            return;
        }
    }

    public virtual void GoNextQuestion() {

    } 
}
