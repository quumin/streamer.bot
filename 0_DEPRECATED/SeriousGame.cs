using System;
using System.IO;
using System.Collections.Generic;

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        List<string> list_actions;
        string[] str_rewardGroups;
        string str_art, str_scene;

        //Initializations
        list_actions = CPH.GetGlobalVar<List<string>>("soundInteractActions");
        str_rewardGroups = new string[] {"Standard",
            "Standard - Sounds",
            "GS - DD2",
            "GS - PoE" };
        str_art = args["oldGameBoxArt"].ToString();
        str_scene = "SS_MidScreen";

        //Show the old game Box Art.
        CPH.ObsSetBrowserSource(str_scene, "Old GameBox Art", str_art);
        CPH.ObsShowSource(str_scene, "Old GameBox Art");

        //Show Serious Visualizer.
        CPH.ObsShowSource("SS_KiyoPro_FancyCam", "VM_Visualizer_Serious");
        CPH.ObsHideSource("SS_KiyoPro_FancyCam", "VM_Visualizer_Normal");
        CPH.ObsHideSource("SS_MidScreen", "soPlayer");

        //Enable Serious mode.
        CPH.SetGlobalVar("seriousMode", true, true);

        //Disable Sound Actions.
        foreach (string s in list_actions)
        {
            CPH.DisableAction(s);
        }//foreach

        //Disable all Rewards.
        for (int i = 0; i < str_rewardGroups.Length - 1; i++)
        {
            CPH.TwitchRewardGroupDisable(str_rewardGroups[i]);
        }//for

        return true;
    }//public bool Execute()
}//public class CPHInline