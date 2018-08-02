using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GroupingQuestionManager : MonoBehaviour {

    private int qNum = 1;    　//問題番号
    private int maxNum;     //問題数(QuestionObjectの数)
    private int correctNum = 0;      //正解の数
    private bool isFinished = false;

    public GroupingQuestionObject[] groupingQuestions;      //GroupingQuestionオブジェクトのプレハブ

    public GameObject leftBasket;     //左のかご
    public GameObject rightBasket;     //右のかご

    private GameObject leftCharacter;     //左のキャラクター
    private GameObject rightCharacter;     //右のキャラクター

    public GameObject fruit;     //sprite入れる前の果物プレハブ

    public GameObject fruitsParent;     //Instanceする果物の親

    private GameObject[] leftFruits;     //左の果物を入れる
    private GameObject[] rightFruits;    //右の果物を入れる

    private string leftFruitTag;    //左の果物のタグ
    private string rightFruitTag;    //左の果物のタグ

    public Text text;       //Awesomeのテキスト

    public GameObject target;


    private void Start() {

        //シーン名を取得する
        string sceneName = SceneManager.GetActiveScene().name;

        //Resourcesからlevelに対応する問題を読みだして、objectArrayに入れる
        object[] objectArray = Resources.LoadAll(sceneName + "_" + GameManager.levelNum.ToString(), typeof(GroupingQuestionObject));

        //selectingQuestionの配列の大きさを、呼び出した問題の配列数と揃える
        System.Array.Resize(ref groupingQuestions, objectArray.Length);

        //objectArrayの中身をselectingQuestionに入れる
        for (int i = 0; i < objectArray.Length; i++) {
            groupingQuestions[i] = (GroupingQuestionObject) objectArray[i];
        }

        //問題の数を取得
        maxNum = groupingQuestions.Length;

        //一問目を表示
        GoNextQuestion();
    }

    private void Update() {

        //ゲームメニューに戻る
        if (isFinished && Input.GetMouseButton(0)) {
            SceneManager.LoadScene("GameMenu");
        }
    }

    public void GoNextQuestion() {

        //一問目でなければ、前の果物を消去し、correctNumも初期化する
        if (qNum != 1) {
            for(int i = 0; i < leftFruits.Length; i++) {
                Destroy(leftFruits[i]);
            }
            for (int i = 0; i < rightFruits.Length; i++) {
                Destroy(rightFruits[i]);
            }
            correctNum = 0;
        }
        
        if (qNum <= maxNum) {

            //今の対応する問題のデータを持ったquestionObjectを決める
            GroupingQuestionObject questionObject = groupingQuestions[qNum - 1];

            //leftCharacterを生成し、タグをつけて左定位置に置く
            leftCharacter = Instantiate(questionObject.leftCharacter) as GameObject;
            leftCharacter.tag = "leftCharacter";
            leftCharacter.transform.position = new Vector3(-11f, 1, 0);

            //rightCharacterを生成し、タグをつけて右定位置に置く
            rightCharacter = Instantiate(questionObject.rightCharacter) as GameObject;
            rightCharacter.tag = "rightCharacter";
            rightCharacter.transform.position = new Vector3(11f, 1, 0);

            //バスケットにタグをつける
            leftBasket.tag = questionObject.leftFruitTag;
            rightBasket.tag = questionObject.rightFruitTag;

            //fruitのオブジェクトを入れる配列の数を、各問題の果物数に合わせる
            leftFruits = new GameObject[questionObject.leftFruitNum];
            rightFruits = new GameObject[questionObject.rightFruitNum];

            //左の果物を生成する
            for (int i = 0; i < questionObject.leftFruitNum; i++) {
                leftFruits[i] = Instantiate(fruit, questionObject.leftFruitPos[i], Quaternion.identity);
                leftFruits[i].tag = questionObject.leftFruitTag;
                leftFruits[i].GetComponent<SpriteRenderer>().sprite = questionObject.leftFruit;
                leftFruits[i].GetComponent<SpringJoint2D>().connectedAnchor = questionObject.leftFruitPos[i];
                leftFruits[i].transform.parent = fruitsParent.transform;
            }

            //右の果物を生成する
            for (int i = 0; i < questionObject.rightFruitNum; i++) {
                rightFruits[i] = Instantiate(fruit, questionObject.rightFruitPos[i], Quaternion.identity);
                rightFruits[i].tag = questionObject.rightFruitTag;
                rightFruits[i].GetComponent<SpriteRenderer>().sprite = questionObject.rightFruit;
                rightFruits[i].GetComponent<SpringJoint2D>().connectedAnchor = questionObject.rightFruitPos[i];
                rightFruits[i].transform.parent = fruitsParent.transform;
            }

            //キャラクターを動き始めさせる
            leftCharacter.GetComponent<Game2Character>().SetMoveTarget(target, 6.0f);
            rightCharacter.GetComponent<Game2Character>().SetMoveTarget(target, 6.0f);
        }
        else {
            //終了
            text.GetComponent<Text>().text = "AWESOME!";

            //ゲーム終了に設定
            isFinished = true;
        }

        //問題番号を1増やす
        qNum++;
    }

    public void AddCorrectNum() {

        //正解数を加算
        correctNum++;

        //正解数が果物の数と同じになったら次の問題へ
        if(correctNum == leftFruits.Length + rightFruits.Length) {
            leftCharacter.GetComponent<GoBackCharacter>().GoNextState();
            rightCharacter.GetComponent<GoBackCharacter>().GoNextState();
        }
    }
}
