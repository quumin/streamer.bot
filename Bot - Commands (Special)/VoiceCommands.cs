using System;
using QminBotDLL;

/*Voice Commands
 * 
 *  Run the right voice command.
 *  LU: 5-nov-2023
 * 
 */

public class CPHInline
{
    public void Init()
    {
        //Set Static Class in QnamicLib to active instance of CPH
        QnamicLib.CPH = CPH;
    }//Init()

    public bool Execute()
    {
        //Declarations
        string[] usedGlobals, usedActions, videoPlay;
        bool[] usedActionsExist;
        string filePath, soundMedia, msgOut, trigId, markerOut, targetAction, ttsOut;
        float mediaVol;
        int waitTime;

        //Initializations
        //  Global List
        usedGlobals = new string[]
        {
            "qminMediaRoot",
            "qminMediaVolume"
        };
        filePath = CPH.GetGlobalVar<string>(usedGlobals[0]);
        mediaVol = CPH.GetGlobalVar<float>(usedGlobals[1]);
        //Action List
        usedActions = new string[]
        {
            "About - Discord",
            "Road Roller Da (TokioTomare)",
            "About - Steam Friend Code"
        };
        usedActionsExist = QnamicLib.CheckCPHActions(usedActions);
        //  SB Args
        trigId = args["triggerId"].ToString();
        //  Specific
        videoPlay = new string[]
        {
            "SS_Alerts",
            ""
        };
        soundMedia = targetAction = ttsOut = "";
        markerOut = "『🎙』 ";
        msgOut = "/me ";
        waitTime = 0;

        //See which reward it is...
        switch (trigId)
        {
            //  Existing Actions
            //	    Discord
            case "02b68b3c-7cd9-4bf5-8959-48a57764e226":
                targetAction = usedActions[0];
                break;
            //	    EZ Clap
            case "81e9a904-e4cc-4f6a-a2ca-c8afdafe437f":
                targetAction = usedActions[1];
                break;
            //	    Steam	
            case "6a75a2bb-011f-4e47-a424-a27c3f11e341":
                targetAction = usedActions[2];
                break;
            //  Sounds
            //	    Columbo OMT
            case "8e273473-5d63-4c02-b9bd-929a425c727b":
                soundMedia = "OneMoreThing";
                msgOut += "SUSSY Yeah right, commander. LULdata";
                markerOut += "OMT";
                break;
            //	    Cozy Time
            case "b239763c-99be-43b7-9b54-b637736092cd":
                soundMedia = "GruntBirthday";
                msgOut += "BLANKIES YES! Cozy Time! BLANKIES";
                break;
            //	    Yare Yare
            case "beb6c98b-53c1-42f5-a8db-e23b54a8551c":
                soundMedia = "Jojo_Yare";
                msgOut += "JotaroFlex やれやれだぜ peepoJoJo";
                break;
            //  Video
            //	    Oh Shit
            case "bafc9a5b-3dd2-4402-bfa6-9aa161e54c3e":
                soundMedia = "Jojo_Awaken";
                msgOut += "monkaW ?";
                markerOut += " MENACING";
                videoPlay[1] += "Menacing";
                waitTime = 7090;
                break;
            //  TTS    
            //	    Thanks
            case "09d14ecb-b5d5-446d-9213-b48687fbf9e0":
                ttsOut = "You're welcome Q-Mander!";
                msgOut += "PETTHEMODS You\'re welcome Q-mander! peepoShy";
                break;
            //	    Insult Data
            case "4f61c98f-0411-4a4e-be32-4e256aaa3333":
                ttsOut = "You're the one who programmed me, asshole.";
                msgOut += "POUTING You're the one who programmed me, asshole. OhDear";
                break;
            default:
                CPH.LogWarn("『🎙』: Command not found!");
                return true;
                break;
        }//switch

        //If there is an action to run...
        if (!string.IsNullOrEmpty(targetAction))
        {
            //... run it and exit.
            CPH.RunAction(targetAction);
            return true;
        }//if

        //If I'm live and there is a marker...
        if (CPH.ObsIsStreaming() &&
            !string.IsNullOrEmpty(markerOut))
        {
            //... create a marker.
            CPH.CreateStreamMarker(markerOut);
        }//if

        //If there is media to run...
        if (!string.IsNullOrEmpty(soundMedia))
        {
            //... run it.
            CPH.PlaySound($"{filePath}{soundMedia}.mp3", mediaVol, false);
        }//if


        //If there is TTS to run...
        if (!string.IsNullOrEmpty(ttsOut))
        {
            //... run it.
            CPH.TtsSpeak("Brian", ttsOut);
        }//if

        //Feedback
        CPH.SendMessage(msgOut);

        //If there is video to run...
        if (!string.IsNullOrEmpty(videoPlay[1]))
        {
            //... run it, wait, and hide it.
            CPH.ObsShowSource(videoPlay[0], videoPlay[1]);
            CPH.Wait(waitTime);
            CPH.ObsHideSource(videoPlay[0], videoPlay[1]);
        }//if

        return true;
    }//Execute()
}//CPHInline