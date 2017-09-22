using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor {

	private int size = 5;
	private int yAngle;
	private Vector3 startPos;
	private Vector3 endPos;
	private Ray ray;
	private GameObject car;
	private bool isHit = false;
	private double hitDistance;
	private bool showRay = true;

	public Sensor(GameObject car, int yAngle) {
		this.car = car;
		this.yAngle = yAngle;
		ray = new Ray (car.transform.position, Quaternion.Euler (0, yAngle, 0) * car.transform.forward);
	}

	public void updateDirection() {
		ray.direction = (Quaternion.Euler (0, yAngle, 0) * car.transform.forward);
		ray.origin = car.transform.position;
	}

	public void displaySensor(){
		Color color = isHit ? Color.red : Color.green;
		Debug.DrawRay(car.transform.position, ray.direction * size, color);
	}

	public void sense() {
		hitDistance = 0;
		RaycastHit hit;
		isHit = Physics.Raycast (ray, out hit, size);

		if (isHit) {
			hitDistance = hit.distance / size;
		}
	}

	public double getSignalStrength() {
		updateDirection ();
		sense ();
		if (showRay) displaySensor ();

		return hitDistance > 0 ? 1 - hitDistance : 0;
	}
	
}
