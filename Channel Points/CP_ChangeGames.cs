using System;
using System.IO;
using System.Collections.Generic;
using QminBotDLL;

/*Channel Points - Change Games
 * 
 *  Change the game from the channel points.
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
        string[] usedTimers;
        string rawInput, usrName, redemptionId, rewardId, msgOut;

        //Initializations
        //  QnamicLib
        gamesList = QnamicLib.LoadGameLibrary();
        //  Timer List
        usedTimers = new string[]
        {
            "GameChanger"
        };
        //  SB Args
        rawInput = args["rawInput"].ToString();
        usrName = args["userName"].ToString();
        //      If streaming...
        if (CPH.ObsIsStreaming())
        {
            //... get reward info.
            redemptionId = args["redemptionId"].ToString();
            rewardId = args["rewardId"].ToString();
        }//if
        //  Specifc
        redemptionId = "";
        rewardId = "";
        msgOut = "/me ";

        //If that choice is in the Installed Games List...
        if (gamesList[0].Contains(rawInput))
        {
            CPH.LogInfo($"『G A M E S』 \'{rawInput}\' found!");
            msgOut += $"thinkingJojo so uh... Q, you gonna change to {rawInput} like @{usrName} asked, or...? Ghost";
            CPH.SetChannelGame(rawInput);
            CPH.EnableTimer(usedTimers[0]);
        }//if
        else
        {
            CPH.LogInfo($"『G A M E S』 \'{rawInput}\' not found!");
            msgOut += $"WTFF Sorry @{usrName}, \"{rawInput}\" was not found! dataHuh Don't worry, your points have been refunded! Saved Please check the list below PointDown and try again.";
            //... if streaming...
            if (CPH.ObsIsStreaming())
            {
                //... cancel the redemption.
                CPH.TwitchRedemptionCancel(rewardId, redemptionId);
            }//if
        }//else

        //Send message.
        CPH.SendMessage(msgOut);

        return true;
    }//Execute()

}//CPHInline