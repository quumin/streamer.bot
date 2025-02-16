using System;
using System.Collections.Generic;
using System.Collections;
using QminBotDLL;

/*Game Checker
 * 
 *	Post the menu, wait for response before coming back to Games Queue.
 *  LU: 25-jun-2024
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
        string qminMenuType, qminChatState;
        string[] usedActions;
        bool[] usedActionsExist;
        //  Specific
        bool usrResponds;

        //Initializations
        //  Common Variables
        qminMenuType = "qminMenuType";
        qminChatState = "qminChatState";
        usedActions = new string[]
        {
            "Menu - Post Prompt"
        };
        usedActionsExist = QnamicLib.CheckCPHActions(usedActions);
        CPH.SetGlobalVar(qminMenuType, "gameType");
        CPH.SetGlobalVar(qminChatState, "menu_on");
        usrResponds = true;
        CPH.RunAction(usedActions[0], true);

        //... wait until a response from the user.
        while (!usrResponds)
        {
            usrResponds = !CPH.GetGlobalVar<string>(qminChatState).Equals("menu_on");
        }//while()

        return true;
    }//Execute()
}//CPHInline