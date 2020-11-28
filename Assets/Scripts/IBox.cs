using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBox : ILevelObject
{

    public GameObject boxCollider;
    
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D");
        if(hadTrigger == true)
            return;
        Debug.Log("if(hadTrigger = true)");
        if(other.tag == "Player")
            OnHeadHit(other);
        Debug.Log("other.tag == )");
    }

    public void OnHeadHit(Collider2D other){
        // gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        float vec_y = other.GetComponent<PlayerController>().rig2d.velocity.y;
        
        Debug.Log("觸發隱形方塊");
        // 只有往上時才觸發
        if(vec_y > 0) {
            SetBoxCollider(true);
            hadTrigger = true;
        }
    }

    public override void Reset(){
        base.Reset();
        SetBoxCollider(false);
    }

    public void SetBoxCollider(bool bo){
        boxCollider.SetActive(bo);
    }
}

