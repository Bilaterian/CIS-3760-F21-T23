using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    private Piece parent;

    Vector3 getPos() { 
        return this.transform.position;
    }

    void OnMouseDown() {
        parent.moveMe(getPos());
        
    }

    public void setParent(Piece newParent) {
        parent = newParent;
    }
}
