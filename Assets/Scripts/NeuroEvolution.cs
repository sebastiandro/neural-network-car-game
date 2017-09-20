using System;
using System.Collections.Generic;

public class NeuroEvolution
{

	public static int population = 50;
	public static int inputNeurons = 1;
	public static int[] hiddenLayers = new int[]{1};
	public static int outputNeurons = 1;
	public bool lowHistoric = false;
	public int historic = 0;


	Generations generations = new Generations();

	public NeuroEvolution ()
	{
		
	}


	public Generation nextGeneration() {

		Generation nextGeneration;

		if (generations.getGenerations ().Count == 0) {
			nextGeneration = generations.firstGeneration ();
		} else {
			nextGeneration = generations.nextGeneration ();
		}

		if (lowHistoric) {
			if (generations.getGenerations ().Count >= 2) {
				List<Genome> genomes = generations.getGenerations () [generations.getGenerations () - 1].getGenomes ();
				for (int i = 0; i < genomes.Count; i++) {
					genomes [i].deleteNeuralNetwork ();
				}
			}
		}

		if (historic != -1) {
			if (generations.getGenerations ().Count > historic + 1) {
				generations.getGenerations ().RemoveRange (0, generations.getGenerations ().Count - (historic + 1));
			}
		}

		return nextGeneration;
	}

	public void networkScore(NeuralNetwork neuralNetwork, int score) {
		generations.addGenome (new Genome (neuralNetwork, score));
	}
}


