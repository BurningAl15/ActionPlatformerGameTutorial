using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
    
	public float bulletSpeed;

	private Rigidbody2D myRB;
	private PlayerController thePlayer;

    //public GameObject leftExplosion;
    public GameObject rightExplosion;
    public GameObject currentExplosion;

    public int type;
    public float lifeDelay;
    float startingDelay;

	public int damageToGive;

    public float sprayRange;

	// Use this for initialization
	void Start () {
		myRB = GetComponent<Rigidbody2D>();
		thePlayer = FindObjectOfType<PlayerController>();
        currentExplosion = rightExplosion;
        //Si la posicion de la bala es menor que la del jugador, entonces la velocidad se multiplica * -1 y la imagen se escala en x a -1
        if (thePlayer!=null)
        {
            //Sol 1
            //currentExplosion = rightExplosion;
            if (transform.position.x < thePlayer.transform.position.x)
		    {
			    bulletSpeed = -bulletSpeed;
			    transform.localScale = new Vector3(-1f, 1f, 1f);
                //currentExplosion = leftExplosion;
		    }

            sprayRange = Random.Range(-sprayRange, sprayRange);
        }
        //Reutilizable
        switch (type)
        {
            default:
            case 0:
                lifeDelay = 0.7f;
                startingDelay = lifeDelay;
                break;
            case 1:
                lifeDelay = 0.8f;
                startingDelay = lifeDelay;
                break;
            case 2:
                lifeDelay = 0.9f;
                startingDelay = lifeDelay;
                break;
            case 3:
                lifeDelay = 0.6f;
                startingDelay = lifeDelay;
                break;
        }


	}
	
	// Update is called once per frame
	void Update () {

		myRB.velocity = new Vector2(bulletSpeed, sprayRange);
        lifeDelay -= Time.deltaTime;
        if(lifeDelay<=0)
        {
            Destroy(gameObject);
            
        }
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Enemy")
		{
			other.gameObject.GetComponent<EnemyHealthManager>().TakeDamage(damageToGive);
            
        }
        //Sol 1
        //Instantiate(currentExplosion,this.transform.position,Quaternion.identity);
        GameObject explosion= (GameObject) Instantiate(currentExplosion, this.transform.position, transform.rotation);
        explosion.transform.localScale = transform.localScale;
        SoundManager.instance.PlayExplosion(thePlayer.soundChecker);
        Destroy(gameObject);
	}
}