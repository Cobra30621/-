using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy
{

	void Start ()
    {
        Init();
	}
	
	void Update ()
    {
        // stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (!_player.isDead && !isHit && !isDead && !canMove)
        {
            Move();
        }

        if (_player.isDead)
        {
            // anim.speed = 0;
        }

        if (isHit)
        {
            GetHit();
        }
	}

    public void SetCanMove(bool bo){
        canMove = bo;
    }

    protected override void Init()
    {
        base.Init();
        // anim = GetComponentInChildren<Animator>();
    }

    protected override void GetHit()
    {
        // anim.SetTrigger("Hit");
        CloseCollidersInChild(this.transform);
        enemyRig.isKinematic = true;
        body.SetActive(false);
        // if (stateInfo.IsName("Hit") && stateInfo.normalizedTime >= 1f)
        // {
        //     this.gameObject.SetActive(false);
        // }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _CheckPlayerPos();
        }
        else if (collision.CompareTag("Border") || collision.gameObject.layer == enemyLayer)
        {
            ChangeMoveDir();
        }
    }

    private void _CheckPlayerPos()
    {
        var playerPos = _player.checkPoint.position;
        var curPos = transform.position;

        if (playerPos.y - curPos.y > 0)
        {
            isHit = true;
            _player.Bounce();
        }
        else
        {
            _player.Die();
        }
    }
}
