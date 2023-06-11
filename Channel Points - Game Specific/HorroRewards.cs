using System;
using System.IO;

/*Horror Game Rewards Handler
 * 
 *  Handle the rewards when redeemed from PoE.
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string str_msg, str_usr, str_ss, str_redeem, str_media, str_path;
        string[] str_src, str_reward;
        int[] int_cnt;
        int int_wait;
        float f_vol;

        //Initializations
        str_msg = "/me 『💀』 ";
        str_usr = "@" + args["user"].ToString();
        str_ss = "SS_Alerts_Text";
        str_redeem = args["redemptionId"].ToString();
        str_media = "";
        str_path = CPH.GetGlobalVar<string>("mediaRoot") + "\\Jumpscares\\";
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

        int_cnt = new int[]
        {
            Convert.ToInt32(args["counter"].ToString()),
            Convert.ToInt32(args["userCounter"].ToString())
        };
        int_wait = 5000;
        f_vol = CPH.GetGlobalVar<float>("mediaVolume");

        //Select the reward that's relevant
        switch (str_reward[0])
        {
            //	FNAF
            case "75b534e1-9f38-4d31-b4da-add59d532314":
                str_media = "FNAF.mp3";
                str_msg += str_usr + " sent Freddy monkaW !";
                break;
            //	Ghost Groan
            case "9b4d7dbe-851e-4ae9-8510-f2da25451442":
                str_media = "Ghost_Groan.mp3";
                str_msg += "Kreygasm ?" + str_usr + "... Aware !";
                break;
            //	Granny Jumpscare
            case "b219d203-b085-45fd-88f9-ee44ff47b2cf":
                str_media = "Granny.mp3";
                str_msg += "!showemote MEGALUL " + str_usr;
                break;
            //	Classic Jumpscare
            case "8efa3d57-6385-474d-aa65-61017beeabf7":
                str_media = "Jumpscare.mp3";
                str_msg += " NOWAY " + str_usr + " !";
                break;
            //	Oh Shit, a Ghost!
            case "964929dd-6daf-4b8b-bd46-9ce1b61837bc":
                str_media = "OS_AGhost.mp3";
                str_msg += " picardAAH " + str_usr + " RIPBOZO ?";
                break;
            // Error
            default:
                CPH.LogWarn("『💀』: Something went wrong with \'" + str_reward[1] + "\'.");
                CPH.TwitchRedemptionCancel(str_reward[0], str_redeem);
                return true;
                break;
        }//switch(str_reward[0])

        //Feedback
        CPH.PlaySound(str_path + str_media, f_vol, false);
        CPH.Wait(int_wait);
        CPH.ObsSetGdiText(str_ss, str_src[0], str_usr);
        CPH.ObsSetGdiText(str_ss, str_src[1], str_reward[1]);
        CPH.ObsShowSource(str_ss, str_src[0]);
        CPH.ObsShowSource(str_ss, str_src[1]);
        CPH.Wait(int_wait);
        CPH.ObsHideSource(str_ss, str_src[1]);
        CPH.ObsHideSource(str_ss, str_src[0]);

        //Send Message afterwards
        CPH.SendMessage(str_msg);

        return true;
    }//Execute()
}//CPHInline