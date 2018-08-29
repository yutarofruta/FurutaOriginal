using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RunningQuestionManager : MonoBehaviour {

    public Text finText;    //ゲーム終了時に出すテキスト
    public Text qText;      //How many?と表示するテキスト
    private int qNum = 1;    　//問題番号
    private int maxNum;     //問題数
    private bool isFinished = false;        //ゲーム終了
    private bool isGamwOver = false;        //ゲームオーバー

    public GameObject fruit;        //果物のプレハブ
    public GameObject[] activeFruits;       //画面上にある果物
    public GameObject[] backGrounds;

    public Button[] buttons;   //選択肢用のボタン

    private RunningQuestionObject[] runningQuestions;      //Questionオブジェクトのプレハブ

    public GameObject character;        //走らせるキャラクター

    // Use this for initialization
    void Start() {

        //問題を読み出す
        ReadQuestion();

        //出題順をシャッフル
        //QuestionShuffle();

        //問題の数を取得
        maxNum = runningQuestions.Length;

        //一問目を表示
        GoNextQuestion();
    }

    // Update is called once per frame
    void Update() {

        //ゲームメニューに戻る
        if (isFinished && Input.GetMouseButtonDown(0) || isGamwOver && Input.GetMouseButtonDown(0)) {
            SceneManager.LoadScene("GameMenu");
        }
    }

    public void GoNextQuestion() {

        //一問目でなければ、前の果物を消去する
        if (qNum != 1) {
            for (int i = 0; i < activeFruits.Length; i++) {
                Destroy(activeFruits[i]);
            }
        }

        if (qNum <= maxNum) {      //問題出題中

            //今の対応する問題のデータを持ったquestionObjectを決める
            RunningQuestionObject questionObject = runningQuestions[qNum - 1];

            //activeFruits[]の要素数と果物の数を対応
            activeFruits = new GameObject[questionObject.fruitNum];

            //果物を生成する
            for (int i = 0; i < questionObject.fruitNum; i++) {
                activeFruits[i] = Instantiate(fruit, questionObject.fruitPos[i], Quaternion.identity);
                activeFruits[i].GetComponent<SpriteRenderer>().sprite = questionObject.fruit;
                activeFruits[i].GetComponent<SpriteRenderer>().sortingOrder = 1;
            }

            //ボタンを全てinteractiveにする
            for (int i = 0; i < buttons.Length; i++) {
                buttons[i].GetComponent<Button>().interactable = true;
            }
        }

        //全部の問題が終わったら
        if (qNum > maxNum) {

            //キャラクターと背景のスクロール停止
            StopScroll();

            //キャラクターにジャンプさせる
            character.GetComponent<Animator>().SetTrigger("RunningClear");

            //問題文を消す
            finText.GetComponent<Text>().text = "AWESOME!";

            //ボタンをすべて非表示にする
            for (int i = 0; i < buttons.Length; i++) {
                buttons[i].gameObject.SetActive(false);
            }

            //ゲーム終了
            isFinished = true;

            //次のレベルを遊べるようにする
            if (GameManager.openLevelDic["Game6"] == GameManager.levelNum) {
                GameManager.openLevelDic["Game6"] = GameManager.levelNum + 1;
                Debug.Log("レベルを開放する");
            }
        }

        //問題番号を1増やす
        qNum++;
    }

    //ゲームオーバー状態にして、スクロールを止め、GAMEOVERテキストを表示する
    public void GameOver() {
        isGamwOver = true;
        finText.GetComponent<Text>().text = "GAMEOVER";
        StopScroll();
    }

    //スクロールをやめる
    public void StopScroll() {
        character.GetComponent<Game6Character>().isScroll = false;
        for (int i = 0; i < backGrounds.Length; i++) {
            backGrounds[i].GetComponent<BackGroundController>().isScroll = false;
        }
        qText.GetComponent<Text>().text = "";
    }

    //questionObjectをシャッフルする
    public void QuestionShuffle() {
        for (int index1 = 0; index1 < runningQuestions.Length; index1++) {
            int index2 = Random.Range(0, runningQuestions.Length);
            QuestionSwap(ref runningQuestions[index1], ref runningQuestions[index2]);
        }
    }

    // question1 と questioon2 を入れ替える
    private void QuestionSwap(ref RunningQuestionObject question1, ref RunningQuestionObject question2) {
        RunningQuestionObject question = question1;
        question1 = question2;
        question2 = question;
    }

    public void ReadQuestion() {

        //シーン名を取得する
        string sceneName = SceneManager.GetActiveScene().name;

        //Resourcesからlevelに対応する問題を読みだして、objectArrayに入れる
        object[] objectArray = Resources.LoadAll(sceneName + "_" + GameManager.levelNum.ToString(), typeof(RunningQuestionObject));

        //selectingQuestionの配列の大きさを、呼び出した問題の配列数と揃える
        System.Array.Resize(ref runningQuestions, objectArray.Length);

        //objectArrayの中身をselectingQuestionに入れる
        for (int i = 0; i < objectArray.Length; i++) {
            runningQuestions[i] = (RunningQuestionObject)objectArray[i];
        }
    }
}
