using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadEnemiesCounter : MonoBehaviour {

    public Text enemyTxt;
    public static int killedCounter;

    public GameObject cure;

    //Positions
    [Range(0,10)]
    public float xPos;
    Vector3 newPos;
    public bool call;

    //Time for delaying
    public float delay;
    float startingDelay;

    // Use this for initialization
    void Start () {
        killedCounter = 0;
        enemyTxt.text = "Killed enemies: " + killedCounter;
        //Corregir
        xPos = Random.Range(-xPos, xPos);
        newPos = new Vector3(transform.position.x + xPos, transform.position.y, transform.position.z);
        call = false;

        //Delay
        startingDelay = delay;
    }

    // Update is called once per frame
    void Update() {
        enemyTxt.text = "Killed enemies: " + killedCounter;
        
        call = false;

        //if(Input.GetKeyDown(KeyCode.P))
        //{
        //    call = true;
        //}

        if (call)
        {
            delay -= Time.deltaTime;
            if(delay<=0)
            {
                var value = Instantiate(cure, newPos, Quaternion.identity);
                delay = startingDelay;
            }    
        }

    }

    void instantiation()
    {
        var value = Instantiate(cure, newPos, Quaternion.identity);
        call = true;
    }



    public static void KilledEnemies()
    {
        killedCounter++;
    }

}
