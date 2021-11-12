using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class CreateGameMenuBehaviour : MonoBehaviour
{
    [SerializeField] Dropdown playerOneDropdown;
    [SerializeField] Dropdown playerTwoDropdown;
    [SerializeField] InputField createPlayerInputField;
    private CreateGameMenu createGameMenu;

    void Start()
    {
        createGameMenu = new CreateGameMenu();
        createGameMenu.InitializeDropdowns(playerOneDropdown, playerTwoDropdown);
    }

    public void CreatePlayer()
    {
        createGameMenu.CreatePlayer(createPlayerInputField.text, playerOneDropdown, playerTwoDropdown);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Checkers");
    }
}
