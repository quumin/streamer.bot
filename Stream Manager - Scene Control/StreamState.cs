using System;
using System.IO;
using System.Collections.Generic;
using QminBotDLL;

/*Stream State
 * 
 *  Start the stream.
 *  LU: 4-nov-2023
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
        List<string> list_actions;
        string[] str_timers;
        string str_state, str_msg;

        //Initializations
        LoadAutoShout();
        LoadSoundActions();
        QnamicLib.MediaLoad("W:\\Streaming\\Media\\Sounds\\", 0.15f);

        list_actions = CPH.GetGlobalVar<List<string>>("qminAutoShouts");
        str_timers = new string[]
        {
            "AdultRemind",
            "Uptime Watcher",
            "Random Shouts"
        };
        str_state = args["obsEvent.outputState"].ToString();
        str_msg = "/me ";

        //Check the state of OBS
        switch (str_state)
        {
            //	Output Starting
            case "OBS_WEBSOCKET_OUTPUT_STARTING":
                CPH.ObsSetScene("Starter");
                CPH.TwitchEmoteOnly(false);

                //Enable all Shoutouts
                //... Disable Sound Actions.
                foreach (string s in list_actions)
                {
                    CPH.EnableAction(s);
                }//foreach

                //Load the Riddles
                CPH.RunAction("Riddles - Load File");

                //Start Timers
                for (int i = 0; i < str_timers.Length; i++)
                    CPH.EnableTimer(str_timers[i]);

                //Feedback
                str_msg += "All systems go Q-Mander! DataFingerbang";
                CPH.LogInfo("『MARKER』: START_OF_STREAM");
                break;
            //	Output Stopping
            case "OBS_WEBSOCKET_OUTPUT_STOPPING":
                str_msg += "It was a pleasure to serve you Q-mander, see you next time!";
                //Stop Timers
                for (int i = 0; i < str_timers.Length; i++)
                    CPH.DisableTimer(str_timers[i]);
                break;
            //	Other
            default:
                return true;
        }//switch

        //Send message
        CPH.SendMessage(str_msg);

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