using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1Character : GoBackCharacter {

    //GoNextQuestion
    public override void GoNextQuestion() {
        base.GoNextQuestion();

        questionManager.GetComponent<SelectingQuestionManager>().GoNextQuestion();
    }
}
