using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class CarSensors : MonoBehaviour {

	private GameObject car;
	public int rayDistance = 10;
	private int height = 10;
	private double sensorStrength;

	RaycastHit hit;

	Ray frontRay;

	public void Awake() {
		car = gameObject;
		Ray frontRay = new Ray (car.transform.position, car.transform.forward * 2 * rayDistance);
	}

	private void FixedUpdate() {
		RaycastHit hit;


		frontRay.origin = car.transform.position;
		frontRay.direction = (car.transform.forward * rayDistance);
		Ray rightRay = new Ray (car.transform.position, Quaternion.Euler(0, 45, 0) * car.transform.forward * rayDistance);
		Ray rightFrontRay = new Ray (car.transform.position, Quaternion.Euler(0, 35, 0) * car.transform.forward * rayDistance);
		Ray rightFrontRay2 = new Ray (car.transform.position, Quaternion.Euler(0, 25, 0) * car.transform.forward * rayDistance);
		Ray rightFrontRay3 = new Ray (car.transform.position, Quaternion.Euler(0, 15, 0) * car.transform.forward * rayDistance);
		Ray rightFrontRay4 = new Ray (car.transform.position, Quaternion.Euler(0, 5, 0) * car.transform.forward * rayDistance);

		Ray leftRay = new Ray (car.transform.position, Quaternion.Euler(0, -45, 0) * car.transform.forward * rayDistance);
		Ray leftFrontRay = new Ray (car.transform.position, Quaternion.Euler(0, -35, 0) * car.transform.forward * rayDistance);
		Ray leftFrontRay2 = new Ray (car.transform.position, Quaternion.Euler(0, -25, 0) * car.transform.forward * rayDistance);
		Ray leftFrontRay3 = new Ray (car.transform.position, Quaternion.Euler(0, -15, 0) * car.transform.forward * rayDistance);
		Ray leftFrontRay4 = new Ray (car.transform.position, Quaternion.Euler(0, -5, 0) * car.transform.forward * rayDistance);

		double frontDistance = 0;

		double leftDistance = 0;
		double rightFrontDistance = 0;
		double rightFrontDistance2 = 0;
		double rightFrontDistance3 = 0;
		double rightFrontDistance4 = 0;

		double rightDistance = 0;
		double leftFrontDistance = 0;
		double leftFrontDistance2 = 0;
		double leftFrontDistance3 = 0;
		double leftFrontDistance4 = 0;

		// Mark collision from front
		if (Physics.Raycast(frontRay, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, frontRay.direction * rayDistance, Color.red);
			//Debug.Log ("Front distance: " + hit.distance);

			frontDistance = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, frontRay.direction * rayDistance, Color.green);
		}

		// Mark collision from right
		if (Physics.Raycast(rightRay, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 45, 0) * car.transform.forward * rayDistance, Color.red);
			//Debug.Log ("Right distance: " + hit.distance);

			rightDistance = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 45, 0) * car.transform.forward * rayDistance, Color.green);
		}

		// Mark collision from right
		if (Physics.Raycast(rightFrontRay, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 35, 0) * car.transform.forward * rayDistance, Color.red);
			//Debug.Log ("Right distance: " + hit.distance);

			rightFrontDistance = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 35, 0) * car.transform.forward * rayDistance, Color.green);
		}



		// Mark collision from right
		if (Physics.Raycast(rightFrontRay2, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 25, 0) * car.transform.forward * rayDistance, Color.red);
			//Debug.Log ("Right distance: " + hit.distance);

			rightFrontDistance2 = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 25, 0) * car.transform.forward * rayDistance, Color.green);
		}

		// Mark collision from right
		if (Physics.Raycast(rightFrontRay3, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 15, 0) * car.transform.forward * rayDistance, Color.red);
			//Debug.Log ("Right distance: " + hit.distance);

			rightFrontDistance3 = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 15, 0) * car.transform.forward * rayDistance, Color.green);
		}

		// Mark collision from right
		if (Physics.Raycast(rightFrontRay4, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 5, 0) * car.transform.forward * rayDistance, Color.red);
			//Debug.Log ("Right distance: " + hit.distance);

			rightFrontDistance4 = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 5, 0) * car.transform.forward * rayDistance, Color.green);
		}


		// Mark collision from left
		if (Physics.Raycast(leftRay, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -45, 0) * car.transform.forward * rayDistance, Color.red);
			//Debug.Log ("Left distance: " + hit.distance);
			leftDistance = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -45, 0) * car.transform.forward * rayDistance, Color.green);
		}


		// Mark collision from right
		if (Physics.Raycast(leftFrontRay, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -35, 0) * car.transform.forward * rayDistance, Color.red);
			//Debug.Log ("Right distance: " + hit.distance);

			leftFrontDistance = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -35, 0) * car.transform.forward * rayDistance, Color.green);
		}

		// Mark collision from right
		if (Physics.Raycast(leftFrontRay2, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -25, 0) * car.transform.forward * rayDistance, Color.red);
			//Debug.Log ("Right distance: " + hit.distance);

			leftFrontDistance2 = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -25, 0) * car.transform.forward * rayDistance, Color.green);
		}

		// Mark collision from right
		if (Physics.Raycast(leftFrontRay3, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -15, 0) * car.transform.forward * rayDistance, Color.red);
			//Debug.Log ("Right distance: " + hit.distance);

			leftFrontDistance3 = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -15, 0) * car.transform.forward * rayDistance, Color.green);
		}

		// Mark collision from right
		if (Physics.Raycast(leftFrontRay4, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -5, 0) * car.transform.forward * rayDistance, Color.red);
			//Debug.Log ("Right distance: " + hit.distance);

			leftFrontDistance4 = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -5, 0) * car.transform.forward * rayDistance, Color.green);
		}
	}
}
