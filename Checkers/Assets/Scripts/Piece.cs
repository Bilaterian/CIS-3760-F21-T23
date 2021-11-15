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
    [SerializeField] private Sprite kingSprite;

    private MoveTile tile1;
    private MoveTile tile2;
    private MoveTile tile3;
    private MoveTile tile4;
    private List<MoveTile> moveTiles = new List<MoveTile>();

    private PlayerController parent;
    private bool isInteractable = true;
    private bool isKing = false;

    private Vector3 tempVector;
    private Player playerTurn;
    // public Text player;
    private bool isFirstMove = true;

    void Start(){
        thisSprite = GetComponent<SpriteRenderer>();
        colorOrig = thisSprite.color;
    }

    // Start is called before the first frame update
    void OnMouseOver(){
        if (this.isInteractable == false) return;
         thisSprite.color = Color.yellow;
    }

    void OnMouseExit() {
         thisSprite.color = colorOrig;
    }

    void OnMouseDown(){
        if (this.isInteractable == false) return;
        if (Input.GetMouseButtonDown(0)){ //on left click
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log("is this hitting? " + mousePos);
            parent.setPieceName(this.name);
            parent.removeAllMoveTiles();
            //remove all other 
            // spawnTiles();
            if (this.isKing)
            {
                highlightMoves(this.transform.position.x, this.transform.position.y, true, !isFirstMove);
                highlightMoves(this.transform.position.x, this.transform.position.y, false, !isFirstMove);
            } else if (this.teamColor == 0)
            {
                highlightMoves(this.transform.position.x, this.transform.position.y, false, !isFirstMove);
            } else
            {
                highlightMoves(this.transform.position.x, this.transform.position.y, true, !isFirstMove);
            }
        }
    }

    private void highlightMoves(float posX, float posY, bool directionIsUp, bool isMultiCapture)
    {
        int direction = directionIsUp ? 1 : -1;
        Vector3 tile1Vector;
        Vector3 tile2Vector;
        tile1Vector.x = posX + 2;
        tile2Vector.x = posX - 2;
        tile1Vector.y = posY + (2 * direction);
        tile2Vector.y = posY + (2 * direction);
        tile1Vector.z = -1;
        tile2Vector.z = -1;

        int boundCheck1 = checkBounds(tile1Vector);
        int boundCheck2 = checkBounds(tile2Vector);

        if (boundCheck1 == 1)
        {
            if (parent.checkIfTileOccupied(tile1Vector) == 0 && isMultiCapture == false)
            {
                var newMoveTile = Instantiate(moveTile, tile1Vector, Quaternion.identity);
                newMoveTile.setParent(this);
                this.moveTiles.Add(newMoveTile);
            }
            else if (parent.checkIfTileHasEnemyPiece(tile1Vector, teamColor) == 1)
            {
                var newSpotVector = tile1Vector;
                newSpotVector.x = tile1Vector.x + 2;
                newSpotVector.y = tile1Vector.y + (2 * direction);
                var boundCheckNewSpot = checkBounds(newSpotVector);
                if (boundCheckNewSpot == 1 && parent.checkIfTileOccupied(newSpotVector) == 0)
                {
                    var newMoveTile = Instantiate(moveTile, newSpotVector, Quaternion.identity);
                    newMoveTile.setParent(this);
                    this.moveTiles.Add(newMoveTile);
                    newMoveTile.setKill();
                }
            }
        }
        if (boundCheck2 == 1)
        {
            if (parent.checkIfTileOccupied(tile2Vector) == 0 && isMultiCapture == false)
            {
                var newMoveTile = Instantiate(moveTile, tile2Vector, Quaternion.identity);
                newMoveTile.setParent(this);
                this.moveTiles.Add(newMoveTile);
            }
            else if (parent.checkIfTileHasEnemyPiece(tile2Vector, teamColor) == 1)
            {
                var newSpotVector = tile2Vector;
                newSpotVector.x = tile2Vector.x - 2;
                newSpotVector.y = tile2Vector.y + (2 * direction);
                var boundCheckNewSpot = checkBounds(newSpotVector);
                if (boundCheckNewSpot == 1 && parent.checkIfTileOccupied(newSpotVector) == 0)
                {
                    var newMoveTile = Instantiate(moveTile, newSpotVector, Quaternion.identity);
                    newMoveTile.setParent(this);
                    this.moveTiles.Add(newMoveTile);
                    newMoveTile.setKill();
                }
            }
        }
    }

    public void destroyTiles(){
        foreach (var tile in this.moveTiles)
        {
            tile.DestroyMe();
        }
        this.moveTiles = new List<MoveTile>();
    }

    public void moveMe(Vector3 newPos, bool isKillMove) {
        newPos.z = -1;
        if (isKillMove){
            // send signal to player controller to kill all pieces that are in between the old piece transform and the new piece transform

            var x = this.transform.position.x;
            var y = this.transform.position.y;
            while (x != newPos.x || y != newPos.y)
            {
                if (x != newPos.x)
                {
                    x = (x < newPos.x) ? x + 2 : x - 2;
                }
                if (y != newPos.y)
                {
                    y = (y < newPos.y) ? y + 2 : y - 2;
                }
                var spotToCapture = new Vector3(x, y, -1);
                parent.killAPiece(spotToCapture, teamColor);
            }
        }
        this.transform.position = newPos;
        destroyTiles();
        var newPosIsEndOfBoard = newPos.y == 0 && teamColor == 0 || newPos.y == 14 && teamColor == 1;
        if (newPosIsEndOfBoard)
        {
            this.isKing = true;
            this.thisSprite.sprite = kingSprite;
        }

        if(isKillMove == false)
        {
            if (this.teamColor == 0)
            {
                this.parent.setPlayerTurnRed();
            }
            else
            {
                this.parent.setPlayerTurnBlack();
            }
        }
        else
        {
            isFirstMove = false;
            parent.setallButOneImmovable(this.transform.position);
        }
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

    public void disableInteractions() {
        this.isInteractable = false;
    }

    public void enableInteractions() {
        this.isInteractable = true;
    }

    public void setParent(PlayerController newParent){
        parent = newParent;
    }

    public void DestroyMe(){
        this.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this.GetComponent<BoxCollider2D>());
       // Destroy(this);
    }

    public void resetFirstMove()
    {
        isFirstMove = true;
    }
}
