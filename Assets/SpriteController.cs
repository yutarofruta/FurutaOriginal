using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteController : MonoBehaviour {

    public GameObject goal;
    public GameObject grape;
    public GameObject questionManager;
    public Text text;

    private string answerTag;
    public bool isTouchable = true;
        
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        answerTag = questionManager.GetComponent<Questions>().answerTag.ToString();
        //Debug.Log(answerTag);

    }

    public void OnDrag() {

        //Touchableでないときはオブジェクトを移動させない
        if (!isTouchable) {
            return;
        }

        //動かしているオブジェクトとゴールの場所までの距離
        float distance = Vector3.Distance(transform.position, goal.transform.position);
        Debug.Log(distance);

        //正解のオブジェクトがゴールの十分近くに来たら、静止してCLEARに移る
        if (distance < 0.7f && gameObject.tag == answerTag) {
            transform.position = goal.transform.position;
            grape.GetComponent<GrapeController>().GoNextState();
            Debug.Log("Go CLEAR from PLAY");
            Destroy(gameObject);

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

        //ドラッグが終わったらSpringJointを有効にする
        if(transform.position != goal.transform.position) {
            gameObject.GetComponent<SpringJoint2D>().enabled = true;
        }
    }
   
}
