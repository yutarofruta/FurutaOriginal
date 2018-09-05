using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackCharacter : MonoBehaviour {

    Animator anim;
    AnimatorStateInfo animInfo;
    Rigidbody2D rigid2D;

    public GameObject choiceManager;
    public GameObject questionManager;

    protected GameObject target;      //停止場所
    protected float stopDistance;     //停止する距離

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
    protected virtual void Start() {

        anim = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();

        //spriteManagerとquestionManagerを取得
        choiceManager = GameObject.Find("ChoiceManager");
        questionManager = GameObject.Find("QuestionManager");

        //WAITから開始
        playerState = PlayerState.WAIT;
    }


    // Update is called once per frame
    void Update() {
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

    //WAITステイト中にやること
    public virtual void WaitState() {
        
        //スプライトのタッチを禁止
        choiceManager.GetComponent<ChoiceManager>().ChangeSpritesIsTouchable(false);

        if (target != null) {

            //キャラクターからターゲットまでの距離の絶対値
            float diff = Mathf.Abs(target.transform.position.x - transform.position.x);
            Debug.Log(diff);

            //targetの近くまで歩き、十分近くなったら次のStateへ
            if (diff >= stopDistance) {
                transform.Translate(0.1f, 0, 0, Space.Self);
            }
            else {
                GoNextState();

                //スプライトのタッチを許可
                choiceManager.GetComponent<ChoiceManager>().ChangeSpritesIsTouchable(true);
            }

        }
    }

    //PLAYステイト中にやること
    public virtual void PlayState() {
        Debug.Log("PlayState is called");

    }

    //CLEARステイト中にやること
    public virtual void ClearState() {

        //スプライトのタッチを禁止
        choiceManager.GetComponent<ChoiceManager>().ChangeSpritesIsTouchable(false);

        bool isGround = (rigid2D.velocity.y == 0) ? true : false;

        //三回ジャンプさせ終わったら、逆向きにして次のステートに移る
        if (isGround) {
            if (jumpNum < 3) {
                rigid2D.AddForce(transform.up * 100f);
                jumpNum++;
            }
            else {
                jumpNum = 0;
                GoNextState();
                transform.Rotate(0, 180f, 0);
            }
        }
    }

    //LEAVEステイト中にやること
    public virtual void LeaveState() {
        
        //画面から出たら果物を消して、WAITに移る
        if (GetComponent<Renderer>().isVisible) {
            transform.Translate(0.1f, 0, 0, Space.Self);
        }
        else {
            GoNextState();
            Destroy(gameObject);
        }
    }

    //targetと停止する距離を指定する
    public void SetMoveTarget(GameObject target, float stopDistance) {

        //ターゲットと停止場所確定
        this.target = target;
        this.stopDistance = stopDistance;

        //もしtargetより右にいれば左向きにする
        if (target.transform.position.x - transform.position.x < 0) {
            transform.Rotate(0, 180f, 0);
        }

    }

    //次のステイトに移る
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
