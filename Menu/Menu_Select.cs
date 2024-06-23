using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        List<string>[] gamesList;
        string[] usedGlobals, currentGame;
        bool inLib;
        int i;

        //Intializations
        // Load Game Library
        gamesList = LoadGameLibrary();
        // Global List
        usedGlobals = new string[]
        {
            "qminCurrentGame"
        };
        // Specific
        currentGame = new string[7];
        inLib = false;
        i = 0;
        // SB Args
        currentGame[0] = args["gameName"].ToString();
        currentGame[1] = args["gameId"].ToString();


        foreach (string str in gamesList[1])
        {
            if (str.Equals(currentGame[1]))
            {
                inLib = true;
                for (int j = 2; j < currentGame.Length; j++)
                {
                    currentGame[j] = gamesList[j][i];
                    CPH.LogVerbose($"『G A M E S』 \'{currentGame[0]}\' | {currentGame[j]}");
                }//for()
            }//if()
            i++;
        }//foreach()

        if (inLib)
        {
            //Run "Check Game Type"
            CPH.LogInfo($"『G A M E S』 \'{currentGame[0]}\' is already in the library.");
        }
        else
        {
            CPH.LogInfo($"『G A M E S』 \'{currentGame[0]}\' is not yet in the library.");
            CPH.RunAction("Menu - Post Prompt");
        }
        CPH.SetGlobalVar(usedGlobals[0], currentGame);
        return true;
    }//Execute()

    public List<String>[] LoadGameLibrary(int int_col = 7, char chr_delim = ';')
    {
        //Declarations
        List<string>[] gamesList;
        string filePath, openFile;

        //Initializations
        gamesList = new List<string>[int_col];
        for (int i = 0; i < gamesList.Length; i++)
        {
            /*Games Library
			 *	0 - Game
			 *	1 - ID
			 *	2 - Platform
			 *	3 - Installed
			 *	4 - Serious
			 *	5 - Horror
			 *	6 - Special
			 */
            gamesList[i] = new List<string>();
        }//for
        filePath = @".\\external_files\\";
        openFile = "GamesList.csv";

        try
        {
            // ... to Read CSV.
            string[] lines = File.ReadAllLines($"{filePath}{openFile}");
            //For each line after the first...
            foreach (string line in lines.Skip(1))
            {
                //... split by delimiter.
                string[] values = line.Split(chr_delim);
                //... add to list.
                for (int i = 0; i < gamesList.Length; i++)
                {
                    gamesList[i].Add(values[i]);
                }//for
            }//foreach
            CPH.LogInfo("『L I B』 Loaded Successfully.");
        }//try
        catch (Exception ex) when (ex is FileNotFoundException || ex is DirectoryNotFoundException)
        {
            //... when the directory and/or file is incorrect.
            CPH.LogWarn("『L I B』 Games File failed to load!");
            CPH.SendMessage("/me dataHuh The Games file could not be found, sir.");
        }//catch

        return gamesList;
    }//LoadGameLibrary
}//CPHInline