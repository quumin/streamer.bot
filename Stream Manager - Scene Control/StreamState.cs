using System;
using System.IO;
using System.Collections.Generic;
using QminBotDLL;

/*State Checker
 * 
 *  Check what's going on after the state of the stream is triggered.
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
        List<string> actionList;
        string[] timerList;
        string msgOut, logOut, trigTxt;
        bool isStream, isRecord;

        //Initializations
        LoadAutoShout();
        LoadSoundActions();
        QnamicLib.MediaLoad("W:\\Streaming\\Media\\Sounds\\", 0.45f);
        isStream = CPH.ObsIsStreaming();
        isRecord = CPH.ObsIsRecording();
        trigTxt = args["triggerName"].ToString();
        actionList = CPH.GetGlobalVar<List<string>>("qminAutoShouts");
        timerList = new string[]
        {
            "AdultRemind",
            "Uptime Watcher",
            "Random Shouts"
        };
        msgOut = "/me ";
        logOut = "『STREAM STATE』: ";

        //Check Case of Trigger
        switch (trigTxt)
        {
            // Stream Start
            case "Streaming Started":
                //	Send first feedback to chat
                CPH.SendMessage("/me Powering on all Systems Q-Mander... Warp5 ");
                //	Set scene to Starting Scene
                CPH.ObsSetScene("Starter");
                //  Turn off Emote Only, in case it's on
                CPH.TwitchEmoteOnly(false);
                //	Enable all Shoutout Actions
                foreach (string s in actionList)
                {
                    CPH.EnableAction(s);
                }//foreach

                //	Load the Riddles
                CPH.RunAction("Riddles - Load File");

                //	Start the Timers
                for (int i = 0; i < timerList.Length; i++)
                {
                    CPH.EnableTimer(timerList[i]);
                }//for

                //	Feedback
                msgOut += "All systems go Q-Mander! DataFingerbang";
                logOut += "START_OF_STREAM";
                break;
            // Stream Stop
            case "Streaming Stopped":
                //  Feedback
                msgOut += "It was a pleasure to serve you Q-mander, see you next time! VulkanSalute";
                //  Stop the Timers
                for (int i = 0; i < timerList.Length; i++)
                {
                    CPH.DisableTimer(timerList[i]);
                }//for
                break;
            // Record Start
            case "Recording Started":
                msgOut += "VOD Security Systems enabled Q-Mander! DataFingerbang";
                logOut += "START_OF_RECORD";
                break;
            // Record Stop
            case "Recording Stopped":
                msgOut += "SirGasp VOD Security Systems disabled Q-Mander!";
                logOut += "RECORD_STOP";
                break;
            // Error
            default:
                CPH.LogError("Something went wrong with \'State Checker\'!");
                break;
        }//switch

        //If I'm live...
        if (CPH.ObsIsStreaming())
        {
            //... create a marker.
            CPH.CreateStreamMarker($"STATE - \'{trigTxt}\'");
        }//if


        //Send Feedback Message
        CPH.SendMessage(msgOut);
        //Update Logfile
        CPH.LogInfo(logOut);
        return true;
    }//Execute()

    void LoadAutoShout()
    {
        /*Generate Globals - Shout Outs
		 * 
		 *  Generate the global variable for all shoutouts.
		 * 
		 */
        //Declarations
        List<string> list_actions;

        //Initializations
        list_actions = new List<string>
        {
            "aypoci",
            "bobotucci",
            "cactuarmike",
            "claymorefenrir",
            "earthtothien",
            "galaxy19",
            "gryze_wolf",
            "grzajabarkus",
            "harukunsama",
            "kukuburra",
            "lolloer__1",
            "mechamayfly",
            "muhgoop",
            "ogweirdbeard",
            "owsgt",
            "sharkiemarki3",
            "thepenguinbean",
            "toothpicksforrobots",
            "whymusticryy"
        };

        //Set Global
        CPH.SetGlobalVar("qminAutoShouts", list_actions, true);
    }//gg_AutoShout()

    void LoadSoundActions()
    {
        /*Generate Globals - Sounds
		* 
		*  Generate the global variable for all sound interaction with viewers Actions.
		* 
		*/

        //Declarations
        List<string> list_actions;

        //Initializations
        list_actions = new List<string>
        {
            "Best of Both Worlds",
            "Bing Chilling",
            "KEKW",
            "Kira",
            "Torture Dance",
            "Unlurk",
            "Cozy Time",
            "EZ Clap",
            "Fuck you Data",
            "Oh Shit!",
            "OMT",
            "Thanks Data",
            "YareYare"
        };

        //Set Global
        CPH.SetGlobalVar("qminSoundInteractions", list_actions, true);
    }//gg_Sounds()
}//CPHInline