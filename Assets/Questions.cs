using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questions : MonoBehaviour {

    public Text qText;
    public int qNum = 1;
    public string answerTag;

    public GameObject goal;

    public Sprite grapeFruit;
    public Sprite cherryFruit;
    public Sprite melonFruit;

    private void Start() {
        
    }

    private void Update() {

        if(qNum == 1) {//一問目

            qText.GetComponent<Text>().text = "Which is Grape?";
            goal.GetComponent<SpriteRenderer>().sprite = grapeFruit;
            answerTag = "grape";

        }
        else if (qNum == 2) {//二問目

            qText.GetComponent<Text>().text = "Which is Cherry?";
            goal.GetComponent<SpriteRenderer>().sprite = cherryFruit;
            answerTag = "cherry";

        }
        else if(qNum == 3) {//三問目

            qText.GetComponent<Text>().text = "Which is Melon?";
            goal.GetComponent<SpriteRenderer>().sprite = melonFruit;
            answerTag = "melon";

        }
    }



}
