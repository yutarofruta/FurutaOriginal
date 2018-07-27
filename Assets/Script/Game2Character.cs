using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2Character : CharacterManager {

    //キャラクターが停止する場所
    private Transform leftTarget;
    private Transform rightTarget;

    protected override void Start() {
        base.Start();

        //targetを取得
        leftTarget = GameObject.Find("LeftTarget").transform;
        rightTarget = GameObject.Find("RightTarget").transform;
    }

    //WAIT Stateでやること
    public override void WaitState() {
        base.WaitState();

        //スプライトのタッチを禁止
        choiceManager.GetComponent<ChoiceManager>().ChangeSpritesIsTouchable(false);

        //定位置に移動・定位置まで移動したらPLAYに移る
        if(gameObject.tag == "leftCharacter" && transform.position.x < leftTarget.position.x) {
            transform.Translate(0.1f, 0, 0);
        }
        else if(gameObject.tag == "rightCharacter" && transform.position.x >= rightTarget.position.x) {
            transform.Translate(-0.1f, 0, 0);
        }
        else {
            GoNextState();

            //スプライトのタッチを許可
            choiceManager.GetComponent<ChoiceManager>().ChangeSpritesIsTouchable(true);
        }
    }

    //PLAY Stateでやること
    public override void PlayState() {
        base.PlayState();

        
    }

    //CLEAR Stateでやること
    public override void ClearState() {
        base.ClearState();

        //スプライトのタッチを禁止
        choiceManager.GetComponent<ChoiceManager>().ChangeSpritesIsTouchable(false);

        bool isGround = (rigid2D.velocity.y == 0) ? true : false;

        //三回ジャンプさせる
        if (isGround) {
            if (jumpNum < 3) {
                rigid2D.AddForce(transform.up * 100f);
                jumpNum++;
            }
            else {
                //スプライトを反転
                transform.Rotate(0, 180, 0);
                jumpNum = 0;
                GoNextState();
            }
        }
    }

    //LEAVE Stateでやること
    public override void LeaveState() {
        base.LeaveState();

        //画面から出たら果物を消して、WAITに移る
        if (GetComponent<Renderer>().isVisible && gameObject.tag == "leftCharacter") {
            transform.Translate(0.1f, 0, 0);
        }
        else if (GetComponent<Renderer>().isVisible && gameObject.tag == "rightCharacter") {
            transform.Translate(-0.1f, 0, 0);
        }
        else {
            GoNextState();
            Destroy(gameObject);
        }
    }

    //GoNextQuestion
    public override void GoNextQuestion() {
        base.GoNextQuestion();

        //改善必要？
        if(gameObject.tag == "leftCharacter") {
            questionManager.GetComponent<GroupingQuestionManager>().GoNextQuestion();
        }
    }
}
