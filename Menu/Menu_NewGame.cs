using System;
using System.IO;
using System.Collections.Generic;
using QminBotDLL;

/*Menu - New Game
 * 
 *  Check if game is installed or mentioned as Installed in the Library.
 *  LU: 23-jun-2024
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
        List<string>[] gamesList;
        string[] usedGlobals, currentGame;
        bool inLib;
        int i;

        //Intializations
        usedGlobals = new string[]
        {
            "qminCurrentGame"
        };
        gamesList = QnamicLib.LoadGameLibrary();
        currentGame = new string[7];
        currentGame[0] = args["gameName"].ToString();
        currentGame[1] = args["gameId"].ToString();
        inLib = false;
        i = 0;

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

}//CPHInline