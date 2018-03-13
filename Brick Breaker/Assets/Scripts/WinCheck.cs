using UnityEngine;
using System.Collections;

public class WinCheck : MonoBehaviour {
    
	public GameObject winScreen;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(FindObjectOfType<BrickController>() == null)
		{
			winScreen.SetActive(true);
			Time.timeScale = 0f;
		}
	}
}