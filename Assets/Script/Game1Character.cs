using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1Character : CharacterManager {

    //キャラクターが停止する場所
    private Transform target;

    //削除されるスプライト
    public GameObject clearedSprite;

    protected override void Start() {
        base.Start();

        //targetを取得
        target = GameObject.Find("Target").transform;
    }

    //WAIT Stateでやること
    public override void WaitState() {
        base.WaitState();

        //スプライトのタッチを禁止
        choiceManager.GetComponent<ChoiceManager>().ChangeSpritesIsTouchable(false);

        //定位置に移動・定位置まで移動したらPLAYに移る
        if (gameObject.transform.position.x > target.transform.position.x) {
            transform.Translate(-0.1f, 0, 0);
        }
        else {
            GoNextState();
        }
    }

    //PLAY Stateでやること
    public override void PlayState() {
        base.PlayState();

        //スプライトのタッチを許可
        choiceManager.GetComponent<ChoiceManager>().ChangeSpritesIsTouchable(true);
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
        if (GetComponent<Renderer>().isVisible) {
            transform.Translate(-0.1f, 0, 0);
        }
        else {
            Destroy(clearedSprite);
            GoNextState();
        }
    }

    //GoNextQuestion
    public override void GoNextQuestion() {
        base.GoNextQuestion();

        questionManager.GetComponent<SelectingQuestionManager>().GoNextQuestion();
    }


}
