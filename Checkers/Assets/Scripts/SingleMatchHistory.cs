using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleMatchHistory : MonoBehaviour
{
    [SerializeField]
    private Text winnerText;
    [SerializeField]
    private Text loserText;

    public void SetText(string winner, string loser)
    {
        winnerText.text = winner;
        loserText.text = loser;
    }

    public void OnClick()
    {

    }
}
