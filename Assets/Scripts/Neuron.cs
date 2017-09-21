using UnityEngine;
using System.Collections;

public class Neuron {
	private double value = 0;
	private double[] weights;

	public void populate(int nb){
		weights = new double[nb];
		for (int i = 0; i < nb; i++) {
			weights [i] = MathHelpers.randomClamped ();
		}
	}

	public double[] getWeights() {
		return weights;
	}

	public void setWeights(double[] weights) {
		this.weights = weights;
	}

	public void setWeightAtIndex(double weight, int index) {
		weights [index] = weight;
	}

	public void setValue(double value) {
		this.value = value;
	}

	public double getValue() {
		return value;
	}

	public Neuron Clone() {
		Neuron newNeuron = new Neuron ();
		double newValue = value;
		double[] newWeights = new double[weights.Length];
		for (int i = 0; i < weights.Length; i++) {
			newWeights [i] = weights [i];
		}

		newNeuron.setValue (newValue);
		newNeuron.setWeights (newWeights);


		return newNeuron;
	}

}
