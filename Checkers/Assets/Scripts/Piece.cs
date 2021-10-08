using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour //so far this only highlights the pieces
{
    SpriteRenderer thisSprite;
    Color colorOrig;
    [SerializeField] private int teamColor; //0 equals black, 1 equals red
    private bool moveMe;

    void Start(){
        thisSprite = GetComponent<SpriteRenderer>();
        colorOrig = thisSprite.color;
        moveMe = false;
    }

    // Start is called before the first frame update
    void OnMouseOver(){
        thisSprite.color = Color.yellow;
    }

    void OnMouseExit() {
        thisSprite.color = colorOrig;
    }

    void OnMouseClick() {
        moveMe = true;
        thisSprite.color = Color.yellow;
        if (Input.GetMouseButtonDown(0)) { //on left click
            RaycastHit hit = new RaycastHit();

        }


    }
}
