using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CreateGameMenu
{
    private List<Player> possiblePlayers;
    private Player playerOne;
    private Player playerTwo;

    public void InitializeDropdowns(Dropdown playerOneDropdown, Dropdown playerTwoDropdown)
    {
        possiblePlayers = DataSaver.LoadList<Player>("possiblePlayers");

        playerOneDropdown.options.Clear();
        playerTwoDropdown.options.Clear();

        if (possiblePlayers == null || possiblePlayers.Count() == 0)
        {
            possiblePlayers = new List<Player>();
            CreatePlayer("Player 1", playerOneDropdown, playerTwoDropdown);
            CreatePlayer("Player 2", playerTwoDropdown, playerOneDropdown);
        }
        else
        {
            foreach (Player player in possiblePlayers)
            {
                playerOneDropdown.options.Add(new Dropdown.OptionData { text = player.name });
                playerTwoDropdown.options.Add(new Dropdown.OptionData { text = player.name });
            }
        }

        playerOneDropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(playerOneDropdown, playerTwoDropdown);
            var playerfirst = this.GetSelectedPlayer(playerOneDropdown);
            DataSaver.Save<Player>("playerOne", playerfirst);
        });
        playerTwoDropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(playerTwoDropdown, playerOneDropdown);
            var playerSecond = this.GetSelectedPlayer(playerTwoDropdown);
            DataSaver.Save<Player>("playerTwo", playerSecond);
        });


        playerOneDropdown.value = 0;
        DropdownValueChanged(playerOneDropdown, playerTwoDropdown);
        playerTwoDropdown.value = 1;
        DropdownValueChanged(playerTwoDropdown, playerOneDropdown);

        DataSaver.SaveList<Player>("possiblePlayers", possiblePlayers);

        var playerfirst = this.GetSelectedPlayer(playerOneDropdown);
        DataSaver.Save<Player>("playerOne", playerfirst);

        var playerSecond = this.GetSelectedPlayer(playerTwoDropdown);
        DataSaver.Save<Player>("playerTwo", playerSecond);
    }

    private Player GetSelectedPlayer(Dropdown dropdown)
    {
        var playerName = dropdown.options[dropdown.value].text;
        return this.possiblePlayers.Find(player => player.name == playerName);
    }

    public void CreatePlayer(string name, Dropdown playerOneDropdown, Dropdown playerTwoDropdown)
    {
        if (possiblePlayers.Exists(player => player.name == name))
        {
            return;
        }

        Player newPlayer = new Player(name);
        possiblePlayers.Add(newPlayer);

        playerOneDropdown.options.Add(new Dropdown.OptionData() { text = name });
        playerTwoDropdown.options.Add(new Dropdown.OptionData() { text = name });

        playerOneDropdown.RefreshShownValue();
        playerTwoDropdown.RefreshShownValue();

        DataSaver.SaveList<Player>("possiblePlayers", possiblePlayers);
    }

    public void DropdownValueChanged(Dropdown changedDropdown, Dropdown unchangedDropdown)
    {
        unchangedDropdown.options.Clear();

        var newDropdownValue = changedDropdown.options[changedDropdown.value].text;
        var possiblePlayersMinusNewlySelected = from player in possiblePlayers
                                                where player.name != newDropdownValue
                                                select new Dropdown.OptionData() { text = player.name };
        unchangedDropdown.AddOptions(possiblePlayersMinusNewlySelected.ToList());
    }
}