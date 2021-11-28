using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerStats : MonoBehaviour
{
    [SerializeField]
    private Text playerNameText;
    [SerializeField]
    private Text winsText;
    [SerializeField]
    private Text lossesText;
    [SerializeField]
    private Text piecesCapturedText;

    public void SetText(string name, int wins, int losses, int piecesCaptured)
    {
        this.playerNameText.text = "Player: " + name;
        this.winsText.text = "Wins: " + wins.ToString();
        this.lossesText.text = "Losses: " + losses.ToString();
        this.piecesCapturedText.text = "Pieces Captured: " + piecesCaptured.ToString();
    }
}
