using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {

    PlayerHealthManager health;

	// Use this for initialization
	void Start () {
        health = FindObjectOfType<PlayerHealthManager>();
	}
	

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            health.CurePlayer(10);
            Destroy(gameObject);
        }
    }

}
