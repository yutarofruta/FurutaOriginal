using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharingQuestionObject : MonoBehaviour {

    public int animalNum;        //動物の人数
    public GameObject animal;       //生成する動物のプレハブ

    public Vector3[] fruitPos;      //果物の配置場所
    public Vector3[] animalPos;      //動物の生成場所


    /*注意*
     targetの要素の順番が真ん中を1として4,2,1,3,5となっているので
     果物の順番もそれに対応する順番でないと、すでに果物を持っているかどうかの判定がおかしくなる。
     questionObjectの初期値を設定するときにはそこを注意してください
     */

}
