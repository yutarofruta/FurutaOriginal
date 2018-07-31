using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject popUp;
    private bool isPopUpActive = false;
    private string gameName;
    public static int levelNum;

    private void Start() {
        popUp.SetActive(false);
    }

    public void LoadGame(int level) {

        levelNum = level;
        SceneManager.LoadScene(gameName);
    }

    public void PopUpSetActice(string game) {

        this.gameName = game;

        isPopUpActive = !isPopUpActive;
        popUp.SetActive(isPopUpActive);
    }
}
