using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public GameObject popUp;
    private bool isPopUpActive = false;
    private string game;

    private void Start() {
        popUp.SetActive(false);
    }

    public void LoadGame(string level) {

        SceneManager.LoadScene(game);
    }

    public void PopUpSetActice(string game) {

        this.game = game;

        isPopUpActive = !isPopUpActive;
        popUp.SetActive(isPopUpActive);
    }
}
