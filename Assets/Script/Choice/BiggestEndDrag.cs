using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggestEndDrag : MonoBehaviour {

    private GameObject questionManager;
    private GameObject activeCharacter;
    private GameObject basket;

    private bool isTouchable;
    private bool isCorrect;
    private string answerTag;

    private void Start() {
        questionManager = GameObject.Find("QuestionManager");
        basket = questionManager.GetComponent<BiggestQuestionManager>().basket;
    }

    public void CheckIfCorrect() {

        //触れるかどうか判断
        isTouchable = gameObject.GetComponent<ChoiceController>().isTouchable;

        //触れないならここでリターン
        if (!isTouchable) {
            return;
        }

        //ChoiceControllerから正解かを取得する
        isCorrect = gameObject.GetComponent<ChoiceController>().isCorrect;

        //ドラッグが終わったと時に正解だったら停止。正解でなければSpringJointで戻る
        if (isCorrect) {

            gameObject.GetComponent<ChoiceController>().isTouchable = false;
            transform.position = basket.transform.position + new Vector3(0, 1f, 0);

            //activeCharacterを取得して、PlayerStateをJumpに変えることでアニメーションさせる
            activeCharacter = questionManager.GetComponent<BiggestQuestionManager>().activeCharacter;
            activeCharacter.GetComponent<GoBackCharacter>().GoNextState();
        }
        else {
            gameObject.GetComponent<SpringJoint2D>().enabled = true;
        }

    }
}
