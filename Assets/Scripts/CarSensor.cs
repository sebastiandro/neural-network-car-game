using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class CarSensor : MonoBehaviour {

	public GameObject car;
	public int yAngle;

	private Ray ray;
	public int raySize = 20;
	private double sensorStrength;

	RaycastHit hit;

	public void Awake() {
		ray = new Ray (car.transform.position, Quaternion.Euler (0, yAngle, 0) * transform.forward * raySize);
	}

	private void FixedUpdate() {
		double distance = 0;
		updateDirection ();

		ray = new Ray (car.transform.position, Quaternion.Euler (0, yAngle, 0) * transform.forward * raySize);

		Debug.Log ("senor update");

		if (Physics.Raycast(ray, out hit, raySize)) {
			Debug.DrawRay(car.transform.position, ray.direction, Color.red);
			distance = hit.distance / raySize; // returns percentage of
		} else {
			Debug.Log (Quaternion.Euler (0, yAngle, 0) * transform.forward * raySize);
			Debug.DrawRay(car.transform.position, Quaternion.Euler (0, yAngle, 0) * transform.forward * raySize, Color.green);
		}

		sensorStrength = distance > 0 ? 1 - distance : 0;
	}

	private void updateDirection(){
		ray.direction = Quaternion.Euler (0, yAngle, 0) * transform.forward * raySize;
	}

	public double GetSensorStrength() {
		return sensorStrength;	
	}
}
