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

    public void DestroyMe(){
        this.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this.GetComponent<BoxCollider2D>());
        Destroy(this);
    }
}
