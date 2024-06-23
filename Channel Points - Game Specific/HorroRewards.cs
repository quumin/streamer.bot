using System;
using System.IO;

/*Horror Game Rewards Handler
 * 
 *  Handle the rewards when redeemed from PoE.
 *  LU: 22-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string msgOut, usrName, obScene, redemptionId, mediaOut, filePath;
        string[] obSource, rewardInfo;
        int[] counters;
        int waitTime;
        float vol;

        //Initializations
        msgOut = "/me 『💀』 ";
        usrName = "@" + args["user"].ToString();
        obScene = "SS_Alerts_Text";
        redemptionId = args["redemptionId"].ToString();
        mediaOut = "";
        filePath = CPH.GetGlobalVar<string>("qminMediaRoot") + "\\Jumpscares\\";
        obSource = new string[]
        {
            "Username",
            "Action"
        };
        rewardInfo = new string[]
        {
            args["rewardId"].ToString(),
            args["rewardName"].ToString()
        };

        counters = new int[]
        {
            Convert.ToInt32(args["counter"].ToString()),
            Convert.ToInt32(args["userCounter"].ToString())
        };
        waitTime = 5000;
        vol = CPH.GetGlobalVar<float>("qminMediaVolume");

        //Select the reward that's relevant
        switch (rewardInfo[0])
        {
            //	FNAF
            case "75b534e1-9f38-4d31-b4da-add59d532314":
                mediaOut = "FNAF.mp3";
                msgOut += usrName + " sent Freddy monkaW !";
                break;
            //	Ghost Groan
            case "9b4d7dbe-851e-4ae9-8510-f2da25451442":
                mediaOut = "Ghost_Groan.mp3";
                msgOut += "Kreygasm ?" + usrName + "... Aware !";
                break;
            //	Granny Jumpscare
            case "b219d203-b085-45fd-88f9-ee44ff47b2cf":
                mediaOut = "Granny.mp3";
                msgOut += "!showemote MEGALUL " + usrName;
                break;
            //	Classic Jumpscare
            case "8efa3d57-6385-474d-aa65-61017beeabf7":
                mediaOut = "Jumpscare.mp3";
                msgOut += " NOWAY " + usrName + " !";
                break;
            //	Oh Shit, a Ghost!
            case "964929dd-6daf-4b8b-bd46-9ce1b61837bc":
                mediaOut = "OS_AGhost.mp3";
                msgOut += " picardAAH " + usrName + " RIPBOZO ?";
                break;
            // Error
            default:
                CPH.LogWarn("『💀』: Something went wrong with \'" + rewardInfo[1] + "\'.");
                CPH.TwitchRedemptionCancel(rewardInfo[0], redemptionId);
                return true;
                break;
        }//switch(str_reward[0])

        //Feedback
        CPH.PlaySound(filePath + mediaOut, vol, false);
        CPH.Wait(waitTime);
        CPH.ObsSetGdiText(obScene, obSource[0], usrName);
        CPH.ObsSetGdiText(obScene, obSource[1], rewardInfo[1]);
        CPH.ObsShowSource(obScene, obSource[0]);
        CPH.ObsShowSource(obScene, obSource[1]);
        CPH.Wait(waitTime);
        CPH.ObsHideSource(obScene, obSource[1]);
        CPH.ObsHideSource(obScene, obSource[0]);

        //Send Message afterwards
        CPH.SendMessage(msgOut);

        return true;
    }//Execute()
}//CPHInline