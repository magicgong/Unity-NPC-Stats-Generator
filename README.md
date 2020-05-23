# Unity-NPC-Stats-Generator
A generic and game-agnostic NPC stats generator in Unity (made with version 2019.3.21f1).

This generates all permutations of NPC stats based on settings you provide in the inspector and then writes the results to a JSON file. 

## Usage

1. Optional: You may choose an alternative output file path and file name for the JSON file. The default is `Assets/Output/NPC_Stats`.
2. Optional: You may choose an alternative NPC prefix. The JSON data is indexed by `<prefix>#`.
3. Set the total number of stat points you want each NPC to receive.
4. Under "Stats" set the size to the number of stats you would like for each NPC.
5. By changing the size, the inspector will update and allow you to give each stat a name (str, dex, int, ...). This can be anything you want.
6. Once you have updated the stat names. click **Save**. 
7. The inspector will update and allow you to set min and max constraints for each stat.
8. Once you are ready to generate, click on the context menu and select **Go**.
9. Keep an eye on the dev console to know when the generation has completed. 
10. Once complete, head over to the output file location and you will find the generated JSON file.

That's it!

One thing to note is that if you don't set any constraints and you choose a high number of stat points and or a large size (steps 3 & 4), then it might take a while to generate as there may be a large number of permutations. The script can fail if you generate a number beyond memory limits. Thus, it is recommended to put in at least one constraint. For example, you might set HP to a min of 1 (since an NPC with 0 HP may not very useful - or healthy).

Also, the name NPC Stats Generator is somewhat of a misnomer. This generator can be used any time that you want to distribute `n` indistinguishable elements to `r` partitions (groups). It uses stars and bars combinatorics.

This generator is meant to be run offline as external computation and has not been optimized. However, with little modification you could have this working in your game at run-time for a variety of uses. 

Use it however you like.

Enjoy!
