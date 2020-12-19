using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Portal : MonoBehaviour
{
    // public Vector3 ChangePos;
    public GameObject ChangePos;
    public MapName myMap;
    public Color color;
    
    private BoxCollider2D boxCollider2D;

    void Start(){

    }

    void OnTriggerEnter2D (Collider2D other){
        Debug.Log("SomeThingEnter");
		if(other.tag == "Player"){
		}
	}

    void OnTriggerExit2D (Collider2D other){
        if(other.tag == "Player"){
            ChangeScene();
		}
    }


    public void ChangeScene(){
        Vector3 vector= ChangePos.transform.position;
        MapName changeMap = ChangePos.GetComponent<Portal>().GetProtalMapName();
        LevelManager.ChangeScene(vector, changeMap);
    }

    public MapName GetProtalMapName(){
        return myMap;
    }


    void OnDrawGizmos()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        Vector3 scale = transform.localScale;
        Vector3 size = new Vector3 ( boxCollider2D.size.x * scale.x, boxCollider2D.size.y * scale.y, 1);
        Vector3 offset = new Vector3 ( boxCollider2D.offset.x * scale.x, boxCollider2D.offset.y * scale.y, 1);

        Gizmos.color = color;
        Gizmos.DrawWireCube(this.transform.position + offset, size);
 
        Gizmos.color = new Color (color.r, color.g, color.b, 0.4f);
        Gizmos.DrawCube(this.transform.position + offset, size);
    }

}