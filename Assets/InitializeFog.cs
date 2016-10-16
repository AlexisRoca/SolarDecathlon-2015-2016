using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class InitializeFog : MonoBehaviour {
	public string Camera = "Camera0";
	// Use this for initialization
	void Start () {
		GameObject camera = GameObject.Find (Camera);
		camera.AddComponent<GlobalFog>().fogShader = Shader.Find("Hidden/GlobalFog");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
