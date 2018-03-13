using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeOnStart : MonoBehaviour {

    public float shakeAmount;

    CameraController camera;


	// Use this for initialization
	void Start () {
        camera = FindObjectOfType<CameraController>();
        camera.ScreenShake(shakeAmount);
	}

}
