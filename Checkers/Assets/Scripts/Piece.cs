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

    private PlayerController parent;

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
            //Debug.Log("is this hitting? " + mousePos);
            parent.setPieceName(this.name);
            parent.removeAllOtherMoveTiles();
            //remove all other 
            spawnTiles();
        }
    }

    void spawnTiles() {
        Vector3 tile1Vector;
        Vector3 tile2Vector;
        Vector3 tile3Vector;
        Vector3 tile4Vector;
        int boundCheck1;
        int boundCheck2;
        int boundCheck3;
        int boundCheck4;
        int collCheck1;
        int collCheck2;
        int collCheck3;
        int collCheck4;

        //check for king here later
        tile1Vector.x = this.transform.position.x + 2;
        tile2Vector.x = this.transform.position.x - 2;
        tile1Vector.z = 0;
        tile2Vector.z = 0;

        if(teamColor == 1){ //if red
            tile1Vector.y = this.transform.position.y + 2;
            tile2Vector.y = this.transform.position.y + 2;
        }
        else{ //if black 
            tile1Vector.y = this.transform.position.y - 2;
            tile2Vector.y = this.transform.position.y - 2;
        }

        //add boundary checks here
        boundCheck1 = checkBounds(tile1Vector);
        boundCheck2 = checkBounds(tile2Vector);

        //add piece collision check here, need reference to player controller

        //check before spawning
        if (boundCheck1 == 1 && parent.checkIfTileOccupied(tile1Vector) == 0) {
            tile1 = Instantiate(moveTile, tile1Vector, Quaternion.identity);
            tile1.setParent(this);
        }
        if (boundCheck2 == 1 && parent.checkIfTileOccupied(tile2Vector) == 0) {
            tile2 = Instantiate(moveTile, tile2Vector, Quaternion.identity);
            tile2.setParent(this);
        }
    }

    public void destroyTiles(){
        if (tile1 != null) {
            tile1.DestroyMe();
        }
        if (tile2 != null) {
            tile2.DestroyMe();
        }
    }

    public void moveMe(Vector3 newPos) {
        newPos.z = -1;
        this.transform.position = newPos;
        destroyTiles();
    }

    private int checkBounds(Vector3 toCheck) {
        //check x and y
        if ((toCheck.x >= 0 && toCheck.x <= 14) && (toCheck.y >= 0 && toCheck.y <= 14)){
            return 1;
        }
        else{
            return 0;
        }
    }

    Vector3 getPos() {
        return this.transform.position;
    }

    public void disableColl() { 
    
    }

    public void enableColl() { 
    
    }

    public void setParent(PlayerController newParent){
        parent = newParent;
    }


}
