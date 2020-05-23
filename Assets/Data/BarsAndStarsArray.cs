using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BarsAndStarsArray : MonoBehaviour
{
	public int bars = 2;
	public int stars = 2;

	public BarsAndStarsArray(int bars, int stars)
    {
		this.bars = bars;
		this.stars = stars;
    }

	[ContextMenu("Go")]
	public void startBarsAndStars()
	{
		var arrayOfBarsAndStarsPermutations = getBarsAndStars(bars, stars);
	}

	public int[,] getBarsAndStars(int bars, int stars)
    {
		// Pre-allocate list
		var top = Factorial(stars + bars - 1);
		var bot = Factorial(bars - 1) * Factorial(stars - bars);
		var numberOfPermutations = (top / bot);
		var data = new List<string>(numberOfPermutations);

		// Do the thing
		barsAndStars(bars, stars, "", data);

		// Transform list of strings into multi-dimensional array of integers
		var arrayOfBarsAndStarsPermutations = new int[numberOfPermutations, bars + 1];
		for (int iPermutation = 0; iPermutation < data.Count; ++iPermutation)
		{
			var arrayOfStars = data[iPermutation].Split(',');
			for (int iPartition = 0; iPartition < bars + 1; ++iPartition)
			{
				var numberOfStars = arrayOfStars[iPartition];
				arrayOfBarsAndStarsPermutations[iPermutation, iPartition] = int.Parse(numberOfStars);
			}
		}

		return arrayOfBarsAndStarsPermutations;
	}

	public void barsAndStars(int bars, int stars, string prefix, List<string> outputData)
	{
		if (stars == 0)
		{
			string append = string.Join<char>(",", new string('0', (bars + 1)));
			outputData.Add(prefix + String.Join(",", append));
			return;
		}

		if (bars == 0)
		{
			outputData.Add($"{prefix}{stars}");
			return;
		}

		foreach (int i in Enumerable.Range(0, stars + 1))
		{
			var newPrefix = $"{prefix}{i},";
			barsAndStars(bars - 1, stars - i, newPrefix, outputData);
		}
	}

	public int Factorial(int n)
	{
		int res = 1;
		while (n != 1)
		{
			res *= n;
			n -= 1;
		}

		return res;
	}

}