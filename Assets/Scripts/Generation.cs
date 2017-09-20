using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generation {

	NeuralNetwork network;
	List<Genome> genomes = new List<Genome>();


	public Generation(NeuralNetwork network) {
		this.network = network;
	}

	// Add genome sorted
	public void addGenome(Genome genome) {
		int index = 0;
		for (int i = 0; i < genomes.Count; i++) {
			if (genome.getScore () > genomes [i].getScore ()) {
				break;
			}
			index++;
		}

		genomes.Insert (index, genome);
	}

	public void breed(Genome g1, Genome g2, int nbChildren){
		Genome g1Clone = g1.Clone();
	}

}

