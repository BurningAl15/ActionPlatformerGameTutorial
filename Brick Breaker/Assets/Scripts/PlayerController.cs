using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
	public float moveSpeed;

	private Rigidbody2D myRB;

	private bool gameActive;

	public Rigidbody2D ball;
	public float ballForce;
	public BallController theBC;

	public Transform leftLimit;
	public Transform rightLimit;

	//public float ballSpeedModifier;

	// Use this for initialization
	void Start () {
		myRB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		myRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, 0f);
		if(transform.position.x > rightLimit.position.x && myRB.velocity.x > 0f)
		{
			myRB.velocity = Vector2.zero;
		} else if (transform.position.x < leftLimit.position.x && myRB.velocity.x < 0f)
		{
			myRB.velocity = Vector2.zero;
		}

		if(!gameActive)
		{
			ball.transform.position = new Vector3(transform.position.x, ball.transform.position.y, ball.transform.position.z);

			if(Input.GetButtonDown("Fire1"))
			{
				ball.velocity = new Vector2(Random.Range(-ballForce /2f, ballForce /2f), ballForce);
				theBC.ballSpeed = ballForce;
				gameActive = true;
			}
		}
	}

	/* void OnCollisionExit2D(Collision2D other)
	{
		if(other.gameObject.tag == "Ball")
		{
			Rigidbody2D ballRB = other.gameObject.GetComponent<Rigidbody2D>();
			if(myRB.velocity.x > 0)
			{
				ballRB.velocity = new Vector2(ballRB.velocity.x + ballSpeedModifier, ballRB.velocity;
			}
		}
	} */
}