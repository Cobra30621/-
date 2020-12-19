using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGround : ILevelObject
{
    private Rigidbody2D rig2d;
    public Vector3 startPos;
    
    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    private void Init(){
        rig2d = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        SetGravity(false);
    }
    public override void Reset(){
        base.Reset();
        if(rig2d == null)
            rig2d = GetComponent<Rigidbody2D>();
        
        SetGravity(false);
        transform.position = startPos;
        rig2d.velocity = Vector3.zero;
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
            SetGravity(true);
            
    }

    private void SetGravity(bool bo){
        if(bo)
            rig2d.gravityScale = 1;
        else
            rig2d.gravityScale = 0;
    }

}
