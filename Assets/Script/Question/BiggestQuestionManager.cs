using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BiggestQuestionManager : MonoBehaviour {

    private int qNum = 1;    　//問題番号
    private int maxNum;     //問題数(QuestionObjectの数)
    private float fruitScale;       //果物の拡大率
    private string fruitTag;    //果物のタグ
    public string answerTag;       //biggest か smallest
    public bool isFinished = false;     //ゲームが終了したか

    public BiggestQuestionObject[] biggestQuestions;      //BiggestQuestionオブジェクトのプレハブ

    public GameObject activeCharacter;     //現在のキャラクター
    public GameObject fruit;     //sprite入れる前の果物プレハブ
    public GameObject fruitsParent;     //Instanceする果物の親
    private GameObject[] fruits;     //果物を入れる
    public GameObject target;       //キャラクターが真ん中でストップするときの目印
    public GameObject basket;

    private Vector3[] fruitPos;      //果物の配置場所

    public Text qText;      //問題用のテキスト
    public Text finText;       //Awesomeのテキスト


    // Use this for initialization
    void Start () {
        //シーン名を取得する
        string sceneName = SceneManager.GetActiveScene().name;

        //Resourcesからlevelに対応する問題を読みだして、objectArrayに入れる
        object[] objectArray = Resources.LoadAll(sceneName + "_" + GameManager.levelNum.ToString(), typeof(BiggestQuestionObject));

        //selectingQuestionの配列の大きさを、呼び出した問題の配列数と揃える
        System.Array.Resize(ref biggestQuestions, objectArray.Length);

        //objectArrayの中身をselectingQuestionに入れる
        for (int i = 0; i < objectArray.Length; i++) {
            biggestQuestions[i] = (BiggestQuestionObject) objectArray[i];
        }

        //問題の数を取得
        maxNum = biggestQuestions.Length;

        //問題の順番入れ替え
        QuestionShuffle();

        //一問目を表示
        GoNextQuestion();
    }

    private void Update() {

        //ゲームメニューに戻る
        if(isFinished && Input.GetMouseButtonDown(0)) {
            SceneManager.LoadScene("GameMenu");
        }
    }

    public void GoNextQuestion() {

        //一問目でなければ、残った果物を消去する
        if (qNum != 1) {
            for (int i = 0; i < fruits.Length; i++) {
                Destroy(fruits[i]);
            }
        }

        if (qNum <= maxNum) {

            //今の対応する問題のデータを持ったquestionObjectを決める
            BiggestQuestionObject questionObject = biggestQuestions[qNum - 1];

            //この問題の答えのタグをbasketに持たせる
            basket.tag = questionObject.answerTag;

            //問題を表示する
            //果物が2個の場合はbiggerかsmallerにする
            if(questionObject.fruitNum == 2) {
                if(basket.tag == "biggest") {
                    qText.GetComponent<Text>().text = "Which one is bigger?";
                }
                else if (basket.tag == "smallest") {
                    qText.GetComponent<Text>().text = "Which one is smaller?";
                }
            }
            else {
                //三個以上の時はbiggestかsmallestにする
                qText.GetComponent<Text>().text = "Which one is the " + basket.tag.ToString() + "?";
            }

            //Characterを生成し、タグをつけて定位置に置く
            activeCharacter = Instantiate(questionObject.character) as GameObject;
            activeCharacter.transform.position = new Vector3(-11f, -3, 0);

            //fruitのオブジェクトを入れる配列の数を、各問題の果物数に合わせる
            fruits = new GameObject[questionObject.fruitNum];

            //果物の位置を入れる配列を初期化する
            fruitPos = new Vector3[questionObject.fruitNum];

            //果物の位置を取得する
            for (int i = 0; i < questionObject.fruitNum; i++) {
                fruitPos[i] = questionObject.fruitPos[i];
            }

            //果物の位置をシャッフルする
            PositionShuffle();

            //果物をシャッフル後の位置に生成する。
            for (int i = 0; i < questionObject.fruitNum; i++) {
                fruits[i] = Instantiate(fruit, fruitPos[i], Quaternion.identity);
                fruits[i].GetComponent<SpriteRenderer>().sprite = questionObject.fruit;
                fruits[i].GetComponent<SpringJoint2D>().connectedAnchor = fruitPos[i];
                fruits[i].transform.parent = fruitsParent.transform;

                //i=0にsmallestタグを付けて縮小、最後のiにbiggestタグをつけて拡大
                if(i == 0) {
                    fruits[i].tag = "smallest";
                    fruits[i].transform.localScale = new Vector3(5, 5, 1);
                }
                else if(i == questionObject.fruitNum - 1) {
                    fruits[i].tag = "biggest";
                    fruits[i].transform.localScale = new Vector3(7, 7, 1);
                }
            }
            activeCharacter.GetComponent<Game3Character>().SetMoveTarget(target, 0.1f);
        }
        else {
            //問題を消す
            Destroy(qText);

            //終了
            finText.GetComponent<Text>().text = "AWESOME!";

            //ゲーム終了に指定
            isFinished = true;

            //次のレベルを遊べるようにする
            if (GameManager.openLevelDic["Game3"] == GameManager.levelNum) {
                GameManager.openLevelDic["Game3"] = GameManager.levelNum + 1;
                Debug.Log("レベル開放");
            }
        }

        //問題番号を1増やす
        qNum++;
    }

    // selectingQuestionsをシャッフルする
    public void QuestionShuffle() {
        for (int index1 = 0; index1 < biggestQuestions.Length; index1++) {
            int index2 = Random.Range(0, biggestQuestions.Length - 1);
            QuestionSwap(ref biggestQuestions[index1], ref biggestQuestions[index2]);
        }
    }

    // question1 と questioon2 を入れ替える
    private void QuestionSwap(ref BiggestQuestionObject question1, ref BiggestQuestionObject question2) {
        BiggestQuestionObject question = question1;
        question1 = question2;
        question2 = question;
    }

    // fruitPos[]の中身をシャッフルする
    public void PositionShuffle() {
        for (int index1 = 0; index1 < fruitPos.Length; index1++) {
            int index2 = Random.Range(0, fruitPos.Length);
            PositionSwap(ref fruitPos[index1], ref fruitPos[index2]);
        }
    }

    // position1 と position2 を入れ替える
    private void PositionSwap(ref Vector3 position1, ref Vector3 position2) {
        Vector3 postion = position1;
        position1 = position2;
        position2 = postion;
    }
}
