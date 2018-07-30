using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game3Character : GoBackCharacter {
    
    //GoNextQuestion
    public override void GoNextQuestion() {
        base.GoNextQuestion();

        questionManager.GetComponent<BiggestQuestionManager>().GoNextQuestion();
    }

    


}

