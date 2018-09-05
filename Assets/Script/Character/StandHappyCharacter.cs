using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandHappyCharacter : MonoBehaviour {

    Animator anim;
    AnimatorStateInfo animInfo;

    public GameObject questionManager;

    private bool isPlaying = true;      //PlayState中かどうか

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


        //アニメーションが終わっていれば次のステイトに移る(次の問題のIdleに移る)
        StartCoroutine("CheckAnimInfo");

        
    }

    //次のステイトに移る
    public void GoNextState() {
        if (playerState == PlayerState.PLAY) {
            playerState = PlayerState.CLEAR;

            //喜ぶアニメーションを実行
            anim.SetTrigger("HappyTrigger");

            //PlayState中でないとする
            isPlaying = false;

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

    private IEnumerator CheckAnimInfo() {

        yield return new WaitForSeconds(0.1f);

        animInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (!animInfo.IsName("GrapeHappy") && !isPlaying) {
            GoNextState();
            Debug.Log("GoNextState");
            isPlaying = true;
        }

    }

}
