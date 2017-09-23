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

	Sensor frontSensor;
	Sensor rightSensor0;
	Sensor rightSensor1;
	Sensor rightSensor2;
	Sensor rightSensor3;
	Sensor rightSensor4;
	Sensor rightSensor5;
	Sensor leftSensor0;
	Sensor leftSensor1;
	Sensor leftSensor2;
	Sensor leftSensor3;
	Sensor leftSensor4;
	Sensor leftSensor5;

	double[] sensorOutput = new double[13];

	public void Awake() {
		car = gameObject;
		frontSensor = new Sensor (car, -90);
		rightSensor0= new Sensor (car, -75);
		rightSensor1= new Sensor (car, -60);
		rightSensor2 = new Sensor (car, -45);
		rightSensor3 = new Sensor (car, -30);
		rightSensor4 = new Sensor (car, -15);
		rightSensor5 = new Sensor (car, 0);
		leftSensor0 = new Sensor (car, 15);
		leftSensor1 = new Sensor (car, 30);
		leftSensor2 = new Sensor (car, 45);
		leftSensor3 = new Sensor (car, 60);
		leftSensor4 = new Sensor (car, 76);
		leftSensor5 = new Sensor (car, 90);
	}

	private void FixedUpdate() {
		sensorOutput[0] = frontSensor.getSignalStrength ();
		sensorOutput[1] = rightSensor0.getSignalStrength ();
		sensorOutput[2] = rightSensor1.getSignalStrength ();
		sensorOutput[3] = rightSensor2.getSignalStrength ();
		sensorOutput[4] = rightSensor3.getSignalStrength ();
		sensorOutput[5] = rightSensor4.getSignalStrength ();
		sensorOutput[6] = rightSensor5.getSignalStrength ();
		sensorOutput[7] = leftSensor0.getSignalStrength ();
		sensorOutput[8] = leftSensor1.getSignalStrength ();
		sensorOutput[9] = leftSensor2.getSignalStrength ();
		sensorOutput[10] = leftSensor3.getSignalStrength ();
		sensorOutput[11] = leftSensor4.getSignalStrength ();
		sensorOutput[12] = leftSensor5.getSignalStrength ();
	}

	public double[] getSensorOutput() {
		return sensorOutput;
	}
}
