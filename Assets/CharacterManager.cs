using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

    Animator anim;
    AnimatorStateInfo animInfo;

    public GameObject spriteManager;
    public GameObject questionManager;

    //削除されるスプライト
    public GameObject clearedSprite;

    public enum PlayerState {
        WAIT,   //問題準備アニメーション中
        PLAY,   //回答中
        CLEAR   //正解アニメーション中
    }

    public PlayerState playerState;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        playerState = PlayerState.WAIT;
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log(playerState);

		switch(playerState) {
            case PlayerState.WAIT:

                //スプライトのタッチを禁止
                spriteManager.GetComponent<SpriteManager>().ChangeSpritesIsTouchable(false);

                //EnterAnimationが終了したらPLAYに移動
                if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Enter")) {
                    GoNextState();
                }
                break;

            case PlayerState.PLAY:

                //スプライトのタッチを許可
                spriteManager.GetComponent<SpriteManager>().ChangeSpritesIsTouchable(true);
                break;

            case PlayerState.CLEAR:

                //スプライトのタッチを禁止
                spriteManager.GetComponent<SpriteManager>().ChangeSpritesIsTouchable(false);

                //Idle,Jump,Leaveのすべてのアニメーションが終わったらWAITに戻る
                animInfo = this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
                if ((!animInfo.IsName("Idle") && !animInfo.IsName("Jump")) && !animInfo.IsName("Leave")) {
                    GoNextState();
                }
                break;
        }
	}

    public void GoNextState() {
        if (playerState == PlayerState.WAIT) {
            playerState = PlayerState.PLAY;
            return;
        }
        else if (playerState == PlayerState.PLAY) {
            playerState = PlayerState.CLEAR;
            anim.SetTrigger("JumpTrigger");
            Debug.Log("JumpTrigger");
            return;
        }
        else if (playerState == PlayerState.CLEAR) {
            playerState = PlayerState.WAIT;
            Destroy(clearedSprite);
            //次の問題に移る
            questionManager.GetComponent<Questions>().qNum++;
            return;
        }
    }

}
