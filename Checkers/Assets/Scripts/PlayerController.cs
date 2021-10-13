using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject redPiece;
    public GameObject blackPiece;
    private int[] blackX = new int[12] { 0, 0, 1, 2, 2, 3, 4, 4, 5, 6, 6, 7 };
    private int[] blackY = new int[12] { 7, 5, 6, 7, 5, 6, 7, 5, 6, 7, 5, 6 };
    private int[] redX = new int[12] { 0, 1, 1, 2, 3, 3, 4, 5, 5, 6, 7, 7 };
    private int[] redY = new int[12] { 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0 };
    private int i = 0;

    //track cursor
    // private vector2 mouseOver;


    // Start is called before the first frame update
    void Start()
    {
        setupPieces();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setupPieces(){
        
        for (i = 0; i < 12; i++) {
            var newPieceRed = Instantiate(redPiece, new Vector3(redX[i]*2, redY[i]*2, 0), Quaternion.identity);
            var newPieceBlack = Instantiate(blackPiece, new Vector3(blackX[i] * 2, blackY[i] * 2, 0), Quaternion.identity);
        }
    }

    //move pieces
    
}
