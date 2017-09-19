using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderCheck : MonoBehaviour {

	public bool restartScene = false;

	void OnTriggerEnter(Collider collider) {
		
		restartScene = true;
	}

	void Update(){
		if (restartScene) {
			Application.LoadLevel (0);
		}
	}
		
}
