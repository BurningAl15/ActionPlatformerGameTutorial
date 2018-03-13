using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealthManager : MonoBehaviour {
    
	public int startingHealth;
	public int currentHealth;
    public int maxAmount;

    public PlayerController thePlayer;

    public Image lifeImg;
    public Text lifeText;

    public float delay;
    float startingDelay;
    public bool call;
    public GameObject hurt;

    private Animator anim;
    public bool dead;
	// Use this for initialization
	void Start () {
        
        thePlayer = FindObjectOfType<PlayerController>();
        //thePlayer.enabled = true;
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        lifeText.text = "" + startingHealth + "%";
        maxAmount = 150;
        startingDelay = delay;
        call = false;
        hurt.SetActive(call);
        dead = false;   
    }
	
	// Update is called once per frame
	void Update () {
		if(currentHealth <= 0 && !dead)
		{
            //gameObject.SetActive(false);
            anim.SetTrigger("DeathP");
            dead = true;
            //thePlayer.enabled = false;
            Time.timeScale = 0.5f;
            gameObject.layer = LayerMask.NameToLayer("PDie");
        }
        
        

        lifeText.text = "" + currentHealth + "%";

        if(call)
        { 
        delay -= Time.deltaTime;
        }
        if (delay<=0)
        {
            call = false;
            hurt.SetActive(call);
            delay = startingDelay;
        }
        
    }

    public void HurtPlayer(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 100)
        {
            lifeImg.fillAmount -= 0.1f;
        }
        lifeImg.fillAmount -= 0f;
        thePlayer.Knockback();
        call = true;
        hurt.SetActive(call);
    }

    public void CurePlayer(int damage)
    {        
        if(currentHealth<150)
        {
            lifeImg.fillAmount += 0.1f;
            currentHealth += damage;
        }
        lifeImg.fillAmount += 0f;
        currentHealth += 0;
    }


}