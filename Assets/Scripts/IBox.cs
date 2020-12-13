using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBox : ILevelObject
{
    public GameObject showObject;
    public GameObject boxCollider;
    private BoxCollider2D boxCollider2D;
    public Color color;
    
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
    }

    public void OnHeadHit(Collider2D other){
        // gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        float vec_y = other.GetComponent<PlayerController>().rig2d.velocity.y;
        
        Debug.Log("觸發隱形方塊");
        // 只有往上時才觸發
        if(vec_y > 0) {
            SetBoxCollider(true);
            hadTrigger = true;
            showObject.SetActive(true);
            showObject.GetComponent<ShowObject>().Show();
        }
    }

    public override void Reset(){
        base.Reset();
        SetBoxCollider(false);
        showObject.GetComponent<ShowObject>().Reset();
        showObject.SetActive(false);
    }

    public void SetBoxCollider(bool bo){
        boxCollider.SetActive(bo);
    }

    void OnDrawGizmos()
    {
        boxCollider2D = boxCollider.GetComponent<BoxCollider2D>();
        Vector3 scale = transform.localScale;
        Vector3 size = new Vector3 ( boxCollider2D.size.x * scale.x, boxCollider2D.size.y * scale.y, 1);
        Vector3 offset = new Vector3 ( boxCollider2D.offset.x * scale.x, boxCollider2D.offset.y * scale.y, 1);

        Gizmos.color = color;
        Gizmos.DrawWireCube(this.transform.position + offset, size);
 
        Gizmos.color = new Color (color.r, color.g, color.b, 0.4f);
        Gizmos.DrawCube(this.transform.position + offset, size);
    }
}

