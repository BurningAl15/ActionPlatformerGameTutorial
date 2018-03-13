using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
	public float moveSpeed;
	public float jumpSpeed;

	private Rigidbody2D myRB;

	private Animator anim;

    //Bullets section
	public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;
    public GameObject mainBullet;
    public GameObject muzzleFlash;

    public int armVal;
    public Transform bulletPoint;

	public Transform groundPoint;
	public LayerMask whatIsGround;
	public float groundRadius;

	public bool grounded;

    //Time to wait between shoots
    public float waitBetweenShots;
    float betweenShotCounter;

    public string bulletName;

    CameraController theCamera;

    public float shakeAmount;

    //Knockback Force
    public float knockbackForce;
    public float knockbackLength;
    float knockbackCounter;
    bool knockback;

    PlayerHealthManager health;

    public int soundChecker;

    // Use this for initialization
    void Start () {
		myRB = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
        armVal = 0;
        mainBullet = bullet1;
        muzzleFlash.SetActive(false);
        BulletNameChange(armVal);
        theCamera = FindObjectOfType<CameraController>();
        health = GetComponent<PlayerHealthManager>();
        soundChecker = 0;
    }

	void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle(groundPoint.position, groundRadius, whatIsGround);
	}
	
	// Update is called once per frame
	void Update () {
        muzzleFlash.SetActive(false);

        if (knockback)
        {
            knockbackCounter -= Time.deltaTime;
            myRB.velocity = new Vector2(-knockbackForce * transform.localScale.x, myRB.velocity.y);

            if (knockbackCounter <= 0)
            {
                knockback = false;
            }

        }
        else if(!health.dead)
        {


            myRB.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, myRB.velocity.y);

            if (Input.GetButtonDown("Jump") && grounded)
            {
                myRB.velocity = new Vector2(myRB.velocity.x, jumpSpeed);
                SoundManager.instance.PlayJump();
            }

            if (Input.GetAxisRaw("Horizontal") > 0 && transform.localScale.x < 0 && betweenShotCounter==0)
                transform.localScale = new Vector3(1f, 1f, 1f);
            if (Input.GetAxisRaw("Horizontal") < 0 && transform.localScale.x > 0 && betweenShotCounter == 0)
                transform.localScale = new Vector3(-1f, 1f, 1f);

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                armVal++;
                if (armVal == 0)
                {
                    soundChecker = 0;
                }
                else
                    soundChecker = 1;
                if (armVal > 2)
                {
                    armVal = 0;
                }
                BulletNameChange(armVal);
            }


            if (Input.GetButtonDown("Fire1"))
            {

                Shooting(armVal);
                //
                betweenShotCounter = waitBetweenShots;
            }

            if (Input.GetButton("Fire1"))
            {
                betweenShotCounter -= Time.deltaTime;
                if (betweenShotCounter <= 0)
                {
                    Shooting(armVal);
                }
            }

            if (Input.GetButtonUp("Fire1"))
            {
                //
                betweenShotCounter = 0;
            }
        }
        if(health.dead)
        {
            if(myRB.velocity.x>0.1f)
                myRB.velocity = new Vector2(myRB.velocity.x / 2f, myRB.velocity.y);
            else
                myRB.velocity = new Vector2(0f, myRB.velocity.y);
        }
		anim.SetFloat("Speed", Mathf.Abs(myRB.velocity.x));
		anim.SetBool("Grounded", grounded);
        Debug.Log("" + myRB.velocity.x);
	}

    void Shooting(int bulletNum)
    {
        switch(bulletNum)
        {
            default:
            case 0:
                mainBullet = bullet1;
                waitBetweenShots = 0.1f;
                break;
            case 1:
                mainBullet = bullet2;
                waitBetweenShots = 0.25f;
                break;
            case 2:
                mainBullet = bullet3;
                waitBetweenShots = 0.35f;
                break;
        }

        Instantiate(mainBullet, bulletPoint.position, transform.rotation);
        SoundManager.instance.PlayShoot(soundChecker);

        betweenShotCounter = waitBetweenShots;
        muzzleFlash.SetActive(true);
        theCamera.ScreenShake(shakeAmount);
    }

    public void BulletNameChange(int bulletNum)
    {
        string blltName;
        switch (bulletNum)
        {
            default:
            case 0:
                blltName = "Small Bullet";
                break;
            case 1:
                blltName = "Medium Bullet";
                break;
            case 2:
                blltName = "Big Bullet";
                break;
        }
        bulletName = blltName;
    }

    public void Knockback()
    {
        knockbackCounter = knockbackLength;
        myRB.velocity = new Vector2(-knockbackForce * transform.localScale.x, knockbackForce);
        knockback = true;
    }

    void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			Debug.Log("On Enemy");
			myRB.velocity = new Vector2(myRB.velocity.x, jumpSpeed * 0.75f);
            
        }
	}
}