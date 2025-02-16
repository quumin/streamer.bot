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
        string[] usedActions, currentGame;
        bool[] usedActionsExist;
        //  Specific
        string gameName, gameId, rawInput;
        int gameColumnIterator;
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
        usedActionsExist = QnamicLib.CheckCPHActions(usedActions);
        //  Specific
        /*Games Library
        *	0   Game
        *	1   ID
        *	2   Platform
        *	3   Installed
        *	4   Serious
        *	5   Horror
        *	6   Special
        *	7   No Back Seating
        *	8   Completed
        *	9   Q-Quotient
        *	10  Twitch VOD Link
        *	11  YouTube Link
        */
        currentGame = new string[11];
        gameName = currentGame[0] = args["gameName"].ToString();
        gameId = currentGame[1] = args["gameId"].ToString();
        inLibrary = false;
        usrResponds = false;
        gameColumnIterator = 0;
        CPH.SetGlobalVar(qminCurrentGame, currentGame);

        //Iterate through the Library
        foreach (string str in gamesLibrary[1])
        {
            //	... if the game is in the library...
            if (str.Equals(gameId))
            {
                //... log it.
                CPH.LogInfo($"『G A M E S』 \'{gameName} (#{gameId})\' is already in the library.");
                //... flag it and get the stored info.
                inLibrary = true;
                for (int j = 2; j < currentGame.Length; j++)
                {
                    currentGame[j] = gamesLibrary[j][gameColumnIterator];
                    CPH.LogVerbose($"『G A M E S』 \'{currentGame[0]}\' | {currentGame[j]}");
                }//for()
                //... update the Global and leave the loop.
                CPH.SetGlobalVar(qminCurrentGame, currentGame);
                break;
            }//if()
            gameColumnIterator++;
        }//foreach()

        //If the game is not in the library...
        if (!inLibrary && usedActionsExist[0])
        {
            //... prompt the streamer for the missing info and add it to the library.
            CPH.RunAction(usedActions[0], true);
        }//else


        return true;
    }//Execute()
}//CPHInline