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
        string sndMedia, imgMedia, ttsAlias, msgOut, filePath, obsSceneAlertsText, obsSceneAlerts, obsSceneCam, obsUsername, obsAction, obsGotcha, obsQ, gotchaMsg, alertMsg;
        bool srsMode;
        int[] waitTime;
        int charLimit, commonWaitTime, specialWaitTime, gotchaWaitTime;
        float vol;

        //Initializations
        srsMode = CPH.GetGlobalVar<bool>("qminSeriousMode");
        vol = CPH.GetGlobalVar<float>("qminMediaVolume");
        filePath = CPH.GetGlobalVar<string>("qminMediaRoot");
        CPH.TryGetArg("__source", out string eventSource);
        CPH.TryGetArg("userName", out string usrName);
        CPH.TryGetArg("rawInput", out string rawInput);
        CPH.TryGetArg("tier", out string tier);
        CPH.TryGetArg("viewers", out string raidViewers);
        CPH.TryGetArg("message", out string bitMsg);
        CPH.TryGetArg("bits", out string bitNum);

        sndMedia = imgMedia = ttsAlias = msgOut = gotchaMsg = alertMsg = "";
        obsSceneAlertsText = "SS_Alerts_Text";
        obsSceneAlerts = "SS_Alerts";
        obsSceneCam = "SS_KP_No-Filter";
        obsUsername = "Username";
        obsAction = "Action";
        obsGotcha = "Sub_Gotcha";
        obsQ = "quuminL";
        
        commonWaitTime = specialWaitTime = gotchaWaitTime = 2000;
        charLimit = 200;


        //Check the event type.
        switch (eventSource)
        {
            //  Follow
            case "TwitchFollow":
                //Defaults
                imgMedia = "SaltBae";
                sndMedia = "CarelessWhisper.mp3";
                //Waits
                commonWaitTime = 1130;
                specialWaitTime = 2000;
                gotchaWaitTime = 5870;
                //Update Texts & Messages
                alertMsg = $"{usrName} just saled me with some quuminL";
                msgOut = "SaltBae ";
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

                imgMedia = "Welcome";
                //Wait
                commonWaitTime = 6070;
                //Update Texts
                alertMsg = $"{usrName} welcome to the stream, son/daughter/kiddo!";
                break;
            //  Sub
            case "TwitchSub":
                //Defaults
                imgMedia = "BB Subscribe";
                ttsAlias = "Brian";
                
                msgOut = rawInput;
                //Wait
                specialWaitTime = 11000;
                //Update Texts
                alertMsg = $"{usrName} cooked up a {tier} sub!";
                gotchaMsg = "... BITCH.";
                break;
            //  Resub
            case "TwitchReSub":
                //Defaults
                imgMedia = "Spicy";
                sndMedia = "WelcomeBack.mp3";
                ttsAlias = "Brian";
                msgOut = rawInput;
                //Wait
                specialWaitTime = 5464;
                //Update Texts
                alertMsg = $"{usrName} returned for za spice!";
                gotchaMsg = "... BITCH.";
                break;
            //  Raid
            case "TwitchRaid":
                //Defaults
                imgMedia = "Raid";
                sndMedia = "CrabRAID.mp3";
                ttsAlias = "Brian";
                //Update Texts & Messages
                msgOut = $"{usrName} brought {raidViewers} for a hot pantsu raid!";
                alertMsg = $"{usrName}, thanks for bringing {raidViewers}!";
                break;
            //  Bits
            case "TwitchCheer":
                //Defaults
                imgMedia = "Spare Change";
                sndMedia = "Shulk_Bitties.mp3";
                ttsAlias = "Takumi";
                msgOut = bitMsg;
                CPH.TryGetArg("anonymous", out bool isAnon);
                //If they are anonymous...
                if (isAnon)
                {
                    usrName = "Anonymous";
                } //if

                //Update Texts
                alertMsg = $"{usrName} gave Q-min {bitNum}";
                //If plural...
                if (bitNum == "1")
                {
                    //... single bit.
                    alertMsg += " SPICY bit to nom";
                }//if
                else
                {
                    //... multi bits.
                    alertMsg += " SPICY bits to nom";
                }//else
                break;
            //  Other
            default:
                //Event not recognized.
                return true;
                break;
        }//switch


        //Check if Serious Mode is inactive...
        if (!srsMode)
        {
            //... update the common text.
            CPH.ObsSetGdiText(obsSceneAlertsText, obsUsername, usrName); //Username
            CPH.ObsSetGdiText(obsSceneAlertsText, obsAction, alertMsg); //Action
            //... show the sources.
            CPH.ObsShowSource(obsSceneAlertsText, obsUsername); //Username
            CPH.ObsShowSource(obsSceneAlertsText, obsAction); //Action
            CPH.ObsShowSource(obsSceneAlerts, imgMedia); //Alert Media
            //... show event specifics.
            switch (eventSource)
            {
                //  Follow
                case "TwitchFollow":
                    //Show quuminL and SaltBae
                    CPH.ObsShowSource(obsSceneAlertsText, obsQ);
                    CPH.ObsShowSource(obsSceneCam, imgMedia);
                    //Play Sound
                    CPH.PlaySound($"{filePath}{sndMedia}", vol);
                    //Wait until the Salt Appears & Send Message		
                    CPH.Wait(specialWaitTime);
                    CPH.SendMessage(msgOut, true);
                    break;
                //  Sub
                case "TwitchSub":
                //  Resub
                case "TwitchReSub":
                    //Update, Wait, & Show Gotcha
                    CPH.ObsSetGdiText(obsSceneAlertsText, obsGotcha, gotchaMsg);
                    CPH.Wait(specialWaitTime);
                    CPH.ObsShowSource(obsSceneAlertsText, obsGotcha);
                    CPH.TtsSpeak(ttsAlias, msgOut, true);
                    break;
                //  Other
                default:
                    //Sounds & Image/Video
                    CPH.PlaySound($"{filePath}{sndMedia}", vol, true);
                    CPH.TtsSpeak(ttsAlias, msgOut, true);
                    break;
            } //switch

            //... use common delay.
            CPH.Wait(commonWaitTime);
            //... hide the common media.
            CPH.ObsHideSource(obsSceneAlerts, imgMedia);
            //... hide event specifics.
            switch (eventSource)
            {
                //  Follow
                case "TwitchFollow":
                    //Hide SaltBae on cam & wait to let quuminL linger
                    CPH.ObsHideSource(obsSceneCam, imgMedia);
                    CPH.Wait(gotchaWaitTime);
                    CPH.ObsHideSource(obsSceneAlertsText, obsQ);
                    break;
                //  Sub
                case "TwitchSub":
                //  Resub
                case "TwitchReSub":
                    //Hide Gotcha
                    CPH.ObsHideSource(obsSceneAlertsText, obsGotcha);
                    break;
            }//switch

            //... hide the common text.
            CPH.ObsHideSource(obsSceneAlertsText, obsAction);
            CPH.ObsHideSource(obsSceneAlertsText, obsUsername);
        } //if
        else
        {
            //... show event specifics.
            CPH.TwitchAnnounce($"『SERIOUS ALERT』{msgOut}!",true,"orange", false);
        } //else

        return true;
    }//Execute()
}//CPHInline