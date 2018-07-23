﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingEndDrag: MonoBehaviour {

    private GameObject goal;
    private GameObject questionManager;

    private GameObject activeCharacter;

    private bool isTouchable;
    private bool isCorrect;

    private void Start() {
        goal = GameObject.Find("Goal");
        questionManager = GameObject.Find("QuestionManager");

        isTouchable = gameObject.GetComponent<ChoiceController>().isTouchable;
    }

    public void CheckIfCorrect() {

        //触れないならここでリターン
        if (!isTouchable) {
            return;
        }

        //ChoiceControllerから正解かを取得する
        isCorrect = gameObject.GetComponent<ChoiceController>().isCorrect;

        //ドラッグが終わったと時に正解だったら停止。正解でなければSpringJointで戻る
        if (isCorrect) {
            gameObject.GetComponent<ChoiceController>().isTouchable = false;
            transform.position = goal.transform.position;

            //activeCharacterを取得して、PlayerStateをJumpに変えることでアニメーションさせる
            activeCharacter = questionManager.GetComponent<SelectingQuestionManager>().activeCharacter;
            activeCharacter.GetComponent<CharacterManager>().GoNextState();

            //今の果物choiceを削除するものに指定する
            activeCharacter.GetComponent<CharacterManager>().clearedSprite = this.gameObject;
        }
        else {
            gameObject.GetComponent<SpringJoint2D>().enabled = true;
        }

    }
}
