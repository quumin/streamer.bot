using System;

/*Alert Handler
 * 
 *  Handle the Alerts for stream.
 *  LU: 23-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string str_event, str_snd, str_img, str_alias, usrName, msgOut, filePath;
        string[] obSubScene, obSource, str_txt;
        bool bool_sers;
        int[] waitTime;
        int charLimit;
        float vol;

        //Initializations
        str_event = args["__source"].ToString();
        str_snd = str_img = str_alias = usrName = msgOut = "";
        filePath = CPH.GetGlobalVar<string>("qminMediaRoot");
        obSubScene = new string[]
        {
            "SS_Alerts_Text",
            "SS_Alerts",
            "SS_KP_PreHex"
        };
        obSource = new string[]
        {
            "Username",
            "Action",
            "Sub_Gotcha",
            "quuminL"
        };
        str_txt = new string[3];
        bool_sers = CPH.GetGlobalVar<bool>("qminSeriousMode");
        waitTime = new int[] { 2000, 2000, 2000 };
        charLimit = 200;
        vol = CPH.GetGlobalVar<float>("qminMediaVolume");

        //Check the event type.
        switch (str_event)
        {
            //  Follow
            case "TwitchFollow":
                //Defaults
                usrName = args["user"].ToString();
                str_img = "SaltBae";
                str_snd = "CarelessWhisper.mp3";
                //Waits
                waitTime[0] = 1130;
                waitTime[1] = 2000;
                waitTime[2] = 5870;
                //Update Texts & Messages
                str_txt[0] = usrName;
                str_txt[1] = "just salted me with some ";
                str_txt[2] = "quuminL";
                msgOut = "/me SaltBae ";
                // While message is less than character limit...
                while (msgOut.Length < charLimit)
                {
                    //... add quuminL.
                    msgOut += "quuminL ";
                }//while

                break;
            //  Welcome
            case "TwitchFirstWord":
                //Defaults
                usrName = args["userName"].ToString();
                // Check if user should be ignored before continuing.
                switch (usrName)
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
                waitTime[0] = 6070;
                //Update Texts
                str_txt[0] = usrName;
                str_txt[1] = "welcome to the stream, son";
                break;
            //  Sub
            case "TwitchSub":
                //Defaults
                usrName = args["user"].ToString();
                str_img = "BB Subscribe";
                str_alias = "Brian";
                msgOut = args["rawInput"].ToString();
                string str_tier = args["tier"].ToString();
                //Wait
                waitTime[1] = 11000;
                //Update Texts
                str_txt[0] = usrName;
                str_txt[1] = "cooked up a " + str_tier + " sub";
                str_txt[2] = "... BITCH.";
                break;
            //  Resub
            case "TwitchReSub":
                //Defaults
                usrName = args["user"].ToString();
                str_img = "Spicy";
                str_snd = "WelcomeBack.mp3";
                str_alias = "Brian";
                msgOut = args["rawInput"].ToString();
                //Wait
                waitTime[1] = 5464;
                //Update Texts
                str_txt[0] = usrName;
                str_txt[1] = "returned for za spice";
                str_txt[2] = "... BITCH.";
                break;
            //  Raid
            case "TwitchRaid":
                //Defaults
                usrName = args["user"].ToString();
                str_img = "Raid";
                str_snd = "CrabRAID.mp3";
                str_alias = "Brian";
                string str_viewers = args["viewers"].ToString();
                //Update Texts & Messages
                msgOut = usrName + " brought " + str_viewers + " for a hot pantsu raid!";
                str_txt[0] = usrName;
                str_txt[1] = "thanks for bringing " + str_viewers;
                break;
            //  Bits
            case "TwitchCheer":
                //Defaults
                str_img = "Spare Change";
                str_snd = "Shulk_Bitties.mp3";
                str_alias = "Takumi";
                msgOut = args["message"].ToString();
                string str_bit = args["bits"].ToString();
                bool bool_anon = Convert.ToBoolean(args["anonymous"]);
                //If they are not anonymous...
                if (!bool_anon)
                {
                    //... use the username.
                    usrName = args["user"].ToString();
                } //if
                else
                {
                    //... otherwise use anonymous.
                    usrName = "Anonymous";
                }//else

                //Update Texts
                str_txt[0] = usrName + " gave Q-min";
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
            CPH.ObsSetGdiText(obSubScene[0], obSource[0], str_txt[0]); //Username
            CPH.ObsSetGdiText(obSubScene[0], obSource[1], str_txt[1]); //Action
            //... show the sources.
            CPH.ObsShowSource(obSubScene[0], obSource[0]); //Username
            CPH.ObsShowSource(obSubScene[0], obSource[1]); //Action
            CPH.ObsShowSource(obSubScene[1], str_img); //Alert Media
            //... show event specifics.
            switch (str_event)
            {
                //  Follow
                case "TwitchFollow":
                    //Show quuminL and SaltBae
                    CPH.ObsShowSource(obSubScene[0], obSource[3]);
                    CPH.ObsShowSource(obSubScene[2], str_img);
                    //Play Sound
                    CPH.PlaySound(filePath + str_snd, vol);
                    //Wait until the Salt Appears & Send Message		
                    CPH.Wait(waitTime[1]);
                    CPH.SendMessage(msgOut, true);
                    break;
                //  Sub
                case "TwitchSub":
                //  Resub
                case "TwitchReSub":
                    //Update, Wait, & Show Gotcha
                    CPH.ObsSetGdiText(obSubScene[0], obSource[2], str_txt[2]);
                    CPH.Wait(waitTime[1]);
                    CPH.ObsShowSource(obSubScene[0], obSource[2]);
                    CPH.TtsSpeak(str_alias, msgOut, true);
                    break;
                //  Other
                default:
                    //Sounds & Image/Video
                    CPH.PlaySound(filePath + str_snd, vol, true);
                    CPH.TtsSpeak(str_alias, msgOut, true);
                    break;
            } //switch

            //... use common delay.
            CPH.Wait(waitTime[0]);
            //... hide the common media.
            CPH.ObsHideSource(obSubScene[1], str_img);
            //... hide event specifics.
            switch (str_event)
            {
                //  Follow
                case "TwitchFollow":
                    //Hide SaltBae on cam & wait to let quuminL linger
                    CPH.ObsHideSource(obSubScene[2], str_img);
                    CPH.Wait(waitTime[2]);
                    CPH.ObsHideSource(obSubScene[0], obSource[3]);
                    break;
                //  Sub
                case "TwitchSub":
                //  Resub
                case "TwitchReSub":
                    //Hide Gotcha
                    CPH.ObsHideSource(obSubScene[0], obSource[2]);
                    break;
            }//switch

            //... hide the common text.
            CPH.ObsHideSource(obSubScene[0], obSource[1]);
            CPH.ObsHideSource(obSubScene[0], obSource[0]);
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