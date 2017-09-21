using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Vehicles.Car;

public class ColliderCheck : MonoBehaviour {

	public bool restartScene = false;
	public GameObject carObject;
	public 	CarController m_Car;
	public int waypoint = 1;

	void OnTriggerEnter(Collider collider) {
		
		restartScene = true;
	}

	void FixedUpdate(){
		if (restartScene) {
			if (waypoint == 1) {
				carObject.transform.position = new Vector3 (14, 1, 28);
				carObject.transform.rotation = new Quaternion(0,0,0,0);
			}

			if (waypoint == 2) {
				carObject.transform.position = new Vector3 (80, 0.37f, 57);
				carObject.transform.rotation = new Quaternion(0,-90,0,0);
			}

			restartScene = false;
			EventManager.TriggerEvent ("gameover");
		}
	}
		
}
