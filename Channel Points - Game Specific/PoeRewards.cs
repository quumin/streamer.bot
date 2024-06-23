using System;
using System.IO;

/*Path of Exile Rewards Handler
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
        string msgOut, usrName, obScene, redemptionId, mediaOut, rawInput;
        string[] obSource, rewardInfo, filePaths;
        int[] counters;
        int waitTime;
        float vol;

        //Initializations
        msgOut = "/me 『P O E』 ";
        usrName = args["user"].ToString();
        obScene = "SS_Alerts_Text";
        redemptionId = args["redemptionId"].ToString();
        mediaOut = "ChangePlaces";
        rawInput = "";
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
        filePaths = new string[]
        {
            CPH.GetGlobalVar<string>("qminMediaRoot") + "Channel Points\\ChangePlaces.mp3",
            "W:\\Streaming\\PoE Log.txt"
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
            //	New Character
            case "a6e368d2-6026-459a-aefc-66498844ceb3":
                rawInput = args["rawInput"].ToString();
                msgOut += usrName + " wants to make a new character named " + rawInput + "!";
                break;
            //	Skill Gem
            case "ec0c8c6e-6458-42e7-80b5-0db2d11d4734":
                rawInput = args["rawInput"].ToString();
                msgOut += usrName + " wants you to use the Skill " + rawInput + "!";
                break;
            //	Skill Point
            case "74ac9cbb-b977-4baa-a054-f4efa0ed86f6":
                msgOut += usrName + " wants you to choose your next skill point!";
                break;
            //	Weapon
            case "5dc8dc16-f1f8-45b0-97ff-34b01e5ae114":
                rawInput = args["rawInput"].ToString();
                msgOut += usrName + " wants you to use the weapon " + rawInput + "!";
                break;
            //	Respec
            case "1f30adf5-c9aa-42cc-a8cb-bb12ab652005":
                msgOut += usrName + " wants you to Respec!";
                break;
            // Error
            default:
                CPH.LogWarn("『P O E』: Something went wrong with \'" + rewardInfo[1] + "\'.");
                CPH.TwitchRedemptionCancel(rewardInfo[0], redemptionId);
                return true;
                break;
        }//switch(str_reward[0])

        //Feedback
        CPH.ObsSetGdiText(obScene, obSource[0], usrName);
        CPH.ObsSetGdiText(obScene, obSource[1], rewardInfo[1]);
        CPH.PlaySound(filePaths[0], vol, false);
        CPH.SendMessage(msgOut);
        CPH.ObsShowSource(obScene, obSource[0]);
        CPH.ObsShowSource(obScene, obSource[1]);
        CPH.Wait(waitTime);
        CPH.ObsHideSource(obScene, obSource[1]);
        CPH.ObsHideSource(obScene, obSource[0]);

        //Add to file
        using (StreamWriter sw = File.AppendText(filePaths[1]))
        {
            sw.WriteLine("『" + rewardInfo[1] + "』:" + usrName + "T[" + counters[0] + "]|U[" + counters[1] + "]| " + rawInput);
        }//using

        return true;
    }//Execute()
}//CPHInline