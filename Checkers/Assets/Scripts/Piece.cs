using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour //so far this only highlights the pieces
{
    SpriteRenderer thisSprite;
    Color colorOrig;
    [SerializeField] private int teamColor; //0 equals black, 1 equals red
    private Vector3 mousePos;
    [SerializeField] private MoveTile moveTile;

    private MoveTile tile1;
    private MoveTile tile2;
    private MoveTile tile3;
    private MoveTile tile4;

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

    void OnMouseDown(){ //make sure other pieces are unselectable during this process
        if (Input.GetMouseButtonDown(0)){ //on left click
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("is this hitting? " + mousePos);
            spawnTiles();
        }
    }

    void spawnTiles(){
        tile1 = Instantiate(moveTile, new Vector3(this.transform.position.x + 2, this.transform.position.y + 2, 1), Quaternion.identity);
        tile2 = Instantiate(moveTile, new Vector3(this.transform.position.x - 2, this.transform.position.y + 2, 1), Quaternion.identity);

        tile1.setParent(this);
        tile2.setParent(this);
    }

    void destroyTiles(){
        Destroy(tile1);
        Destroy(tile2);
    }

    public void moveMe(Vector3 newPos) {
        this.transform.position = newPos;
        destroyTiles();
    }
}
