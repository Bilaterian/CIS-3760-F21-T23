using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class CreateGameMenu : MonoBehaviour
{
    [SerializeField] Dropdown playerOneDropDown;
    [SerializeField] Dropdown playerTwoDropDown;
    [SerializeField] InputField createPlayerInputField;
    private List<Player> possiblePlayers;
    private Player playerOne;
    private Player playerTwo;

    void Start() {
        possiblePlayers = DataSaver.LoadList<Player>("possiblePlayers");
        Debug.Log(possiblePlayers.Count());

        playerOneDropDown.options.Clear();
        playerTwoDropDown.options.Clear();

        if (possiblePlayers == null || possiblePlayers.Count() == 0) {
            possiblePlayers = new List<Player>();
            CreatePlayer("Player 1");
            CreatePlayer("Player 2");
        } else {
            foreach (Player player in possiblePlayers) {
                playerOneDropDown.options.Add(new Dropdown.OptionData { text = player.name });
                playerTwoDropDown.options.Add(new Dropdown.OptionData { text = player.name });
            }
        }

        playerOneDropDown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(playerOneDropDown);
        });
        playerTwoDropDown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(playerTwoDropDown);
        });


        playerOneDropDown.value = 0;
        DropdownValueChanged(playerOneDropDown);
        playerTwoDropDown.value = 1;
        DropdownValueChanged(playerTwoDropDown);

        DataSaver.SaveList<Player>("possiblePlayers", possiblePlayers);
    }

    public void CreatePlayer() {
        CreatePlayer(createPlayerInputField.text);
    }

    public void CreatePlayer(string name) {
        if (possiblePlayers.Exists(player => player.name == name)) {
            return;
        }

        Player newPlayer = new Player(name);
        possiblePlayers.Add(newPlayer);
        
        playerOneDropDown.options.Add(new Dropdown.OptionData() { text = name });
        playerTwoDropDown.options.Add(new Dropdown.OptionData() { text = name });

        playerOneDropDown.RefreshShownValue();
        playerTwoDropDown.RefreshShownValue();

        DataSaver.SaveList<Player>("possiblePlayers", possiblePlayers);
    }

    private void DropdownValueChanged(Dropdown dropdown) {
        Dropdown unchangedDropdown = object.ReferenceEquals(dropdown, playerOneDropDown) ? playerTwoDropDown : playerOneDropDown;
        unchangedDropdown.options.Clear();

        var newDropdownValue = dropdown.options[dropdown.value].text;
        var possiblePlayersMinusNewlySelected = from player in possiblePlayers
                                                where player.name != newDropdownValue
                                                select new Dropdown.OptionData() { text = player.name };
        unchangedDropdown.AddOptions(possiblePlayersMinusNewlySelected.ToList());
    }

    public void BackToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame() {
        SceneManager.LoadScene("Checkers");
    }
}
