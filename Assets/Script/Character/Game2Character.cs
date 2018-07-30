using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2Character : GoBackCharacter {

    //GoNextQuestion
    public override void GoNextQuestion() {
        base.GoNextQuestion();

        //両方のキャラクターからGoNextされないようにする
        if(gameObject.tag == "leftCharacter") {
            questionManager.GetComponent<GroupingQuestionManager>().GoNextQuestion();
        }
    }
}
