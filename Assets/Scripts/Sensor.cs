using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor {

	private int size = 10;
	private int yAngle;
	private Vector3 startPos;
	private Vector3 endPos;
	private Ray ray;
	private GameObject car;

	public Sensor(GameObject car, int yAngle) {
		this.car = car;
		this.yAngle = yAngle;
		ray = new Ray (car.transform.position, Quaternion.Euler (0, yAngle, 0) * car.transform.forward * size);
	}

	public void updateDirection() {
		ray = new Ray (car.transform.position, Quaternion.Euler (0, yAngle, 0) * car.transform.forward * size);
	}

	public double getSignalStrength() {
		double distance = 0;

		RaycastHit hit;
		// Mark collision from front
		if (Physics.Raycast(ray, out hit, size)) {
			Debug.DrawRay(car.transform.position, ray.direction, Color.red);
			//Debug.Log ("Front distance: " + hit.distance);

			distance = hit.distance / size;
		} else {
			Debug.DrawRay(car.transform.position, ray.direction, Color.green);
		}

		return distance > 0 ? 1 - distance : 0;
	}
	
}
