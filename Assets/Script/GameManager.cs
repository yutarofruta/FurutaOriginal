using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject popUp;        //ポップアップ
    private bool isPopUpActive = false;     //ポップアップが表示されているかどうか
    public string gameName;        //選択されているゲームの名前
    public static int levelNum;     //レベル数

    private static bool created = false;        //GameManagerのDontDestroyOnLoadがされたかどうか

    public static Dictionary<string, int> openLevelDic;        //ゲーム名とクリアレベル数を記録

    //GameManagerのDontDestroyOnLoadする
    void Awake() {
        if (!created) {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
    }

    private void Start() {
        popUp.SetActive(false);
        openLevelDic = new Dictionary<string, int>();
    }

    //押されたゲームの[level]レベルに遷移
    public void LoadGame(int level) {

        levelNum = level;
        SceneManager.LoadScene(gameName);
    }

    //ポップアップを表示・非表示
    public void PopUpSetActice(string game) {

        //押されたゲーム名を保存
        this.gameName = game;

        //表示・非表示
        isPopUpActive = !isPopUpActive;
        popUp.SetActive(isPopUpActive);

        //対応するgameNameのDictionaryができていなかったら、clearLevelを1した要素を追加する
        if (!openLevelDic.ContainsKey(gameName)) {
            openLevelDic.Add(gameName, 1);
        } else {
            Debug.Log(gameName);
            Debug.Log(openLevelDic[gameName]);
        }
    }
}
