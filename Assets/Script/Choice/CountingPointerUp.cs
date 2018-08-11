using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountingPointerUp : MonoBehaviour {

    public bool isTouchable = true;

    private GameObject gameManager;

    private void Start() {

        gameManager = GameObject.Find("GameManager");
    }

    public void PointerDown() {

        string getText = gameObject.GetComponent<Text>().text;

        string answerText = gameManager.GetComponent<CountingQuestionManager>().activeFruits.Length.ToString();

        if(getText == answerText) {
            gameManager.GetComponent<CountingQuestionManager>().GoNextQuestion();
        }
    }
}
