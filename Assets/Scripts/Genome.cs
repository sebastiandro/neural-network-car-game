using System;

public class Genome
{

	private int score = 0;
	private NeuralNetwork network;

	public Genome ()
	{
	}

	public Genome (NeuralNetwork network, int score)
	{
		this.network = network;
		this.score = score;
	}
}

