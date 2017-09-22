using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Vehicles.Car;

public class ColliderCheck : MonoBehaviour {

	public bool restartScene = false;
	public GameObject carObject;

	void OnTriggerEnter(Collider collider) {
		restartScene = true;
	}

	void FixedUpdate(){
		if (restartScene) {
			carObject.transform.position = new Vector3 (14, 1, 28);
			carObject.transform.rotation = new Quaternion(0,0,0,0);

			restartScene = false;
			EventManager.TriggerEvent ("gameover");
		}
	}
		
}
