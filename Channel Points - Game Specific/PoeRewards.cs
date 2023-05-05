using System;
using System.IO;

/*Path of Exile Rewards Handler
 * 
 *  Handle the rewards when redeemed from PoE.
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string str_msg, str_usr, str_ss, str_redeem, str_media, str_ri;
        string[] str_src, str_reward, str_path;
        int[] int_cnt;
        int int_wait;
        float f_vol;

        //Initializations
        str_msg = "/me 『P O E』 ";
        str_usr = args["user"].ToString();
        str_ss = "SS_Alerts_Text";
        str_redeem = args["redemptionId"].ToString();
        str_media = "ChangePlaces";
        str_ri = "";
        str_src = new string[]
        { 
            "Username", 
            "Action" 
        };
        str_reward = new string[]
        { 
            args["rewardId"].ToString(), 
            args["rewardName"].ToString() 
        };
        str_path = new string[] 
        { 
            "W:\\Streaming\\Media\\Sounds\\Channel Points\\ChangePlaces.mp3",
            "W:\\Streaming\\PoE Log.txt"
        };
        int_cnt = new int[] 
        { 
            Convert.ToInt32(args["counter"].ToString()),
            Convert.ToInt32(args["userCounter"].ToString())
        };
        int_wait = 5000;
        f_vol = 0.15f;

        //Select the reward that's relevant
        switch (str_reward[0])
        {
            //	New Character
            case "a6e368d2-6026-459a-aefc-66498844ceb3":
                str_ri = args["rawInput"].ToString();
                str_msg += str_usr + " wants to make a new character named " + str_ri + "!";
                break;
            //	Skill Gem
            case "ec0c8c6e-6458-42e7-80b5-0db2d11d4734":
                str_ri = args["rawInput"].ToString();
                str_msg += str_usr + " wants you to use the Skill " + str_ri + "!";
                break;
            //	Skill Point
            case "74ac9cbb-b977-4baa-a054-f4efa0ed86f6":
                str_msg += str_usr + " wants you to choose your next skill point!";
                break;
            //	Weapon
            case "5dc8dc16-f1f8-45b0-97ff-34b01e5ae114":
                str_ri = args["rawInput"].ToString();
                str_msg += str_usr + " wants you to use the weapon " + str_ri + "!";
                break;
            //	Respec
            case "1f30adf5-c9aa-42cc-a8cb-bb12ab652005":
                str_msg += str_usr + " wants you to Respec!";
                break;
            // Error
            default:
                CPH.LogWarn("『P O E』: Something went wrong with \'" + str_reward[1] + "\'.");
                CPH.TwitchRedemptionCancel(str_reward[0], str_redeem);
                return true;
                break;
        }//switch(str_reward[0])

        //Feedback
        CPH.ObsSetGdiText(str_ss, str_src[0], str_usr);
        CPH.ObsSetGdiText(str_ss, str_src[1], str_reward[1]);
        CPH.PlaySound(str_path[0], f_vol, false);
        CPH.SendMessage(str_msg);
        CPH.ObsShowSource(str_ss, str_src[0]);
        CPH.ObsShowSource(str_ss, str_src[1]);
        CPH.Wait(int_wait);
        CPH.ObsHideSource(str_ss, str_src[1]);
        CPH.ObsHideSource(str_ss, str_src[0]);

        //Add to file
        using (StreamWriter sw = File.AppendText(str_path[1]))
        {
            sw.WriteLine("『" + str_reward[1] + "』:" + str_usr + "T[" + int_cnt[0] + "]|U[" + int_cnt[1] + "]| " + str_ri);
        }//using

        return true;
    }//Execute()
}//CPHInline