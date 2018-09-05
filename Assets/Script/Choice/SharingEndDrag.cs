using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharingEndDrag : MonoBehaviour {

    private GameObject questionManager;

    private bool isTouchable;
    private bool isCorrect;

    private void Start() {
        questionManager = GameObject.Find("QuestionManager");
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

        //ドラッグが終わったと時に正解だったら停止しquestionManagerに加算する。正解でなければSpringJointで戻る
        if (isCorrect) {
            gameObject.GetComponent<ChoiceController>().isTouchable = false;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
            questionManager.GetComponent<SharingQuestionManager>().AddCorrectNum(gameObject);
            transform.position = gameObject.GetComponent<ChoiceController>().currentCollision.transform.position;
        }
        else {
            gameObject.GetComponent<SpringJoint2D>().enabled = true;
        }

    }
}
