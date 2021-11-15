using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    private PlayerController parent;

    public void setParent(PlayerController newParent)
    {
        parent = newParent;
    }

    void OnMouseDown()
    {
        parent.removeAllMoveTiles();
        parent.resetAllFirstMoves();

        if (parent.isRedTurn() == true)
        {
            parent.setPlayerTurnBlack();
        }
        else
        {
            parent.setPlayerTurnRed();
        }
    }
}
