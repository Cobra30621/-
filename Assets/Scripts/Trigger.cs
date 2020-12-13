using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : ILevelObject
{

    // Start is called before the first frame update
    private BoxCollider2D boxCollider2D;
    public Color color;
    public ILevelObject levelObject;
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(hadTrigger == true)
            return;
        if(other.tag == "Player")
        {
            if(levelObject == null){
                Debug.Log("找不到LevelObject");
                return;
            }
            levelObject.OnTrigger();
            hadTrigger = true;
        }
            
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
