using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum playerTurn{ //use this to inform playerController whose turn it is
    RedTurn = 0,
    BlackTurn = 1
}



public class GameController : MonoBehaviour
{
    public static int numOfTurns = 0;
    // Start is called before the first frame update
    void Start()
    {
        numOfTurns = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //listen for player action here and update numOfTurns by 1
        //add a reset functionality somehow
    }
}
