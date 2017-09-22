using System;
using System.Collections.Generic;

public class NeuroEvolution
{

	public static int population = 10;
	public static int inputs = 1;
	public static int[] hiddenLayers = new int[]{1};
	public static int outputs = 1;
	public bool lowHistoric = false;
	public int historic = 0;
	public static double mutationRange = 0.5;
	public static double mutationRate = 0.3;
	public static double elitism = 0.2;
	public static double randomBehaviour = 0.2;
	public static int nbChildren = 1;


	Generations generations = new Generations();

	public NeuroEvolution (int inputs, int[] hiddenLayers, int outputs)
	{
		NeuroEvolution.inputs = inputs;
		NeuroEvolution.hiddenLayers = hiddenLayers;
		NeuroEvolution.outputs = outputs;
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
				List<Genome> genomes = generations.getGenerations () [generations.getGenerations ().Count - 1].getGenomes ();
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


