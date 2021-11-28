using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverMenu;
    [SerializeField]
    Text playerWhoWonText;
    [SerializeField]
    Text playerWhoLostText;

    public void SetGameOver(string playerWhoWon, string playerWhoLost)
    {
        playerWhoWonText.text = playerWhoWon;
        playerWhoLostText.text = playerWhoLost;
        gameOverMenu.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("CreateGameMenu");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
