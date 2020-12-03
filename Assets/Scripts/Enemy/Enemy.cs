using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ILevelObject
{
    protected PlayerController _player;
    protected Rigidbody2D enemyRig;

    protected bool isHit = false;
    protected bool isDead = false;
    [SerializeField] protected bool canMove = true;

    protected Animator anim;
    protected AnimatorStateInfo stateInfo;

    protected LayerMask enemyLayer;

    protected float speed = 1f;
    protected Vector3 dir = new Vector3(-1, 0, 0);

    protected Vector3 startPosition;
    [SerializeField] protected GameObject body;

    void Start()
    {
        Init();
    }

    protected virtual void Init() {
        startPosition = transform.position;
        enemyLayer = LayerMask.NameToLayer("Enemy");
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemyRig = GetComponent<Rigidbody2D>();
    }

    public override void Reset(){
        Debug.Log("重置怪物位置" + startPosition);
        transform.position = startPosition;
        isHit = false;
        isDead = false;
        enemyRig.isKinematic = false;
        OpenCollidersInChild(this.transform);
        body.SetActive(true);
    }

    protected void Move()
    {
        transform.position += dir * Time.deltaTime * speed;
    }

    protected void Reverse()
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    protected void ChangeMoveDir()
    {
        dir.x *= -1;
        Reverse();
    }

    protected void CloseCollidersInChild(Transform rTran)
    {
        var tempTrans = rTran.GetComponentsInChildren<BoxCollider2D>();
        foreach (var child in tempTrans)
        {
            child.enabled = false;
        }
    }

    protected void OpenCollidersInChild(Transform rTran)
    {
        var tempTrans = rTran.GetComponentsInChildren<BoxCollider2D>();
        foreach (var child in tempTrans)
        {
            child.enabled = true;
        }
    }

    

    protected virtual void GetHit() { }
}
