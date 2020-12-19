using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("玩家移動數值")]
    [Range(0f,1000f)]
    public float curSpeed = 3f;

    [Range(0f, 100f)]
    public float jumpHeight = 10f;
	float checkDistance = 0.05f;

	public bool isDead = false;
	public bool enableControl = true;
    internal bool isGrounded = false;

	public LevelManager  _levelManager;

	[SerializeField]
    Animator anim;
	[SerializeField]
    internal Rigidbody2D rig2d;

	public Transform checkPoint;

	LayerMask groundLayer;
    LayerMask enemyLayer;

    Animator playerAnim;
    AnimatorStateInfo stateInfo;

	// 音效
	public enum PlayerSE{Jump, Dead};
	[SerializeField] private AudioClip jumpSE;
	[SerializeField] private AudioClip deadSE;
	public Dictionary<PlayerSE, AudioClip> SEs;




    void Start()
    {
        Init();
    }

	void Init()
    {
        rig2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        checkPoint = transform.Find("GroundCheckPoint");
        playerAnim = GetComponent<Animator>();

        groundLayer = 1 << LayerMask.NameToLayer("Ground");
        enemyLayer = 1 << LayerMask.NameToLayer("Enemy");

		SEs = new Dictionary<PlayerSE, AudioClip>(){
		{PlayerSE.Jump, jumpSE}, {PlayerSE.Dead, deadSE}
		};

    }

    // Update is called once per frame
    void Update()
    {
		// CheckIsGrounded();
        InputProcess();
    }
    private void InputProcess(){
        if (isDead )
            return;
		
		if(! enableControl)
			return;

		Move(0);
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) )
		{
			Move(1);
			Direction(0);
		}
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			Move(-1);
			Direction(1);
		}
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
		{
			Jump();
		}
		if (rig2d.velocity.y < 0)
		{
			// anim.SetBool("Jump", false);
		}
		if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			Move(0);
		}
		if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			Move(0);
		}
	}

	//=======
	// 行動
	//=======
	public virtual void Jump()
	{
		
		if (!isGrounded) 
		{
			return; 
		}
		PlaySE(PlayerSE.Jump);
		rig2d.velocity = new Vector2(rig2d.velocity.x, jumpHeight);
	}

	public virtual void Move(int dic)
	{
		// rig2d.velocity = new Vector2(i * speed * Time.deltaTime, rig2d.velocity.y);
		rig2d.velocity = new Vector2(dic * curSpeed, rig2d.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(dic * curSpeed));
        // anim.SetFloat("MoveSpeed", curSpeed);
	}

	public virtual void Direction(int i)
	{
		transform.eulerAngles = new Vector3(0, 180 * i, 0);
	}

	public void SetEanbleControl(){
		enableControl = true;
		SetGravity(true);
	}

	void CheckIsGrounded()
	{
		Vector2 check = checkPoint.position;
		RaycastHit2D hit = Physics2D.Raycast(check, Vector2.down, checkDistance, groundLayer.value);

		if (hit.collider != null)
		{
			anim.SetBool("IsGrounded", true);
			isGrounded = true;
		}
		else
		{
			anim.SetBool("IsGrounded", false);
			isGrounded = false;
		}
	}

	public void Die()
	{
		Debug.Log("Player Die!");
		LevelManager.StopBGM();
		PlaySE(PlayerSE.Dead);
		isDead = true;
		enableControl = false;
		anim.SetBool("Die", true);
		rig2d.velocity = new Vector2(0, 0);
		
	}

	// 死亡動畫結束後呼叫
	public void DieFinish(){
		Debug.Log("DieFinish");
		_levelManager.PlayerDead();
		SetGravity(false);
	}

	public void Reset(){
		isDead = false;
		rig2d.velocity = Vector3.zero;
		anim.SetBool("Die", false);
	}

	public void Bounce()
	{
		rig2d.velocity = new Vector2(rig2d.velocity.x, 20f);
	}

	public void PlaySE(PlayerSE se){
		AudioClip ac = SEs[se];
		AudioSource.PlayClipAtPoint(ac, Camera.main.transform.position);
	}

	public void SetGravity(bool bo){
        if(bo)
            rig2d.gravityScale = 3.5f;
        else
            rig2d.gravityScale = 0;
    }

	//==========
	// 動畫
	//==========
	public void StateMachine()
	{
		// anim.SetBool("Ground", isGround);
		// anim.SetFloat("Y", rig2d.velocity.y);
		//anim.SetFloat("Ammo", Game.sav.HasAmmo() ? 1 : 0);
		// state = anim.GetCurrentAnimatorStateInfo(0);
	}

	//=======
	// 碰撞
	//=======
	void OnCollisionStay2D(Collision2D col)
	{
		if (col.contacts[0].normal != Vector2.up) { return; }
		anim.SetBool("IsGrounded", true);
		isGrounded = true;
	}

	void OnCollisionExit2D(Collision2D col)
	{
		anim.SetBool("IsGrounded", false);
		isGrounded = false;
	}

		
}
