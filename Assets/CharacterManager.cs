using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

    Animator anim;
    AnimatorStateInfo animInfo;

    public GameObject spriteManager;
    public GameObject questionManager;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        
	}

    public void GetAnimInfo() {
        if(gameObject.tag == questionManager.GetComponent<QuestionManager>().answerTag) {
            animInfo = gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        }
    }

}
