using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountingQuestionObject : MonoBehaviour {

    public Sprite fruit;     //果物
    public int fruitNum;        //果物の個数

    public GameObject character;       //果物のキャラ

    public Vector3[] fruitPos;      //果物の配置場所

    public string[] buttonNum;      //各ボタンの数字
}
