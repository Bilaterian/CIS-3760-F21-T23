using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    private Piece parent;
    private bool isKillMove = false;

    Vector3 getPos() { 
        return this.transform.position;
    }

    void OnMouseDown() {
        parent.moveMe(getPos(), isKillMove);
        
    }

    public void setParent(Piece newParent) {
        parent = newParent;
    }

    public void DestroyMe(){
        this.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this.GetComponent<BoxCollider2D>());
        Destroy(this);
    }

    public void setKill() {
        isKillMove = true;
    }
}
