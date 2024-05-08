using System;
using System.IO;
using System.Collections.Generic;
using QminBotDLL;

/*State Checker
 * 
 *  Check what's going on after the state of the stream is triggered.
 *  LU: 09-may-2024
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
        string msgOut;
        bool isStream;

        //Initializations
        LoadAutoShout();
        LoadSoundActions();
        QnamicLib.MediaLoad("W:\\Streaming\\Media\\Sounds\\", 0.45f);
        isStream = CPH.ObsIsStreaming();
        actionList = CPH.GetGlobalVar<List<string>>("qminAutoShouts");
        timerList = new string[]
        {
            "AdultRemind",
            "Uptime Watcher",
            "Random Shouts"
        };
        msgOut = "/me ";

        //Check the state of OBS...
        if (isStream)
        {
            //... if the stream is started:
            //  Set scene to Starting Scene
            CPH.ObsSetScene("Starter");
            //  Turn off Emote Only, in case it's on
            CPH.TwitchEmoteOnly(false);

            //  Enable all Shoutouts.
            foreach (string s in actionList)
            {
                CPH.EnableAction(s);
            }//foreach

            //  Load the Riddles
            CPH.RunAction("Riddles - Load File");

            //  Start the Timers
            for (int i = 0; i < timerList.Length; i++)
                CPH.EnableTimer(timerList[i]);

            //  Feedback
            msgOut += "All systems go Q-Mander! DataFingerbang";
            CPH.CreateStreamMarker("Start of Stream");
            CPH.LogInfo("『MARKER』: START_OF_STREAM");
        }//if
        else
        {
            //... if the stream is stopped:
            //  Feedback
            msgOut += "It was a pleasure to serve you Q-mander, see you next time!";
            //  Stop the Timers
            for (int i = 0; i < timerList.Length; i++)
                CPH.DisableTimer(timerList[i]);
        }//else

        //Send message
        CPH.SendMessage(msgOut);
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