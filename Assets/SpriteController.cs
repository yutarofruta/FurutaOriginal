using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteController : MonoBehaviour {

    public GameObject goal;
    public GameObject grape;
    public GameObject cherry;
    public GameObject melon;
    public GameObject questionManager;
    public Text text;

    public string answerTag;
    public bool isTouchable = true;
        
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        if(gameObject.tag == answerTag) {

        }
        

    }

    public void OnDrag() {

        //ドラッグされている間は拡大
        transform.localScale = new Vector3(10, 10, 1);

        //Touchableでないときはオブジェクトを移動させない
        if (!isTouchable) {
            return;
        }

        //動かしているオブジェクトとゴールの場所までの距離
        float distance = Vector3.Distance(transform.position, goal.transform.position);

        //正解オブジェクトのタグをQuestionManagerから取得
        answerTag = questionManager.GetComponent<QuestionManager>().answerTag.ToString();

        //正解のオブジェクトがゴールの十分近くに来たら、静止してCLEARに移る
        if (distance < 0.7f && gameObject.tag == answerTag) {
            transform.position = goal.transform.position;
            grape.GetComponent<CharacterManager>().GoNextState();
            cherry.GetComponent<CharacterManager>().GoNextState();
            melon.GetComponent<CharacterManager>().GoNextState();
            grape.GetComponent<CharacterManager>().clearedSprite = this.gameObject;
            transform.localScale = new Vector3(8, 8, 1);

        }
        else {  //オブジェクトがタッチについてくる

            if (Input.touchCount > 0) { 
                Touch touch = Input.GetTouch(0);
                Vector3 vec = touch.position;
                vec.z = 10f;
                vec = Camera.main.ScreenToWorldPoint(vec);
                transform.position = vec;
            }
            else if (Input.GetMouseButton(0)) {
                Vector3 vec = Input.mousePosition;
                vec.z = 10f;
                vec = Camera.main.ScreenToWorldPoint(vec);
                transform.position = vec;
            }
        }
        //オブジェクト移動中はSpringJointを無効にする    
        gameObject.GetComponent<SpringJoint2D>().enabled = false;
    }

    public void EndDrag() {

        //ドラッグが終わったら縮小
        transform.localScale = new Vector3(8, 8, 1);

        //ドラッグが終わったらSpringJointを有効にする
        if (transform.position != goal.transform.position) {
            gameObject.GetComponent<SpringJoint2D>().enabled = true;
        }
    }

    public void PointerDown() {
        //オブジェクトが触られている間は拡大
        transform.localScale = new Vector3(10, 10, 1);
    }

    public void PointerUp() {
        //オブジェクトが離されたら縮小
        transform.localScale = new Vector3(8, 8, 1);
    }

}
