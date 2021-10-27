using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;

    public void Pause() {
        pauseMenu.SetActive(true);
    }

    public void Resume() {
        pauseMenu.SetActive(false);
    }

    public void MainMenu(string scene) {
        SceneManager.LoadScene(scene);
    }
}
