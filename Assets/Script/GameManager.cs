using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private GameObject popUp;        //ポップアップ

    public string gameName;        //選択されているゲームの名前

    public static int levelNum;     //レベル数

    public static bool created = false;        //GameManagerのDontDestroyOnLoadがされたかどうか

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
        openLevelDic = new Dictionary<string, int>();       //Dictionaryを登録
    }
    
    private void Update() {

        //GameMenuでpopUpを持っていなかったらFindする
        if(popUp == null && SceneManager.GetActiveScene().name == "GameMenu") {
            popUp = GameObject.Find("Canvas").transform.Find("PopUp").gameObject;
        }
    }

    //ポップアップを表示
    public void PopUpSetActive(string game) {

        //押されたゲーム名を保存
        gameName = game;

        //ポップアップを表示
        popUp.gameObject.SetActive(true);

        //対応するgameNameのDictionaryができていなかったら、clearLevelを1した要素を追加する
        if (!openLevelDic.ContainsKey(gameName)) {
            openLevelDic.Add(gameName, 1);
        } else {
            Debug.Log(gameName);
            Debug.Log(openLevelDic[gameName]);
        }
    }
}
