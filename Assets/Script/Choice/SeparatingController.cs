using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeparatingController : MonoBehaviour {

    public bool isTouchable = true;

    public GameObject questonManger;

    public GameObject leftPlate;
    public GameObject rightPlate;

    // Use this for initialization
    void Start() {
        questonManger = GameObject.Find("QuestionManager");
        leftPlate = GameObject.Find("leftPlate");
        rightPlate = GameObject.Find("rightPlate");
    }

    // Update is called once per frame
    void Update() {
    }


    //EventSystem

    public void OnDrag() {

        //Touchableでないときはオブジェクトを移動させない
        if (!isTouchable) {
            return;
        }
            
        //オブジェクトがタッチについてくる
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            Vector3 vec = touch.position;
            vec.z = 10f;
            vec = Camera.main.ScreenToWorldPoint(vec);
            if(vec.x > -8 || vec.x < 8) {
                transform.position = new Vector3(vec.x, transform.position.y, vec.z);
            }
            else {
                transform.position = new Vector3(-8, transform.position.y, vec.z);
            }
        }
        else if (Input.GetMouseButton(0)) {
            Vector3 vec = Input.mousePosition;
            vec.z = 10f;
            vec = Camera.main.ScreenToWorldPoint(vec);
            if (vec.x > -8 || vec.x < 8) {
                transform.position = new Vector3(vec.x, transform.position.y, vec.z);
            }
            else {
                transform.position = new Vector3(-8, transform.position.y, vec.z);
            }
        }
        
    }

    public void EndDrag() {

        //正解後は触らせない
        if (!isTouchable) {
            return;
        }

    }

    public void PointerDown() {

        //回答中以外は触らせない
        if (!isTouchable) {
            return;
        }
    }

    public void PointerUp() {

    }

    public void OnTriggerEnter2D(Collider2D collision) {

        if(collision == leftPlate) {
            questonManger.GetComponent<SeparatingQuestionManager>().leftCurrentNum++;
            Debug.Log("leftNum++");
        }
        else if (collision == rightPlate) {
            questonManger.GetComponent<SeparatingQuestionManager>().rightCurrentNum++;
            Debug.Log("rightNum++");
        }

    }

    public void OnTriggerExit2D(Collider2D collision) {

        if (collision == leftPlate) {
            questonManger.GetComponent<SeparatingQuestionManager>().leftCurrentNum--;
            Debug.Log("leftNum--");
        }
        else if (collision == rightPlate) {
            questonManger.GetComponent<SeparatingQuestionManager>().rightCurrentNum--;
            Debug.Log("rightNum--");
        }

    }

}
