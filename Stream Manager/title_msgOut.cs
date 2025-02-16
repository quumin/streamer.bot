using System;
using System.IO;
using System.Collections.Generic;
using System.Timers;
using QminBotDLL;

/*Title - Message Out
 * 
 *  Sends a message to identify the update in status.
 *  LU: 02-jul-24
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
        string qminCurrentGame, qminSeriousMode;
        string[] currentGame;
        bool[] usedActionsExist;

        //	Specific
        bool gameUpdate, statusUpdate;
        string[] usedGlobals;
        string streamTitle, msgPrefix, msgOut;
        bool seriousMode;
        int waitTime;

        //Initializations
        //  Common Variables
        qminCurrentGame = "qminCurrentGame";
        qminSeriousMode = "qminSeriousMode";
        //  Specific
        currentGame = CPH.GetGlobalVar<string[]>(qminCurrentGame);
        streamTitle = args["status"].ToString();
        //	Fix Later
        gameUpdate = Convert.ToBoolean(args["gameUpdate"]);
        statusUpdate = Convert.ToBoolean(args["statusUpdate"]);
        seriousMode = CPH.GetGlobalVar<bool>(qminSeriousMode);
        CPH.LogInfo($"『STATUS』 Serious Mode: {seriousMode}"); ;
        msgOut = "";


        //If the game is serious...
        if (seriousMode)
        {
            //... update message to be "serious."
            msgPrefix = "/me 『SERIOUS』 ";
        }//if()
        else
        {
            //... update message to be " normal."
            msgPrefix = "/me 『SYSTEMS CHECK』 ";
        }//else

        //If the title updates...
        if (statusUpdate)
        {
            //... add status to the message.
            msgOut += $"Title changed to \'{streamTitle}\'.";
            //... log stuff.
            CPH.LogInfo($"『STATUS』: Title updated to \'{streamTitle}\'.");
            //Feedback in Chat
            CPH.SendMessage($"{msgPrefix}{msgOut}");
        }//if()

        return true;
    }//Execute()

}//public class CPHInline