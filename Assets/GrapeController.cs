using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeController : MonoBehaviour {

    Animator anim;
    AnimatorStateInfo animInfo;

    public GameObject spriteManager;

    public enum PlayerState {
        WAIT,
        PLAY,
        CLEAR
    }

    public PlayerState playerState;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        playerState = PlayerState.WAIT;
    }

    // Update is called once per frame
    void Update () {
		switch(playerState) {
            case PlayerState.WAIT:

                //スプライトのタッチを禁止
                spriteManager.GetComponent<SpriteManager>().ChangeSpritesIsTouchable(false);
                anim.Play("Enter");
                animInfo = this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
                if(animInfo.normalizedTime >= 1.0f) {
                    GoNextState();
                }
                break;

            case PlayerState.PLAY:

                //スプライトのタッチを許可
                spriteManager.GetComponent<SpriteManager>().ChangeSpritesIsTouchable(true);
                break;

            case PlayerState.CLEAR:

                //スプライトのタッチを禁止
                //spriteManager.GetComponent<SpriteManager>().ChangeSpritesIsTouchable(false);
                anim.Play("Jump");
                animInfo = this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
                if (animInfo.normalizedTime >= 1.0f) {
                    GoNextState();
                }
                break;
        }
	}

    public void GoNextState() {
        if (playerState == PlayerState.WAIT) playerState = PlayerState.PLAY;
        if (playerState == PlayerState.PLAY) playerState = PlayerState.CLEAR;
        if (playerState == PlayerState.CLEAR) playerState = PlayerState.WAIT;
    }

}
