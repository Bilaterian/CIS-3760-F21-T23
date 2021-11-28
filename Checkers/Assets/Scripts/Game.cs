using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum playerTurn
{ //use this to inform playerController whose turn it is
    RedTurn = 0,
    BlackTurn = 1
}

[Serializable]
public class GameStats
{
    public string wonPlayerName;
    public string lostPlayerName;
    public string gameLength;
    public long gameEndTime;
}


public class Game
{
    public static int numOfTurns = 0;
    Player blackPlayer;
    Player redPlayer;
    float startTime;

    public Game(Player blackPlayer, Player redPlayer)
    {
        this.blackPlayer = blackPlayer;
        this.redPlayer = redPlayer;
        startTime = Time.time;
    }

    public void RedPieceKilled()
    {
        this.blackPlayer.piecesCaptured += 1;
        DataSaver.UpdatePlayer(this.blackPlayer);
    }

    public void BlackPieceKilled()
    {
        this.redPlayer.piecesCaptured += 1;
        DataSaver.UpdatePlayer(this.redPlayer);
    }

    public void GameOver(bool blackWins, GameObject gameOverMenu)
    {
        if (blackPlayer.name == "no name") return;

        var won = blackWins ? this.blackPlayer : this.redPlayer;
        var lost = blackWins ? this.redPlayer : this.blackPlayer;


        won.wins += 1;
        lost.losses += 1;

        GameStats gameStats = new GameStats();
        gameStats.wonPlayerName = won.name;
        gameStats.lostPlayerName = lost.name;

        var timeElapsed = Time.time - startTime;
        var seconds = (int)timeElapsed % 60;
        var minutes = timeElapsed / 60;
        gameStats.gameLength = string.Format("{0}:{1}", minutes.ToString("0"), seconds.ToString("00"));

        gameStats.gameEndTime = DateTime.Now.ToFileTime();

        DataSaver.SaveNewGame(gameStats);
        DataSaver.UpdatePlayer(won);
        DataSaver.UpdatePlayer(lost);

        gameOverMenu.GetComponent<GameOverMenu>().SetGameOver(won.name, lost.name);
    }
}
