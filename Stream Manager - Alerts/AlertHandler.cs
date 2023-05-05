using System;

/*Alert Handler
 * 
 *  Handle the Alerts for stream.
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string str_event, str_snd, str_img, str_alias, str_usr, str_msg, str_path;
        string[] str_ss, str_src, str_txt;
        bool bool_sers;
        int[] int_wait;
        int int_tcl;
        float f_vol;

        //Initializations
        str_event = args["__source"].ToString();
        str_snd = str_img = str_alias = str_usr = str_msg = "";
        str_path = "W:\\Streaming\\Media\\Sounds\\";
        str_ss = new string[]
        {
            "SS_Alerts_Text",
            "SS_Alerts",
            "SS_KP_PreHex"
        };
        str_src = new string[]
        {
            "Username",
            "Action",
            "Sub_Gotcha",
            "quuminL"
        };
        str_txt = new string[3];
        bool_sers = CPH.GetGlobalVar<bool>("seriousMode");
        int_wait = new int[] { 2000, 2000, 2000 };
        int_tcl = 200;
        f_vol = 0.15f;

        //Check the event type.
        switch (str_event)
        {
            //  Follow
            case "TwitchFollow":
                //Defaults
                str_usr = args["user"].ToString();
                str_img = "SaltBae";
                str_snd = "CarelessWhisper.mp3";
                //Waits
                int_wait[0] = 1130;
                int_wait[1] = 2000;
                int_wait[2] = 5870;
                //Update Texts & Messages
                str_txt[0] = str_usr;
                str_txt[1] = "just salted me with some ";
                str_txt[2] = "quuminL";
                str_msg = "/me SaltBae ";
                // While message is less than character limit...
                while (str_msg.Length < int_tcl)
                {
                    //... add quuminL.
                    str_msg += "quuminL ";
                }//while

                break;
            //  Welcome
            case "TwitchFirstWord":
                //Defaults
                str_usr = args["userName"].ToString();
                // Check if user should be ignored before continuing.
                switch (str_usr)
                {
                    case "ltqmanderdata":
                    case "whymusticryy":
                    case "quumin":
                    case "streamcaptainbot":
                    case "dixperbro":
                    case "soundalerts":
                        return true;
                        break;
                }//switch

                str_img = "Welcome";
                //Wait
                int_wait[0] = 6070;
                //Update Texts
                str_txt[0] = str_usr;
                str_txt[1] = "welcome to the stream, son";
                break;
            //  Sub
            case "TwitchSub":
                //Defaults
                str_usr = args["user"].ToString();
                str_img = "BB Subscribe";
                str_alias = "Brian";
                str_msg = args["rawInput"].ToString();
                string str_tier = args["tier"].ToString();
                //Wait
                int_wait[1] = 11000;
                //Update Texts
                str_txt[0] = str_usr;
                str_txt[1] = "cooked up a " + str_tier + " sub";
                str_txt[2] = "... BITCH.";
                break;
            //  Resub
            case "TwitchReSub":
                //Defaults
                str_usr = args["user"].ToString();
                str_img = "Spicy";
                str_snd = "WelcomeBack.mp3";
                str_alias = "Brian";
                str_msg = args["rawInput"].ToString();
                //Wait
                int_wait[1] = 5464;
                //Update Texts
                str_txt[0] = str_usr;
                str_txt[1] = "returned for za spice";
                str_txt[2] = "... BITCH.";
                break;
            //  Raid
            case "TwitchRaid":
                //Defaults
                str_usr = args["user"].ToString();
                str_img = "Raid";
                str_snd = "CrabRAID.mp3";
                str_alias = "Brian";
                string str_viewers = args["viewers"].ToString();
                //Update Texts & Messages
                str_msg = str_usr + " brought " + str_viewers + " for a hot pantsu raid!";
                str_txt[0] = str_usr;
                str_txt[1] = "thanks for bringing " + str_viewers;
                break;
            //  Bits
            case "TwitchCheer":
                //Defaults
                str_img = "Spare Change";
                str_snd = "Shulk_Bitties.mp3";
                str_alias = "Takumi";
                str_msg = args["message"].ToString();
                string str_bit = args["bits"].ToString();
                bool bool_anon = Convert.ToBoolean(args["anonymous"]);
                //If they are not anonymous...
                if (!bool_anon)
                {
                    //... use the username.
                    str_usr = args["user"].ToString();
                } //if
                else
                {
                    //... otherwise use anonymous.
                    str_usr = "Anonymous";
                }//else

                //Update Texts
                str_txt[0] = str_usr + " gave Q-min";
                str_txt[1] = str_bit;
                //If plural...
                if (str_bit == "1")
                {
                    //... single bit.
                    str_txt[1] += " SPICY bit to nom";
                }//if
                else
                {
                    //... multi bits.
                    str_txt[1] += " SPICY bits to nom";
                }//else

                break;
            //  Other
            default:
                //Event not recognized.
                return true;
                break;
        }//switch


        //Check if Serious Mode is inactive...
        if (!bool_sers)
        {
            //... update the common text.
            CPH.ObsSetGdiText(str_ss[0], str_src[0], str_txt[0]); //Username
            CPH.ObsSetGdiText(str_ss[0], str_src[1], str_txt[1]); //Action
            //... show the sources.
            CPH.ObsShowSource(str_ss[0], str_src[0]); //Username
            CPH.ObsShowSource(str_ss[0], str_src[1]); //Action
            CPH.ObsShowSource(str_ss[1], str_img); //Alert Media
            //... show event specifics.
            switch (str_event)
            {
                //  Follow
                case "TwitchFollow":
                    //Show quuminL and SaltBae
                    CPH.ObsShowSource(str_ss[0], str_src[3]);
                    CPH.ObsShowSource(str_ss[2], str_img);
                    //Play Sound
                    CPH.PlaySound(str_path + str_snd, f_vol);
                    //Wait until the Salt Appears & Send Message		
                    CPH.Wait(int_wait[1]);
                    CPH.SendMessage(str_msg, true);
                    break;
                //  Sub
                case "TwitchSub":
                //  Resub
                case "TwitchReSub":
                    //Update, Wait, & Show Gotcha
                    CPH.ObsSetGdiText(str_ss[0], str_src[2], str_txt[2]);
                    CPH.Wait(int_wait[1]);
                    CPH.ObsShowSource(str_ss[0], str_src[2]);
                    CPH.TtsSpeak(str_alias, str_msg, true);
                    break;
                //  Other
                default:
                    //Sounds & Image/Video
                    CPH.PlaySound(str_path + str_snd, f_vol, true);
                    CPH.TtsSpeak(str_alias, str_msg, true);
                    break;
            } //switch

            //... use common delay.
            CPH.Wait(int_wait[0]);
            //... hide the common media.
            CPH.ObsHideSource(str_ss[1], str_img);
            //... hide event specifics.
            switch (str_event)
            {
                //  Follow
                case "TwitchFollow":
                    //Hide SaltBae on cam & wait to let quuminL linger
                    CPH.ObsHideSource(str_ss[2], str_img);
                    CPH.Wait(int_wait[2]);
                    CPH.ObsHideSource(str_ss[0], str_src[3]);
                    break;
                //  Sub
                case "TwitchSub":
                //  Resub
                case "TwitchReSub":
                    //Hide Gotcha
                    CPH.ObsHideSource(str_ss[0], str_src[2]);
                    break;
            }//switch

            //... hide the common text.
            CPH.ObsHideSource(str_ss[0], str_src[1]);
            CPH.ObsHideSource(str_ss[0], str_src[0]);
        } //if
        else
        {
            //... show event specifics.
            switch (str_event)
            {
                //  Follow
                case "TwitchFollow":
                    CPH.SendMessage("/me 『SERIOUS ALERT』 " + str_txt[0] + " " + str_txt[1] + " " + str_txt[2] + "!");
                    break;
                //  Other
                default:
                    CPH.SendMessage("/me 『SERIOUS ALERT』 " + str_txt[0] + " " + str_txt[1] + "!");
                    break;
            }//switch
        } //else

        return true;
    }//Execute()
}//CPHInline