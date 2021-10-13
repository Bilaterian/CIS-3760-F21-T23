using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using System.Linq;

public class CreateGameMenuTests
{
    [UnityTest]
    public IEnumerator DefaultPlayersAddedWhenNoneExistOnStartup()
    {
        PlayerPrefs.DeleteAll();
        var createGameMenu = new CreateGameMenu();
        var gameObjectOne = new GameObject();
        var gameObjectTwo = new GameObject();
        var playerOneDropdown = gameObjectOne.AddComponent<Dropdown>();
        var playerTwoDropdown = gameObjectTwo.AddComponent<Dropdown>();

        createGameMenu.InitializeDropdowns(playerOneDropdown, playerTwoDropdown);

        yield return null;

        Assert.AreEqual(1, playerOneDropdown.options.Count);
        Assert.AreEqual(1, playerTwoDropdown.options.Count);
        // with only the default players, only the selected player should be in the dropdown since
        // players that are selected by the other dropdown should not be selectable.
        Assert.AreNotEqual(playerOneDropdown.options[0].text, playerTwoDropdown.options[0].text);

    }

    [UnityTest]
    public IEnumerator SavedPlayersLoadedIntoDropdownsOnStartup()
    {
        // setup gameObjects
        var createGameMenu = new CreateGameMenu();
        var gameObjectOne = new GameObject();
        var gameObjectTwo = new GameObject();
        var playerOneDropdown = gameObjectOne.AddComponent<Dropdown>();
        var playerTwoDropdown = gameObjectTwo.AddComponent<Dropdown>();

        // setup playerPrefs with players so they can be loaded
        var players = new List<Player>();
        players.Add(new Player("Player 1"));
        players.Add(new Player("Player 2"));
        players.Add(new Player("Evan"));
        players.Add(new Player("Caleb"));
        DataSaver.SaveList("possiblePlayers", players);

        createGameMenu.InitializeDropdowns(playerOneDropdown, playerTwoDropdown);

        yield return null;

        Assert.AreEqual(3, playerOneDropdown.options.Count);
        Assert.AreEqual(3, playerTwoDropdown.options.Count);
        var allPlayerDropdownOptions = playerOneDropdown.options.Concat(playerTwoDropdown.options);
        var allPlayerNamesLoaded = new HashSet<string>(
            from option in allPlayerDropdownOptions
            select option.text
        );
        var expectedPlayerNames = new HashSet<string>(
            from player in players
            select player.name
        );

        Assert.IsTrue(expectedPlayerNames.SetEquals(allPlayerNamesLoaded));
    }
}
