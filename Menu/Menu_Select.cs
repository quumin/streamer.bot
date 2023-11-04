using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        List<string>[] list_games;
        string[] str_uG, str_game;
        bool bool_inLib;
        int int_i;

        //Intializations
        // Load Game Library
        list_games = LoadGameLibrary();
        // Global List
        str_uG = new string[]
        {
            "qminCurrentGame"
        };
        // Specific
        str_game = new string[7];
        bool_inLib = false;
        int_i = 0;
        // SB Args
        str_game[0] = args["gameName"].ToString();
        str_game[1] = args["gameId"].ToString();


        foreach (string str in list_games[1])
        {
            if (str.Equals(str_game[1]))
            {
                bool_inLib = true;
                for (int j = 2; j < str_game.Length; j++)
                {
                    str_game[j] = list_games[j][int_i];
                    CPH.LogVerbose($"『G A M E S』 \'{str_game[0]}\' | {str_game[j]}");
                }//for()
            }//if()
            int_i++;
        }//foreach()

        if (bool_inLib)
        {
            //Run "Check Game Type"
            CPH.LogInfo($"『G A M E S』 \'{str_game[0]}\' is already in the library.");
        }
        else
        {
            CPH.LogInfo($"『G A M E S』 \'{str_game[0]}\' is not yet in the library.");
            CPH.RunAction("Menu - Post Prompt");
        }
        CPH.SetGlobalVar(str_uG[0], str_game);
        return true;
    }//Execute()

    public List<String>[] LoadGameLibrary(int int_col = 7, char chr_delim = ';')
    {
        //Declarations
        List<string>[] list_games;
        string str_path, str_file;

        //Initializations
        list_games = new List<string>[int_col];
        for (int i = 0; i < list_games.Length; i++)
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
            list_games[i] = new List<string>();
        }//for
        str_path = @".\\external_files\\";
        str_file = "GamesList.csv";

        try
        {
            // ... to Read CSV.
            string[] lines = File.ReadAllLines($"{str_path}{str_file}");
            //For each line after the first...
            foreach (string line in lines.Skip(1))
            {
                //... split by delimiter.
                string[] values = line.Split(chr_delim);
                //... add to list.
                for (int i = 0; i < list_games.Length; i++)
                {
                    list_games[i].Add(values[i]);
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

        return list_games;
    }//LoadGameLibrary
}//CPHInline