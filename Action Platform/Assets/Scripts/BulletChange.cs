using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletChange : MonoBehaviour {

    public Image img;
    public Sprite[] sprites;
    public Text txt;

    PlayerController player;

	// Use this for initialization
	void Start () {
        //img = GetComponent<Image>();
        //txt = GetComponent<Text>();
        
        player = FindObjectOfType<PlayerController>();

        img.sprite = sprites[0];
    }
	
	// Update is called once per frame
	void Update () {
        txt.text = ("" + player.bulletName);
        switch (player.armVal)
        {
            default:
            case 0:
                img.sprite = sprites[0];
                break;
            case 1:
                img.sprite = sprites[1];
                break;
            case 2:
                img.sprite = sprites[2];
                break;
        }
    }
}
