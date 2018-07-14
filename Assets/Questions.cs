using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questions : MonoBehaviour {

    public Text qText;
    public int qNum = 1;
    public string answerTag;

    private void Start() {
        
    }

    private void Update() {
        if(qNum == 1) {
            qText.GetComponent<Text>().text = "Which is ONE?";
            answerTag = "one";
        }
        else if (qNum == 2) {
            qText.GetComponent<Text>().text = "Which is TWO?";
            answerTag = "two";
        }
        else if(qNum == 3) {
            qText.GetComponent<Text>().text = "Which is THREE?";
            answerTag = "three";
        }
    }



}
