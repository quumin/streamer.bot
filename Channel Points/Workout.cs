using System;
using System.IO;

/*LIFT!
 * 
 *  Add workout from Raw Input and write to file.
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string[] str_meta, str_src, str_path, str_cond, str_format, str_reward;
        int[] int_cnt;
        string str_ss;
        int int_wait;
        float f_vol;

        //Initializations 
        str_meta = new string[]
        {
            args["user"].ToString(),
            args["rawInput"].ToString(),
            args["__source"].ToString()
        };
        str_src = new string[]
        {
            "Username",
            "Action"
        };
        str_path = new string[]
        {
            $"{CPH.GetGlobalVar<string>("mediaRoot")}LetsGetSalty.mp3",
            "W:\\Streaming\\Exercise.txt",
            "W:\\Streaming\\Exercise.csv"
        };
        str_cond = str_meta[1].Split(new string[] { " |d| " }, StringSplitOptions.None);
        str_format = new string[2];
        str_reward = new string[3];
        int_cnt = new int[2];
        str_ss = "SS_Alerts_Text";
        int_wait = 5000;
        f_vol = CPH.GetGlobalVar<float>("mediaVolume");

        //Check which source triggered the redeem
        switch (str_meta[2])
        {
            //  Channel Points Reward
            case "TwitchRewardRedemption":
                str_reward = new string[]
                {
                    args["rewardId"].ToString(),
                    args["rewardName"].ToString(),
                    args["redemptionId"].ToString()
                };
                int_cnt = new int[]
                {
                    Convert.ToInt32(args["counter"].ToString()),
                    Convert.ToInt32(args["userCounter"].ToString())
                };
                break;
            //  Testing
            case "CommandMessage":
                str_reward = new string[]
                {
                    "rewardID",
                    "LIFT!",
                    "redeemID"
                };
                int_cnt = new int[]
                {
                    1,
                    2
                };
                break;
            default:
                return true;
                break;
        }//switch

        //If the delimiter was used incorrectly...
        if (str_cond.Length > 2)
        {
            CPH.SendMessage($"/me {str_meta[0]} you used the delimiter too many times... disGUSTING ");
            //... and if I'm live...
            if (CPH.ObsIsStreaming())
            {
                //... refund them.
                CPH.TwitchRedemptionCancel(str_reward[0], str_reward[2]);
            }//if
            return true;
        }//if
         //If the delimiter was used correctly...
        else if (str_cond.Length == 2)
        {
            //... prompt user & enable death counter.
            CPH.SendMessage($"/me @{str_meta[0]} - Death Counter is enabled! Use \'!died\' to increase your counter! WICKED");
            CPH.EnableAction("Workout - Death Counter");
            str_format = new string[]
            {
                $"[_]: {str_meta[0]} [{int_cnt[1]}]/[{int_cnt[0]}] - \'{str_cond[0]}\' ({str_cond[1]})",
                $"{str_meta[0]};{int_cnt[1]};{int_cnt[0]};{str_cond[0]};{str_cond[1]};{str_reward[2]}"
            };
        }//else if
        else
        {
            //... send normal workout.
            str_format = new string[]
            {
                $"[_]: {str_meta[0]} [{int_cnt[1]}]/[{int_cnt[0]}] - \'{str_cond[0]}\'",
                $"{str_meta[0]};{int_cnt[1]};{int_cnt[0]};{str_cond[0]};;{str_reward[2]}"
            };
        }//else

        //Feedback
        CPH.PlaySound(str_path[0], f_vol, false);
        CPH.SendMessage($"/me 『LIFT!』 LETSGO Thank {str_meta[0]} and flex them muscles Q-mander kumaPls");
        CPH.ObsSetGdiText(str_ss, str_src[0], str_meta[0]);
        CPH.ObsSetGdiText(str_ss, str_src[1], str_reward[1]);
        CPH.ObsShowSource(str_ss, str_src[0]);
        CPH.ObsShowSource(str_ss, str_src[1]);
        CPH.Wait(int_wait);
        CPH.ObsHideSource(str_ss, str_src[1]);
        CPH.ObsHideSource(str_ss, str_src[0]);

        //Write to .txt (Visible on Stream)
        using (StreamWriter sw = File.AppendText(str_path[1]))
        {
            sw.WriteLine(str_format[0]);
            sw.Flush();
        }//using

        //Write to .csv (Not visible on Stream)
        using (StreamWriter sw = File.AppendText(str_path[2]))
        {
            sw.WriteLine(str_format[1]);
            sw.Flush();
        }//using
        return true;
    }//Execute()
}//CPHInline