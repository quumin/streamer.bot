using System;
using System.IO;
using System.Collections.Generic;
using QminBotDLL;

/*Channel Points - Change Games
 * 
 *  Change the game from the channel points.
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
        string[] str_uT;
        string str_ri, str_usr, str_redeem, str_reward, str_msg;

        //Initializations
        //  QnamicLib
        list_games = QnamicLib.LoadGameLibrary();
        //  Timer List
        str_uT = new string[]
        {
            "GameChanger"
        };
        //  SB Args
        str_ri = args["rawInput"].ToString();
        str_usr = args["userName"].ToString();
        //      If streaming...
        if (CPH.ObsIsStreaming())
        {
            //... get reward info.
            str_redeem = args["redemptionId"].ToString();
            str_reward = args["rewardId"].ToString();
        }//if
        //  Specifc
        str_redeem = "";
        str_reward = "";
        str_msg = "/me ";

        //If that choice is in the Installed Games List...
        if (list_games[0].Contains(str_ri))
        {
            CPH.LogInfo($"『G A M E S』 \'{str_ri}\' found!");
            str_msg += $"thinkingJojo so uh... Q, you gonna change to {str_ri} like @{str_usr} asked, or...? Ghost";
            CPH.SetChannelGame(str_ri);
            CPH.EnableTimer(str_uT[0]);
        }//if
        else
        {
            CPH.LogInfo($"『G A M E S』 \'{str_ri}\' not found!");
            str_msg += $"WTFF Sorry @{str_usr}, \"{str_ri}\" was not found! dataHuh Don't worry, your points have been refunded! Saved Please check the list below PointDown and try again.";
            //... if streaming...
            if (CPH.ObsIsStreaming())
            {
                //... cancel the redemption.
                CPH.TwitchRedemptionCancel(str_reward, str_redeem);
            }//if
        }//else

        //Send message.
        CPH.SendMessage(str_msg);

        return true;
    }//Execute()

}//CPHInline