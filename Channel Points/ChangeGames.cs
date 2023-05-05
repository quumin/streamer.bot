using System;
using System.IO;
using System.Collections.Generic;

/*Change Games
 * 
 *  Change the game from the channel points.
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        List<string> list_games;
        string str_choice, str_usr, str_redeem, str_reward, str_msg;

        //Initializations
        list_games = gameLoad();
        str_choice = args["rawInput"].ToString();
        str_usr = args["userName"].ToString();
        str_redeem = str_reward = "";
        str_msg = "/me ";

        //If streaming...
        if (CPH.ObsIsStreaming())
        {
            //... get reward info.
            str_redeem = args["redemptionId"].ToString();
            str_reward = args["rewardId"].ToString();
        }//if

        //If that choice is in the Installed Games List...
        if (list_games.Contains(str_choice))
        {
            CPH.LogInfo("『G A M E S』 \'" + str_choice + "\' found!");
            str_msg += "thinkingJojo so uh... Q, you gonna change to " + str_choice + " like @" + str_usr + " asked, or...? Ghost";
            CPH.SetChannelGame(str_choice);
            CPH.EnableTimer("GameChanger");
        }//if
        else
        {
            CPH.LogInfo("『G A M E S』 \'" + str_choice + "\' not found!");
            str_msg += "WTFF Sorry @" + str_usr + " , \"" + str_choice + "\" was not found! dataHuh Don't worry, your points have been refunded! Saved Please check the list below PointDown and try again.";
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
    List<string> gameLoad()
    {
        //Declarations
        List<string> list_games;

        //Initializations
        list_games = new List<string>();

        try
        {
            //Try to find the file and read from it...
            using (var reader = new StreamReader(@".\\external_files\\GamesList.csv"))
            {
                //Populate the List
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    list_games.Add(line);
                }//while

                CPH.LogInfo("『G A M E S』 Loaded Successfully.");
            }//using
        }//try
        catch (Exception ex) when (ex is FileNotFoundException || ex is DirectoryNotFoundException)
        {
            //Catch when the directory and/or file is incorrect.
            CPH.LogWarn("『G A M E S』 Games File failed to load!");
            CPH.SendMessage("/me dataHuh The Games file could not be found, sir.");
        }//catch
        return list_games;
    }//gameLoad()
}//CPHInline