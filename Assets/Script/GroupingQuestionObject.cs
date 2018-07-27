using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupingQuestionObject : MonoBehaviour {

    public Sprite leftFruit;     //左に置かれる果物
    public Sprite rightFruit;     //右に置かれる果物
    public int leftFruitNum;        //左に置かれる果物の個数
    public int rightFruitNum;        //右に置かれる果物の個数
    public string leftFruitTag;     //左に置かれる果物のタグ
    public string rightFruitTag;     //右に置かれる果物のタグ
    public GameObject leftCharacter;        //左に置かれる果物のキャラ
    public GameObject rightCharacter;       //右に置かれる果物のキャラ

    public Vector3[] leftFruitPos;      //左の果物の配置場所
    public Vector3[] rightFruitPos;      //右の果物の配置場所

}
