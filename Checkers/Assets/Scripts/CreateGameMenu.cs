using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

[Serializable]
public struct Player {
    public string name;
};

public class CreateGameMenu : MonoBehaviour
{
    [SerializeField] Dropdown playerOneDropDown;
    [SerializeField] Dropdown playerTwoDropDown;
    [SerializeField] InputField createPlayerInputField;
    private List<Player> possiblePlayers;
    private Player playerOne;
    private Player playerTwo;

    void Start() {
        playerOneDropDown.options.Clear();
        playerTwoDropDown.options.Clear();
        possiblePlayers = new List<Player>();

        playerOneDropDown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(playerOneDropDown);
        });
        playerTwoDropDown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(playerTwoDropDown);
        });

        CreatePlayer("Player 1");
        CreatePlayer("Player 2");
        playerOneDropDown.value = 0;
        DropdownValueChanged(playerOneDropDown);
        playerTwoDropDown.value = 1;
        DropdownValueChanged(playerTwoDropDown);
    }

    public void CreatePlayer() {
        CreatePlayer(createPlayerInputField.text);
    }

    public void CreatePlayer(string name) {
        Player newPlayer;
        newPlayer.name = name;
        possiblePlayers.Add(newPlayer);
        
        playerOneDropDown.options.Add(new Dropdown.OptionData() { text = name });
        playerTwoDropDown.options.Add(new Dropdown.OptionData() { text = name });

        playerOneDropDown.RefreshShownValue();
        playerTwoDropDown.RefreshShownValue();
    }

    private void DropdownValueChanged(Dropdown dropdown) {
        Dropdown unchangedDropdown = object.ReferenceEquals(dropdown, playerOneDropDown) ? playerTwoDropDown : playerOneDropDown;
        Debug.Log(dropdown.value);
        unchangedDropdown.options.Clear();

        var newDropdownValue = dropdown.options[dropdown.value].text;
        var possiblePlayersMinusNewlySelected = from player in possiblePlayers
                                                where player.name != newDropdownValue
                                                select new Dropdown.OptionData() { text = player.name };
        unchangedDropdown.AddOptions(possiblePlayersMinusNewlySelected.ToList());
    }
}
