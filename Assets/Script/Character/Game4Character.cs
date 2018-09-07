using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game4Character : GoBackCharacter {
    
    //GoNextQuestion
    public override void GoNextQuestion() {
        base.GoNextQuestion();

        //この問題ではキャラクターが多数いるため、直接GoNextQuestionは呼ばず、questionMangaerのleftCharacterNumがキャラクター数と同じになったらGoNextQuestionが呼ばれるようにする
        questionManager.GetComponent<SharingQuestionManager>().CountLeftCharacterNum();
    }

    //WAITステイト中にやること
    public override void WaitState() {

        //スプライトのタッチを禁止
        choiceManager.GetComponent<ChoiceManager>().ChangeSpritesIsTouchable(false);

        if (target != null) {

            //キャラクターからターゲットまでの距離の絶対値
            float diff = Mathf.Abs(target.transform.position.x - transform.position.x);
            Debug.Log(diff);

            //targetの近くまで歩き、十分近くなったら次のStateへ
            if (diff >= stopDistance) {
                transform.Translate(0.12f, 0, 0, Space.Self);
            }
            else {
                GoNextState();

                //スプライトのタッチを許可
                choiceManager.GetComponent<ChoiceManager>().ChangeSpritesIsTouchable(true);
            }

        }
    }




}

