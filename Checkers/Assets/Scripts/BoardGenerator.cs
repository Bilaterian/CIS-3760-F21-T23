using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Tilemaps;

public class BoardGenerator : MonoBehaviour
{
    private int boardSize = 8;
    private int x, y;
    private int colorPicker;

    public GameObject tile1;
    public GameObject tile2;

    // Start is called before the first frame update
    void Start()
    {
        generateGrid();
    }

    void generateGrid() {
        Debug.Log("this got here");
        for (x = 0; x < boardSize; x++) {
            for (y = 0; y < boardSize; y++) {
                Debug.Log("x: " + x + " y: " + y + " Color: " + colorPicker);
                if (colorPicker == 0){
                    var newTile = Instantiate(tile1, new Vector3(x*2, y*2, 0), Quaternion.identity);
                    colorPicker = 1;
                }
                else {
                    var newTile = Instantiate(tile2, new Vector3(x*2, y*2, 0), Quaternion.identity);
                    colorPicker = 0;
                }
            }
            if (colorPicker == 0){
                colorPicker = 1;
            }
            else {
                colorPicker = 0;
            }
        }
    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
