using UnityEngine;
using System.Collections;

public class EnemyHealthManager : MonoBehaviour {

    public int startingHealth;
    private int currentHealth;

    public SpriteRenderer[] bodyParts;
    public float flashLength;
    float flashCounter;

    private EnemyController ec;
    private Animator anim;
    private Rigidbody2D myRB;

    public float deathSpin;
    public BoxCollider2D[] colliders;

    public Sprite[] brokenParts;

    bool dead;

    public GameObject explosion;

    public Rigidbody2D[] RBParts;
    public float explosionForce;

    PlayerController player;

    // Use this for initialization
    void Start() {
        currentHealth = startingHealth;
        ec = GetComponent<EnemyController>();
        anim = GetComponent<Animator>();
        myRB = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        if (currentHealth <= 0 && !dead)
        {
            //gameObject.SetActive(false);
            ec.enabled = false; 
            anim.enabled = false;

            //Deactivate the constraints from the rigidbody
            myRB.constraints = RigidbodyConstraints2D.None;
            myRB.AddTorque(deathSpin);
            gameObject.layer = LayerMask.NameToLayer("Bodies");

            for(int i=0;i<brokenParts.Length;i++)
            {
                bodyParts[i].sprite = brokenParts[i];
            }

            Instantiate(explosion, transform.position, transform.rotation);
            SoundManager.instance.PlayExplosion(player.soundChecker);

            for (int i=0;i<RBParts.Length;i++)
            {
                RBParts[i].isKinematic = false;
                RBParts[i].AddTorque(deathSpin);
                RBParts[i].velocity = new Vector2(Random.Range(-explosionForce, explosionForce), Random.Range(-explosionForce, explosionForce));
            }

            dead = true;
            DeadEnemiesCounter.KilledEnemies();
        }

        if(flashCounter>0)
        {
            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)
            {
                for (int i = 0; i < bodyParts.Length; i++)
                {
                    bodyParts[i].material.SetFloat("_FlashAmount", 0);
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        ec.Knockback();
        Flash();
    }

    public void Flash()
    {
        for(int i=0;i<bodyParts.Length;i++)
        {
            bodyParts[i].material.SetFloat("_FlashAmount", 1);
        }
        flashCounter = flashLength;

    }

}