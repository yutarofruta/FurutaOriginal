using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunningButtonController : MonoBehaviour {

    public bool isTouchable = true;

    public GameObject questionManager;
    public GameObject character;

    public Button[] options;

    //クリックされたボタンが正しいか判定する
    public void CheckIfCorrect(int buttonNum) {

        //クリックされたボタンを取得
        Button clickedButton = options[buttonNum];

        //クリックされたボタンの子Textを取得
        string getText = clickedButton.gameObject.GetComponentInChildren<Text>().text;

        //表示されている果物の個数をstringにして取得
        string answerText = questionManager.GetComponent<RunningQuestionManager>().activeFruits.Length.ToString();

        //ボタンの子Textと果物の数が等しければ、キャラクターを少し前に出して次の問題に移る
        if (getText == answerText) {

            //キャラクターを少し前に出す
            character.GetComponent<Game6Character>().Recover();

            //ボタンを暗くする
            for (int i = 0; i < options.Length; i++) {
                options[i].GetComponent<Button>().interactable = false;
            }

            //1秒したら次の問題に移る
            Invoke("CallGoNextQuestion", 1);
        }
        else {
            //character.GetComponent<Animator>().SetTrigger("WrongTrigger");
        }
    }

    public void CallGoNextQuestion() {
        questionManager.GetComponent<RunningQuestionManager>().GoNextQuestion();
    }
}
