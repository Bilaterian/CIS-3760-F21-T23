using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CreateGameMenu
{
    private List<Player> possiblePlayers;
    private Player playerOne;
    private Player playerTwo;

    const string prefname = "Optional";

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

        playerOneDropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(playerOneDropdown, playerTwoDropdown);
        });
        playerTwoDropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(playerTwoDropdown, playerOneDropdown);
        });


        playerOneDropdown.value = 0;
        DropdownValueChanged(playerOneDropdown, playerTwoDropdown);
        playerTwoDropdown.value = 1;
        DropdownValueChanged(playerTwoDropdown, playerOneDropdown);

        DataSaver.SaveList<Player>("possiblePlayers", possiblePlayers);
        prefname = playerOneDropdown.options[playerOneDropdown.value].text;
        PlayerPrefs.SetString("playerOne", playerFirst);
        PlayerPrefs.Save();
        Debug.Log(playerFirst);
       
    }

    public void CreatePlayer(string name, Dropdown playerOneDropdown, Dropdown playerTwoDropdown)
    {

        // int playerOneMenuIdex = playerOneDropdown.GetComponent<Dropdown>().value; 
        // List<DropDown.OptionData> playerOneOptions = playerOneDropdown.GetComponent<Dropdown>().options;
        // string playerOneName = playerOneOptions[playerOneMenuIdex].text;


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
        
        var playerfirst = playerOneDropdown.options[playerOneDropdown.value].text;
        PlayerPrefs.SetString("playerOne", playerfirst);
        PlayerPrefs.Save();
        Debug.Log(playerfirst);
        // var playersecond = playerOneDropdown.options[playerOneDropdown.value].text;
        // PlayerPrefs.SetString("playerOne", playersecond);
        // PlayerPrefs.Save();
        //save current players(key,value)
        //PlayerPrefs.SetString("playerOneName", playerOneDropdown.text);
        
        //  Debug.Log(PlayerPrefs.GetString("playerOneName"));
    }

    public void DropdownValueChanged(Dropdown changedDropdown, Dropdown unchangedDropdown)
    {
        unchangedDropdown.options.Clear();

        var newDropdownValue = changedDropdown.options[changedDropdown.value].text;
        var possiblePlayersMinusNewlySelected = from player in possiblePlayers
                                                where player.name != newDropdownValue
                                                select new Dropdown.OptionData() { text = player.name };
        unchangedDropdown.AddOptions(possiblePlayersMinusNewlySelected.ToList());
        //save newly updated value from here

    }
}