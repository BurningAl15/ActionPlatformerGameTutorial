using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{

    private ScoreManager theSM;

    private Rigidbody2D myRB;
    public float maxSideSpeed;

    public float ballSpeed;

    public float speedUpValue;

    public GameObject gameOverScreen;

    // Use this for initialization
    void Start()
    {
        theSM = FindObjectOfType<ScoreManager>();
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Physics
        if (myRB.velocity.x > maxSideSpeed)
        {
            myRB.velocity = new Vector2(maxSideSpeed, myRB.velocity.y);
        }
        else if (myRB.velocity.x < -maxSideSpeed)
        {
            myRB.velocity = new Vector2(-maxSideSpeed, myRB.velocity.y);
        }

        if (myRB.velocity.y > 0)
        {
            myRB.velocity = new Vector2(myRB.velocity.x, ballSpeed);
        }
        else if (myRB.velocity.y < 0)
        {
            myRB.velocity = new Vector2(myRB.velocity.x, -ballSpeed);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        //Physics
        if (other.gameObject.tag == "brick")
        {
            theSM.AddScore(other.gameObject.GetComponent<BrickController>().brickValue);
            other.gameObject.SetActive(false);
            ballSpeed += speedUpValue;
        }

        if (other.gameObject.tag == "Player" && Mathf.Abs(myRB.velocity.x) < 0.1f)
        {
            myRB.velocity = new Vector2(Random.Range(-1f, 1f), myRB.velocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Killzone")
        {
            gameOverScreen.SetActive(true);
            Destroy(gameObject);
        }
    }
}