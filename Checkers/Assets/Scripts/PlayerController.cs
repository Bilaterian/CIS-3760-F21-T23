using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Piece redPiece;
    public Piece blackPiece;
    private int[] blackX = new int[12] { 0, 0, 1, 2, 2, 3, 4, 4, 5, 6, 6, 7 };
    private int[] blackY = new int[12] { 7, 5, 6, 7, 5, 6, 7, 5, 6, 7, 5, 6 };
    private int[] redX = new int[12] { 0, 1, 1, 2, 3, 3, 4, 5, 5, 6, 7, 7 };
    private int[] redY = new int[12] { 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0 };
    private int i = 0;

    private List<Piece> redPieces = new List<Piece>();
    private List<Piece> blackPieces = new List<Piece>(); //consider finding a different way of stroing multiple info underthe same key

    private int numRed = 12;
    private int numBlack = 12;

    private string pieceName;

    // Start is called before the first frame update
    void Start(){
        setupPieces();
    }


    void setupPieces(){
        for (i = 0; i < 12; i++) {
            var newPieceRed = Instantiate(redPiece, new Vector3(redX[i]*2, redY[i]*2, -1), Quaternion.identity);
            newPieceRed.name = $"Red Piece {i}";
            newPieceRed.setParent(this);
            redPieces.Add(newPieceRed); 

            var newPieceBlack = Instantiate(blackPiece, new Vector3(blackX[i] * 2, blackY[i] * 2, -1), Quaternion.identity);
            newPieceBlack.name = $"Black Piece {i}";
            newPieceBlack.setParent(this);
            blackPieces.Add(newPieceBlack); 
        }
    }

    void removeRedPiece(){
        numRed = numRed - 1;
    }

    void reomveBlackPiece(){
        numBlack = numBlack - 1;
    }

    void disableAllBlackPieces(){
        for (i = 0; i < 12; i++) {
            blackPieces[i].disableColl();
            redPieces[i].enableColl();
        }
    }

    void disableAllRedPieces(){
        for (i = 0; i < 12; i++){
            redPieces[i].disableColl();
            blackPieces[i].enableColl();
        }
    }

    public void setPieceName(string newName){
        pieceName = newName;
    }

    public void removeAllOtherMoveTiles(){
        for(i = 0; i< 12; i++){
            if(redPieces[i].name != pieceName){
                redPieces[i].destroyTiles();
            }
            if (blackPieces[i].name != pieceName){
                blackPieces[i].destroyTiles();
            }
        }
    }

    public int checkIfTileOccupied(Vector3 checkThis){
        checkThis.z = -1;
        for (i = 0; i < 12; i++){
            if (redPieces[i].transform.position == checkThis){
                Debug.Log("red: " + redPieces[i].transform.position + " " + checkThis);
                return 1;
            }
            if (blackPieces[i].transform.position == checkThis){
                Debug.Log("black: " + blackPieces[i].transform.position + " " + checkThis);
                return 1;
            }
        }

        return 0;
    }
}
