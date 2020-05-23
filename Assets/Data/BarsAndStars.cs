using System;
using System.Collections.Generic;
using System.Linq;

public class BarsAndStars
{

	public int[,] getBarsAndStars(int bars, int stars)
	{
		var data = new List<string>();

		// Do the thing
		barsAndStars(bars, stars, "", data);

		// Transform list of strings into multi-dimensional array of integers
		var arrayOfBarsAndStarsPermutations = new int[data.Count, bars + 1];
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

	void barsAndStars(int bars, int stars, string prefix, List<string> outputData)
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

}