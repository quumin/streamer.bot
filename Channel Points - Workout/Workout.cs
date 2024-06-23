using System;
using System.IO;

/*LIFT!
 * 
 *  Add workout from Raw Input and write to file.
 *  LU: 4-nov-2023
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string[] metaData, obsSource, filePaths, controlCondition, formatString, rewardList;
        int[] counters;
        string obsScene, deLim;
        int waitTimer;
        float mediaVol;

        //Initializations 
        metaData = new string[]
        {
            "",
            "",
            args["__source"].ToString()
        };
        obsSource = new string[]
        {
            "Username",
            "Action"
        };
        filePaths = new string[]
        {
            $"{CPH.GetGlobalVar<string>("qminMediaRoot")}",
            @".\\external_files\\Exercise.txt",
            @".\\external_files\\Exercise.csv"
        };
        deLim = " |d| ";
        formatString = new string[2];
        rewardList = new string[3];
        counters = new int[2];
        obsScene = "SS_Alerts_Text";
        waitTimer = 5000;
        mediaVol = CPH.GetGlobalVar<float>("qminMediaVolume");

        //Check which source triggered the redeem
        switch (metaData[2])
        {
            //  Channel Points Reward
            case "TwitchRewardRedemption":
                rewardList = new string[]
                {
                    args["rewardId"].ToString(),
                    args["rewardName"].ToString(),
                    args["redemptionId"].ToString()
                };
                counters = new int[]
                {
                    Convert.ToInt32(args["counter"].ToString()),
                    Convert.ToInt32(args["userCounter"].ToString())
                };
                metaData = new string[]
                {
                    args["user"].ToString(),
                    args["rawInput"].ToString(),
                    args["__source"].ToString()
                };
                break;
            //  Testing
            case "HotKeyPress":
            case "CommandMessage":
                rewardList = new string[]
                {
                    /*Reward ID's
                     * Non Death-Counter:
                     *  f3907ebe-0011-4fe4-b85c-7d94101e5f33 
                     * 
                     * Death Counter:
                     *  0d8161d7-1689-49a3-97ca-f5769ecb1b75
                     */
                    "0d8161d7-1689-49a3-97ca-f5769ecb1b75",
                    "LIFT!",
                    "redeemID"
                };
                counters = new int[]
                {
                    1,
                    2
                };
                metaData = new string[]
                {
                    "quumin",
                    $"This is a test{deLim}5",
                    args["__source"].ToString()
                };
                CPH.SendMessage("Testing Workout dataMask", true);
                break;
            default:
                return true;
                break;
        }//switch

        controlCondition = metaData[1].Split(new string[] { deLim }, StringSplitOptions.None);

        //If the delimiter was used incorrectly...
        if ((controlCondition.Length > 2) && (rewardList[0] == "0d8161d7-1689-49a3-97ca-f5769ecb1b75"))
        {
            CPH.SendMessage($"/me {metaData[0]} you used the delimiter too many times... disGUSTING ");
            //... and if I'm live...
            if (CPH.ObsIsStreaming())
            {
                //... refund them.
                CPH.TwitchRedemptionCancel(rewardList[0], rewardList[2]);
            }//if
            return true;
        }//if
         //If the delimiter was used correctly...
        else if ((controlCondition.Length == 2) && (rewardList[0] == "0d8161d7-1689-49a3-97ca-f5769ecb1b75"))
        {
            //... prompt user & enable death counter.
            CPH.SendMessage($"/me @{metaData[0]} - Death Counter is enabled! Use \'!died\' to increase your counter! WICKED");
            CPH.EnableAction("Workout - Death Counter");
            formatString = new string[]
            {
                $"[_]: {metaData[0]} [{counters[1]}]/[{counters[0]}] - \'{controlCondition[0]}\' ({controlCondition[1]})",
                $"{metaData[0]};{counters[1]};{counters[0]};{controlCondition[0]};{controlCondition[1]};{rewardList[2]}"
            };
            CPH.DisableReward(rewardList[0]);
            CPH.EnableCommand("9b23991e-ac4b-48bf-afc4-bce82ad3d674");
            CPH.EnableCommand("69e5a7a8-8d90-4064-b8dd-68a7a63268d1");
            CPH.EnableCommand("d3422d01-3041-49ac-bed8-1d72c9d12dec");
            CPH.EnableCommand("567a1801-dd6e-4a08-9639-e250a89403ba");
            CPH.AddUserToGroup(metaData[0], "WorkOut Starter");
        }//else if
        else
        {
            //... send normal workout.
            formatString = new string[]
            {
                $"[_]: {metaData[0]} [{counters[1]}]/[{counters[0]}] - \'{controlCondition[0]}\'",
                $"{metaData[0]};{counters[1]};{counters[0]};{controlCondition[0]};;{rewardList[2]}"
            };
        }//else

        //Feedback
        CPH.PlaySound($"{filePaths[0]}LetsGetSalty.mp3", mediaVol, false);
        CPH.SendMessage($"/me 『LIFT!』 LETSGO Thank {metaData[0]} and flex them muscles Q-mander kumaPls");
        CPH.ObsSetGdiText(obsScene, obsSource[0], metaData[0]);
        CPH.ObsSetGdiText(obsScene, obsSource[1], rewardList[1]);
        CPH.ObsShowSource(obsScene, obsSource[0]);
        CPH.ObsShowSource(obsScene, obsSource[1]);
        CPH.Wait(waitTimer);
        CPH.ObsHideSource(obsScene, obsSource[1]);
        CPH.ObsHideSource(obsScene, obsSource[0]);

        //Write to .txt (Visible on Stream)
        using (StreamWriter sw = File.AppendText(filePaths[1]))
        {
            sw.WriteLine(formatString[0]);
            sw.Flush();
        }//using

        //Write to .csv (Not visible on Stream)
        using (StreamWriter sw = File.AppendText(filePaths[2]))
        {
            sw.WriteLine(formatString[1]);
            sw.Flush();
        }//using
        return true;
    }//Execute()
}//CPHInline