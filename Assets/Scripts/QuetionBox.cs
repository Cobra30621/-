using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuetionBox : ILevelObject
{
    public GameObject showObject;
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private Sprite quetionIcon;
    [SerializeField] private Sprite emptyIcon;
    
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
        float vec_y = other.GetComponent<PlayerController>().rig2d.velocity.y;
        
        Debug.Log("觸發問號方塊");
        // 只有往上時才觸發
        if(vec_y > 0) {
            hadTrigger = true;
            icon.sprite = emptyIcon;
            showObject.SetActive(true);
            showObject.GetComponent<ShowObject>().Show();
        }
    }


    public override void Reset(){
        base.Reset();
        showObject.GetComponent<ShowObject>().Reset();
        icon.sprite = quetionIcon;
    }

}
