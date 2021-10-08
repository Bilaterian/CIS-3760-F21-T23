using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour //so far this only highlights the pieces
{
    SpriteRenderer thisSprite;
    Color colorOrig;

    void Start(){
        thisSprite = GetComponent<SpriteRenderer>();
        colorOrig = thisSprite.color;
    }

    // Start is called before the first frame update
    void OnMouseOver(){
        thisSprite.color = Color.yellow;
    }

    void OnMouseExit() {
        thisSprite.color = colorOrig;
    }
}
