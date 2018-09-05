using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SharingQuestionManager : MonoBehaviour {

    private int qNum = 1;    　//問題番号
    private int maxNum;     //問題数(QuestionObjectの数)
    private int correctNum = 0;      //正解の数
    private bool isFinished = false;        //クリアしているかどうか

    private SharingQuestionObject[] sharingQuestions;      //SharingQuestionオブジェクトのプレハブ

    private GameObject animalPrehub;        //生成する動物のプレハブ

    private GameObject[] animals;     //動物たち

    public GameObject fruitPrehub;     //Instanceする果物のプレハブ

    private GameObject[] fruits;     //生成された果物を入れる配列

    public GameObject choiceManager;        //生成した果物の親オブジェクト

    public Text text;       //Awesomeのテキスト

    public GameObject[] targets;        //動物たちが止まる場所


    private void Start() {

        //問題を読み出す
        ReadQuestion();

        //問題の数を取得
        maxNum = sharingQuestions.Length;

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
            for (int i = 0; i < fruits.Length; i++) {
                Destroy(fruits[i]);
            }
            correctNum = 0;
        }

        if (qNum <= maxNum) {

            //今の対応する問題のデータを持ったquestionObjectを決める
            SharingQuestionObject questionObject = sharingQuestions[qNum - 1];

            //animalを入れる配列の要素数を、各問題の動物数に合わせる
            animals = new GameObject[questionObject.animalNum];

            //どの動物を生成するのかを取得
            animalPrehub = questionObject.animal;

            //動物たちを生成し、定位置に置く
            for(int i = 0; i < animals.Length; i++) {
                animals[i] = Instantiate(animalPrehub, questionObject.animalPos[i], Quaternion.identity);
                animals[i].GetComponent<SpriteRenderer>().sortingOrder = 1;
                /*注意*
                 targetの要素の順番が真ん中を1として4,2,1,3,5となっているので
                 果物の順番もそれに対応する順番でないと、すでに果物を持っているかどうかの判定がおかしくなる。
                 questionObjectの初期値を設定するときにはそこを注意してください
                */
            }

            //fruitのオブジェクトを入れる配列の数を、各問題の動物の数に合わせる()
            fruits = new GameObject[animals.Length];

            //果物を生成する
            for (int i = 0; i < animals.Length; i++) {
                fruits[i] = Instantiate(fruitPrehub, questionObject.fruitPos[i], Quaternion.identity);
                fruits[i].GetComponent<SpringJoint2D>().connectedAnchor = questionObject.fruitPos[i];
                fruits[i].GetComponent<SpriteRenderer>().sortingOrder = 1;
                fruits[i].transform.parent = choiceManager.transform;
            }

            //キャラクターを動き始めさせる
            for(int i = 0; i < animals.Length; i++) {
                animals[i].GetComponent<Game4Character>().SetMoveTarget(targets[i], 1f);
            }
        }
        else {
            //終了
            text.GetComponent<Text>().text = "AWESOME!";

            //ゲーム終了に設定
            isFinished = true;

            //次のレベルを遊べるようにする
            if (GameManager.openLevelDic["Game4"] == GameManager.levelNum) {
                GameManager.openLevelDic["Game4"] = GameManager.levelNum + 1;
                Debug.Log("レベル開放");
            }
        }

        //問題番号を1増やす
        qNum++;
    }

    public void AddCorrectNum(GameObject fruit) {

        //正解数を加算
        correctNum++;

        //すでに果物を貰った動物が二度貰わないように、動物のタグをUntaggedに変更する
        GameObject currentCollision = fruit.GetComponent<ChoiceController>().currentCollision;
        currentCollision.tag = "Untagged";

        //正解数が動物の数と同じ（動物全員が果物持ってる）になったら次の問題へ
        if (correctNum == animals.Length) {
            for(int i = 0; i < animals.Length; i++) {
                animals[i].GetComponent<GoBackCharacter>().GoNextState();
            }
        }     
    }

    public void ReadQuestion() {
        //シーン名を取得する
        string sceneName = SceneManager.GetActiveScene().name;

        //Resourcesからlevelに対応する問題を読みだして、objectArrayに入れる
        object[] objectArray = Resources.LoadAll(sceneName + "_" + GameManager.levelNum.ToString(), typeof(SharingQuestionObject));

        //sharingQuestionの配列の大きさを、呼び出した問題の配列数と揃える
        System.Array.Resize(ref sharingQuestions, objectArray.Length);

        //objectArrayの中身をsharingQuestionに入れる
        for (int i = 0; i < objectArray.Length; i++) {
            sharingQuestions[i] = (SharingQuestionObject)objectArray[i];
        }
    }
}
