using System;
using System.Collections.Generic;
using System.Collections;
using QminBotDLL;

/*Games - Update Visual
 * 
 *	Update the OBS tiles for a visual transition.
 *  LU: 26-jun-2024
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
        string qminCurrentGame;
        //  Specific
        string[] currentGame, obSubScenes, obSource;
        string gameName, gameBoxArt, oldGameName, oldGameBoxArt;
        string msgOut, msgPrefix;
        int waitTime;
        bool srsMode;

        //Initializations
        //  Common Variables
        qminCurrentGame = "qminCurrentGame";
        currentGame = CPH.GetGlobalVar<string[]>(qminCurrentGame);
        //  Specific
        gameName = currentGame[0];
        gameBoxArt = args["gameBoxArt"].ToString();
        oldGameName = args["oldGameName"].ToString();
        oldGameBoxArt = args["oldGameBoxArt"].ToString();
        obSubScenes = new string[]
        {
            "SS_MidScreen",
            "SS_KiyoPro_FancyCam"
        };
        obSource = new string[]
        {
            "Old GameBox Art",
            "VM_Visualizer_Serious",
            "VM_Visualizer_Normal",
            "soPlayer",
            "New GameBox Art"
        };
        msgOut = "";
        waitTime = 5000;
        srsMode = string.Compare(currentGame[4], "TRUE") == 0;

        //If the game is serious...
        if (srsMode)
        {
            //... update message to be "serious game."
            msgPrefix = "/me 『SERIOUS UPDATE』 ";
        }//if()
        else
        {
            //... update message to be "serious game."
            msgPrefix = "/me 『SYSTEM CHECK』 ";
        }//else

        //Show the old game Box Art.
        CPH.ObsSetBrowserSource(obSubScenes[0], obSource[0], oldGameBoxArt);
        CPH.ObsShowSource(obSubScenes[0], obSource[0]);

        //Disable Game Specific Rewards.
        //  If Serious...
        if (srsMode)
        {
            //... Show Serious Visualizer.
            CPH.ObsShowSource(obSubScenes[1], obSource[1]);
            CPH.ObsHideSource(obSubScenes[1], obSource[2]);
            CPH.ObsHideSource(obSubScenes[0], obSource[3]);
            msgOut += "『S E R I O U S』 ";
        }//else if()
        else
        {
            //... Show Normal Visualizer.
            CPH.ObsHideSource(obSubScenes[1], obSource[1]);
            CPH.ObsShowSource(obSubScenes[1], obSource[2]);
            CPH.ObsShowSource(obSubScenes[0], obSource[3]);
            msgOut += "『G A M E S』 ";
        }//else

        //Log stuff.
        CPH.LogInfo($"『G A M E S』: Game update from \'{oldGameName}\' to \'{gameName}\'.");
        msgOut += $"{oldGameName} -> {gameName}!";

        //Show the game box art change.
        CPH.ObsSetBrowserSource(obSubScenes[0], obSource[4], gameBoxArt);
        CPH.Wait(waitTime);
        CPH.ObsHideSource(obSubScenes[0], obSource[0]);
        CPH.ObsShowSource(obSubScenes[0], obSource[4]);
        CPH.Wait(waitTime);
        CPH.ObsHideSource(obSubScenes[0], obSource[4]);

        //If OBS is streaming...
        if (CPH.ObsIsStreaming())
        {
            //... create a marker.
            CPH.CreateStreamMarker($"CHANGE - {gameName}");
        }//if

        CPH.SendMessage($"{msgPrefix}{msgOut}", true);

        return true;
    }//Execute()
}//CPHInline