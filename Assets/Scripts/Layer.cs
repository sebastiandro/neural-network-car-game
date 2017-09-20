using UnityEngine;
using System.Collections;

public class Layer {
	private int id;
	private Neuron[] neurons = new Neuron[] {};

	public Layer() {
		id = 0;
	}

	public Layer(int index) {
		id = index;
	}

	public void populate(int nbNeurons, int nbInputs) {
		neurons = new Neuron[nbNeurons];
		for (int i = 0; i < nbNeurons; i++) {
			Neuron n = new Neuron ();
			n.populate (nbInputs);
			this.neurons [i] = n;
		}
	}

	public Neuron[] getNeurons() {
		return neurons;
	}

	public void setNeurons(Neuron[] neurons){
		this.neurons = neurons;
	}

	public Layer Clone() {
		Layer newLayer = new Layer (id);
		Neuron[] newNeurons = new Neuron[neurons.Length];

		for (int i = 0; i < neurons.Length; i++) {
			newNeurons [i] = neurons [i].Clone ();
		}

		newLayer.setNeurons (newNeurons);
		return newLayer;
	}
}

