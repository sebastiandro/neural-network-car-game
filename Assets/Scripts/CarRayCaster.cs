using UnityEngine;
using System.Collections;

public class CarRayCaster : MonoBehaviour
{

	public GameObject car;
	int rayDistance = 5;
	int rayDistanceFront = 20;

	void Start() {
		Debug.Log ("Start");

		NeuralNetwork neu = new NeuralNetwork ();
	}

	void Update(){
		RaycastHit hit;

		Ray frontRay = new Ray (car.transform.position, transform.forward * 2 * rayDistance);
		Ray rightRay = new Ray (car.transform.position, Quaternion.Euler(0, 45, 0) * transform.forward * rayDistance);
		Ray leftRay = new Ray (car.transform.position, Quaternion.Euler(0, -45, 0) * transform.forward * rayDistance);

		// Mark collision from front
		if (Physics.Raycast(frontRay, out hit, rayDistanceFront)) {
			Debug.DrawRay(car.transform.position, transform.forward * rayDistanceFront, Color.red);
			Debug.Log ("Front distance: " + hit.distance);
		} else {
			Debug.DrawRay(car.transform.position, transform.forward * rayDistanceFront, Color.green);
		}

		// Mark collision from right
		if (Physics.Raycast(rightRay, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 45, 0) * transform.forward * rayDistance, Color.red);
			Debug.Log ("Right distance: " + hit.distance);
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 45, 0) * transform.forward * rayDistance, Color.green);
		}

		// Mark collision from left
		if (Physics.Raycast(leftRay, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -45, 0) * transform.forward * rayDistance, Color.red);
			Debug.Log ("Left distance: " + hit.distance);
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -45, 0) * transform.forward * rayDistance, Color.green);
		}

	}

}