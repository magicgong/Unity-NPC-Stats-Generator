using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class NPCStatsGenerator: MonoBehaviour
{
	public string filePath = "Assets/Output/";
	public string fileName = "NPC_Stats";

	public string nPCPrefix = "NPC";
	public int statPoints = 2;
	public List<string> stats = new List<string>(2) {"HP", "Damage"};  //contain the names of the stats enum

	public List<StatConstraint> statMinConstraints = new List<StatConstraint>();
	public List<StatConstraint> statMaxConstraints = new List<StatConstraint>();

	public int GetStatPoints()
    {
		return statPoints;
    }

	[ContextMenu("Go")]
	public void startNPCStatsGenerator()
	{
		// Validations for trickers
		validateUserInput();

		// Get the user-defined stats
		string[] arrayOfStats = Enum.GetNames(typeof(StatsEnum));

		// Instantiate the BarsAndStars class
		Debug.Log("NPC stats generation has started. Depending on your settings, this could take a while...");
		BarsAndStars barsAndStars = new BarsAndStars();

		// Get all the bars and stars permutations
		var arrayOfBarsAndStarsPermutations = barsAndStars.getBarsAndStars(arrayOfStats.Length - 1, statPoints);
		Debug.Log("You generated a total of " + arrayOfBarsAndStarsPermutations.GetLength(0) + " permutations.");

		// Filter out invalid permutations (by stat constraints)
		var filteredListOfBarsAndStarsPermutations = filterArrayOfBarsAndStarsPermutationsByStatConstraints(arrayOfBarsAndStarsPermutations);
		Debug.Log("Your min and max constraints reduced the permutations to a total of " + filteredListOfBarsAndStarsPermutations.Count + " valid NPCs.");

		// Transform the filters list into a "JSON" string
		var JSONString = transformListIntoJSONStringOfNPCStats(filteredListOfBarsAndStarsPermutations, arrayOfStats);

		// Write the JSONString to a JSON file. The JSON file will contain entries for all the final NPC stats!
		WriteStringToJSONFile.WriteToJSON(filePath, fileName, JSONString);
		Debug.Log("Done writing to disk. Check the output file.");
	}

	List<int[]> filterArrayOfBarsAndStarsPermutationsByStatConstraints(int[,] arrayOfBarsAndStarsPermutations)
    {
		List<int[]> filteredList = new List<int[]>();

		for (int i = 0; i < arrayOfBarsAndStarsPermutations.GetLength(0); ++i)
		{
			bool isValid = true;
			for (int j = 0; j < arrayOfBarsAndStarsPermutations.GetLength(1); ++j)
			{
				int num = arrayOfBarsAndStarsPermutations[i, j];

				// Check if num is within the corresponding stat constraints; if it is not then it is considered invalid.
				if (num < statMinConstraints[j].Value | num > statMaxConstraints[j].Value)
				{
					isValid = false;
				}
			}

			if (isValid)
			{
				// Add valid permuation (row) to the filtered list.
				filteredList.Add(arrayOfBarsAndStarsPermutations.GetRow(i));
			}
		}

		return filteredList;
	}

	string transformListIntoJSONStringOfNPCStats(List<int[]> filteredList, string[] arrayOfStats)
	{
		string JSONstring = "{";

		for (int i = 0; i < filteredList.Count; ++i)
		{
			JSONstring += "\"" + nPCPrefix + (i + 1) + "\": {";
			for (int j = 0; j < arrayOfStats.Length; ++j)
			{
				JSONstring += "\"" + arrayOfStats[j] + "\":" + filteredList[i][j];
				if (j != arrayOfStats.Length - 1)
				{
					JSONstring += ", ";
				}
			}
			JSONstring += "}";
			if (i != filteredList.Count - 1)
			{
				JSONstring += ", ";
			}
			else
			{
				JSONstring += " ";
			}
		}

		JSONstring += "}";

		return JSONstring;
	}

	void validateUserInput()
    {
		if(this.filePath == "")

		{
			Debug.Log("The file path cannot be blank and must be a valid path.");
			return;
		}

		if (this.fileName == "")
		{
			Debug.Log("The file name must be at least 1 character and must be a valid file name for your operating system.");
			return;
		}

		if (Regex.IsMatch(this.nPCPrefix, "^[a-zA-Z0-9]*$") != true)
		{
			Debug.Log("The NPC Prefix must be alphanumeric.");
			return;
		}

		if (this.stats.Count < 2)
		{
			Debug.Log("The number of stats must not be less than 2.");
			return;
		}

		foreach (string statName in this.stats)
		{
			if (Regex.IsMatch(statName, "^[a-zA-Z0-9]+$") != true)
			{
				Debug.Log("Stat names must be at least 1 character and must be alphanumeric.");
				return;
			}
		}
	}

}
