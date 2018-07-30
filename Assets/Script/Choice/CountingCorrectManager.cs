using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountingCorrectManager : MonoBehaviour {

    private GameObject questionManager;

    private bool isCorrect;
    private bool isTouchable;

    private void Start() {
        questionManager = GameObject.Find("QuestionManager");
    }

    public void CountingCorrect() {

        //今gameObjectを触れるか確認
        isTouchable = gameObject.GetComponent<ChoiceController>().isTouchable;

        //触れないならここでリターン
        if(!isTouchable) {
            return;
        }

        //ChoiceControllerから正解かを取得する
        isCorrect = gameObject.GetComponent<ChoiceController>().isCorrect;

        //ドラッグが終わったと時に正解だったら停止して正解カウントにプラス。正解でなければSpringJointで戻る
        if (isCorrect) {
            gameObject.GetComponent<ChoiceController>().isTouchable = false;

            //GroupingQuestionManagerのcorrectNumに加算する
            questionManager.GetComponent<GroupingQuestionManager>().AddCorrectNum();
        }
        else {
            gameObject.GetComponent<SpringJoint2D>().enabled = true;
        }
    }
}
