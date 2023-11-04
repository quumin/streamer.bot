using System;
using System.IO;
using System.Collections.Generic;
using QminBotDLL;

/*Menu - New Game
 * 
 *  Check if game is installed or mentioned as Installed in the Library.
 *  LU: 31-oct-2023
 * 
 */

public class CPHInline
{
    public void Init()
    {
        //Set Static Class in QnamicLib to active instance of CPH
        QnamicLib.CPH = CPH;
    }//Init()

    public bool Execute()
    {
        //Declarations
        List<string>[] list_games;
        string[] str_uG, str_game;
        bool bool_inLib;
        int int_i;

        //Intializations
        str_uG = new string[]
        {
            "qminCurrentGame"
        };
        list_games = QnamicLib.LoadGameLibrary();
        str_game = new string[7];
        str_game[0] = args["gameName"].ToString();
        str_game[1] = args["gameId"].ToString();
        bool_inLib = false;
        int_i = 0;

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

}//CPHInline