using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour {

    //DeadEnemiesCounter power;
    public GameObject powerUp;
    public GameObject particle;
    [Range(0,5)]
    public float addHeight;

	// Use this for initialization
	void Start () {
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            //power.call = true;
            Vector2 newPos = new Vector2(transform.position.x, transform.position.y + addHeight);
            Instantiate(powerUp, newPos, Quaternion.identity);
            Instantiate(particle, newPos, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
