using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWall : ILevelObject
{
    private Rigidbody2D rig2d;
    public Vector3 startPos;
    private PlayerController _player;
    public float fallingSpeed = 2;
    public bool canKillCat;
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init(){
        rig2d = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        startPos = transform.position;
        SetGravity(false);
        
    }
    public override void Reset(){
        base.Reset();
        SetGravity(false);
        transform.position = startPos;
        rig2d.velocity = Vector3.zero;
        canKillCat = false;
    }


    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player" && canKillCat)
            _player.Die();

    }

    public override void OnTrigger()
    {
        base.OnTrigger();
        SetGravity(true);
        canKillCat = true;
    }

    private void SetGravity(bool bo){
        if(bo)
            rig2d.gravityScale = fallingSpeed;
        else
            rig2d.gravityScale = 0;
    }

    
}
