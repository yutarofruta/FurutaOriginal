using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

    Animator anim;
    AnimatorStateInfo animInfo;
    Rigidbody2D rigid2D;

    public GameObject spriteManager;
    public GameObject questionManager;

    //削除されるスプライト
    public GameObject clearedSprite;

    //キャラクターの入場・退場
    private float stopPos = 6f;

    //ジャンプ用のカウント
    private int jumpNum = 0;

    public enum PlayerState {
        WAIT,   //問題準備アニメーション中
        PLAY,   //回答中
        JUMP,   //正解アニメーション中
        LEAVE   //退場アニメーション中
    }

    public PlayerState playerState;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();

        //spriteManagerとquestionManagerを取得
        spriteManager = GameObject.Find("SpriteManager");
        questionManager = GameObject.Find("QuestionManager");


        //WAITから開始
        playerState = PlayerState.WAIT;
    }

    // Update is called once per frame
    void Update () {
        Debug.Log(playerState);

        switch (playerState) {
            case PlayerState.WAIT:

                //スプライトのタッチを禁止
                spriteManager.GetComponent<SpriteManager>().ChangeSpritesIsTouchable(false);

                //定位置に移動・定位置まで移動したらPLAYに移る
                if (gameObject.transform.position.x > stopPos) {
                    transform.Translate(-0.1f, 0, 0);
                }
                else {
                    GoNextState();
                }

                break;

            case PlayerState.PLAY:

                //スプライトのタッチを許可
                spriteManager.GetComponent<SpriteManager>().ChangeSpritesIsTouchable(true);
                break;

            case PlayerState.JUMP:

                //スプライトのタッチを禁止
                spriteManager.GetComponent<SpriteManager>().ChangeSpritesIsTouchable(false);

                bool isGround = (rigid2D.velocity.y == 0) ? true : false;

                //三回ジャンプさせる
                if (isGround) {
                    if(jumpNum < 3) {
                        rigid2D.AddForce(transform.up * 100f);
                        jumpNum++;
                        Debug.Log(jumpNum);
                    } else {
                        //スプライトを反転
                        transform.Rotate(0, 180, 0);
                        jumpNum = 0;
                        GoNextState();
                    }
                }

                break;

            case PlayerState.LEAVE:

                //定位置に移動・定位置まで移動したらPLAYに移る
                if (gameObject.transform.position.x < 10) {
                    transform.Translate(-0.1f, 0, 0);
                }
                else {
                    GoNextState();
                }


                break;
        }
    }

    public void GoNextState() {
        if (playerState == PlayerState.WAIT) {
            anim.SetTrigger("IdleTrigger");
            playerState = PlayerState.PLAY;
            return;
        }
        else if (playerState == PlayerState.PLAY) {
            playerState = PlayerState.JUMP;

            //ジャンプのアニメーションを実行
            anim.SetTrigger("JumpTrigger");

            Debug.Log("JumpTrigger");
            return;
        }
        else if (playerState == PlayerState.JUMP) {
            playerState = PlayerState.LEAVE;

            //退場のアニメーションを実行
            anim.SetTrigger("LeaveTrigger");

            Debug.Log("LeaveTrigger");
            return;
        }
        else if (playerState == PlayerState.LEAVE) {
            playerState = PlayerState.WAIT;
            Destroy(clearedSprite);

            //入場のアニメーションを実行
            anim.SetTrigger("EnterTrigger");

            //次の問題に移る
            questionManager.GetComponent<QuestionManager>().GoNextQuestion();
            return;
        }
    }
}
