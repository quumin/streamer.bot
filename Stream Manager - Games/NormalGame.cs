using System;
using System.IO;
using System.Collections.Generic;

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        List<string> list_actions;
        List<TwitchReward> list_rewards;
        string[] str_rewardGroups;
        string str_art, str_scene;
        int int_id;

        //Initializations
        list_actions = CPH.GetGlobalVar<List<string>>("soundInteractActions");
        list_rewards = CPH.TwitchGetRewards();
        str_rewardGroups = new string[] {"Standard",
            "Standard - Sounds",
            "GS - DD2",
            "GS - PoE" };
        str_art = args["oldGameBoxArt"].ToString();
        str_scene = "SS_MidScreen";
        int_id = 0;

        //Get Game ID Global
        int_id = Convert.ToInt32(args["gameId"].ToString());

        //Show the old game Box Art.
        CPH.ObsSetBrowserSource(str_scene, "Old GameBox Art", str_art);
        CPH.ObsShowSource(str_scene, "Old GameBox Art");

        //Show Normal Visualizer.
        CPH.ObsHideSource("SS_KiyoPro_FancyCam", "VM_Visualizer_Serious");
        CPH.ObsShowSource("SS_KiyoPro_FancyCam", "VM_Visualizer_Normal");
        CPH.ObsShowSource("SS_MidScreen", "soPlayer");

        //Disable Serious Mode
        CPH.SetGlobalVar("seriousMode", false, true);

        //Check if Standard Rewards are Already Enabled
        if (!list_rewards[0].Enabled)
        {
            //	Enable Normal Rewards.
            CPH.TwitchRewardGroupEnable(str_rewardGroups[0]);
            CPH.TwitchRewardGroupEnable(str_rewardGroups[1]);
            //	Enable Sound Actions.
            foreach (string s in list_actions)
            {
                CPH.EnableAction(s);
            }//foreach
        }//if

        //Disable Game Specific Rewards.
        switch (int_id)
        {
            //	Path of Exile
            case 29307:
                if (list_rewards[3].Enabled)
                {
                    CPH.TwitchRewardGroupDisable(str_rewardGroups[2]);
                }//if
                CPH.SetGlobalVar("specificMode", true, true);
                break;
            //	Darkest Dungeon II
            case 511471:
                if (list_rewards[30].Enabled)
                {
                    CPH.TwitchRewardGroupDisable(str_rewardGroups[3]);
                }//if
                CPH.SetGlobalVar("specificMode", true, true);
                break;
            //	Every other Game
            default:
                if (list_rewards[3].Enabled)
                {
                    CPH.TwitchRewardGroupDisable(str_rewardGroups[2]);
                }//if
                if (list_rewards[30].Enabled)
                {
                    CPH.TwitchRewardGroupDisable(str_rewardGroups[3]);
                }//if
                CPH.SetGlobalVar("specificMode", true, true);
                break;
        }//switch(int_id)

        return true;
    }//Execute()
}//CPHInline