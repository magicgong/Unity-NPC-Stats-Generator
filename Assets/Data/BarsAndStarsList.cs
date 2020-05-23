using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BarsAndStarsList : MonoBehaviour
{
	public int num_stars = 2;
	public int num_bars = 2;

	[ContextMenu("Go")]
	public void startBarsAndStars()
	{
		// Pre-alloc arrays.
		var data = new List<string>();

		barsAndStars(num_bars, num_stars, "", data);

		// Print data back out --- we can process it here. This is just to show that it's 'correct.'
		for (int i_str = 0; i_str < data.Count; i_str++)
		{
			Debug.Log(data[i_str]);
		}
	}

	public void barsAndStars(int bars, int stars, string prefix, List<string> out_data)
	{
		if (stars == 0)
		{
			string append = string.Join<char>(",", new string('0', (bars + 1)));
			out_data.Add(prefix + String.Join(",", append));
			return;
		}

		if (bars == 0)
		{
			out_data.Add($"{prefix}{stars}");
			return;
		}

		foreach (int i in Enumerable.Range(0, stars + 1))
		{
			var newprefix = $"{prefix}{i},";
			barsAndStars(bars - 1, stars - i, newprefix, out_data);
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

	public int nChooseK(int n, int k)
	{
		return Factorial(n) / (Factorial(k) * Factorial(n - k));
	}
}