using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyingEnemy : ILevelObject
{
    private Vector3 flyPos;
    public GameObject flyingPos;
    private Vector3 startPos;

    [Range(0f, 3f)]
    public float animeDur;
    private PlayerController _player;
    protected LayerMask enemyLayer;

	void Start ()
    {
        Init();
	}
	
    public void Init()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        startPos = transform.position;
        flyPos = flyingPos.transform.position;
    }

    public override void Reset(){
        base.Reset();
        transform.position = startPos;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player.Die();
        }
    }

    public override void OnTrigger()
    {
        base.OnTrigger();
        Fly();
    }

    private void Fly(){
        Sequence mySequence = DOTween.Sequence();
        float moveTo = flyPos.y;
        mySequence.Append(transform.DOMoveY(moveTo, animeDur ));

    }

}
