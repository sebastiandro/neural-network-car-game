﻿using UnityEngine;
using System.Collections;
using System;

public class MathHelpers
{

	static System.Random rnd = new System.Random ();

	public static double activation(double x) {
		return (1 / (Math.Exp(x) + 1));
	}

	public static double randomClamped() {
		return rnd.NextDouble () * 2 - 1;
	}

	public static double randomNumber() {
		return rnd.NextDouble ();
	}

}

