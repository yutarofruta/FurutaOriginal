using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountingButtonController : MonoBehaviour {

    public bool isTouchable = true;

    public GameObject questionManager;
    public GameObject character;

    public Button[] options;

    //クリックさればボタンが正しいか判定する
    public void CheckIfCorrect(int buttonNum) {

        //クリックされたボタンを取得
        Button clickedButton = options[buttonNum];
        
        //クリックされたボタンの子Textを取得
        string getText = clickedButton.gameObject.GetComponentInChildren<Text>().text;

        //表示されている果物の個数をstringにして取得
        string answerText = questionManager.GetComponent<CountingQuestionManager>().activeFruits.Length.ToString();

        //キャラクターを取得
        character = questionManager.GetComponent<CountingQuestionManager>().character;

        //ボタンの子Textと果物の数が等しければ、キャラクターにジャンプさせて次の問題に移る
        if (getText == answerText) {

            character.GetComponent<Animator>().SetTrigger("JumpTrigger");

            //ボタンを暗くする
            for (int i = 0; i < options.Length; i++) {
                options[i].GetComponent<Button>().interactable = false;

            }

            //ジャンプのアニメーションが終わったら次の問題に移る
            //if() {}
            Invoke("CallGoNextQuestion", 2);
            //questionManager.GetComponent<CountingQuestionManager>().GoNextQuestion();

        }
        else {
            //character.GetComponent<Animator>().SetTrigger("WrongTrigger");
        }
    }

    public void CallGoNextQuestion() {
        questionManager.GetComponent<CountingQuestionManager>().GoNextQuestion();
    }
}
