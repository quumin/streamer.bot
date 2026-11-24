using System;
using System.Collections.Generic;
using System.Collections;
using QminBotDLL;

/*Game Checker
 * 
 *	Check if the game is in the library. If not, then trigger the menu.
 *  LU: 27-jun-2024
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
        //  Common Variables
        List<string>[] gamesLibrary;
        string qminCurrentGame, qminMenuType, qminChatState, qminSeriousMode;
        string[] usedActions, currentGame, tagList;
        bool[] usedActionsExist;
        //  Specific
        int listIterator;
        bool inLibrary, usrResponds, gameInstalled;

        //Initializations
        //  Common Variables
        gamesLibrary = QnamicLib.LoadGameLibrary();
        qminCurrentGame = "qminCurrentGame";
        qminMenuType = "qminMenuType";
        qminChatState = "qminChatState";
        qminSeriousMode = "qminSeriousMode";
        usedActions = new string[]
        {
            "Games - Trigger Menu"
        };
        tagList = new string[]
        {
            "Game",
            "ID",
            "Platform",
            "Installed",
            "Serious",
            "Horror",
            "Special",
            "No Back Seating",
            "Completed",
            "Q-Quotient",
            "Record",
            "YouTube Link"        	
        };
        currentGame = new string[11];
        CPH.TryGetArg("gameName", out string gameName);
            currentGame[0] = gameName;
        CPH.TryGetArg("gameId", out string gameId);
            currentGame[1] = gameId;
        inLibrary = false;
        usrResponds = false;
        listIterator = 0;
		CPH.SetGlobalVar(qminCurrentGame, currentGame);
        CPH.SetGlobalVar("qminCurrentGameTitle", gameName);
		
        //Iterate through the Library
        foreach (string str in gamesLibrary[1])
        {
            //	... if the game is in the library...
            CPH.LogVerbose($"『G A M E S』 Checkign for: \'{gameName} (#{gameId})\' ... #{listIterator}");
            if (str.Equals(gameId))
            {
                //... log it.
                CPH.LogInfo($"『G A M E S』 \'{gameName} (#{gameId})\' is already in the library at line #{listIterator}");
                //... flag it and get the stored info.
                inLibrary = true;
                for (int j = 0; j < currentGame.Length; j++)
                {
                    currentGame[j] = gamesLibrary[j][listIterator];
                    CPH.LogVerbose($"『G A M E S』 \'{currentGame[0]}\' | {tagList[j]} \t| {currentGame[j]} ");
                }//for()
                //... update the Global and leave the loop.
                CPH.SetGlobalVar(qminCurrentGame, currentGame);
                
                break;
            }//if()
            listIterator++;
        }//foreach()

        //If the game is not in the library...
        if (!inLibrary)
        {
            //... prompt the streamer for the missing info and add it to the library.
            CPH.RunAction(usedActions[0], true);
        }//else

        return true;
    }//Execute()
}//CPHInline