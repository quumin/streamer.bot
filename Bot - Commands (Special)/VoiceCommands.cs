using System;

/*Voice Commands
 * 
 *  Run the right voice command.
 *  LU: 4-nov-2023
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string[] str_vid;
        string str_path, str_media, str_msg, str_cmd, str_marker, str_act, str_tts;
        float f_vol;
        int int_wait;

        //Initializations
        str_vid = new string[]
        {
            "SS_Alerts",
            ""
        };
        str_path = CPH.GetGlobalVar<string>("qminMediaRoot");
        str_media = str_act = str_tts = "";
        str_marker = "『🎙』 ";
        str_msg = "/me ";
        str_cmd = args["spokenCommand"].ToString();
        f_vol = CPH.GetGlobalVar<float>("qminMediaVolume");
        int_wait = 0;

        //See which reward it is...
        switch (str_cmd)
        {
            //  Existing Actions
            //	    Discord
            case "join my discord":
                str_act = "About - Discord";
                break;
            //	    EZ Clap
            case "easy clap":
                str_act = "Road Roller Da (Tokio Tomare)";
                break;
            //	    Steam	
            case "add me on steam":
                str_act = "About - Steam Friend Code";
                break;
            //  Sounds
            //	    Columbo OMT
            case "one more thing":
                str_media = "OneMoreThing";
                str_msg += "SUSSY Yeah right, commander. LULdata";
                str_marker += "OMT";
                break;
            //	    Cozy Time
            case "cozy time":
                str_media = "GruntBirthday";
                str_msg += "BLANKIES YES! Cozy Time! BLANKIES";
                break;
            //	    Yare Yare
            case "good grief":
                str_media = "Jojo_Yare";
                str_msg += "JotaroFlex やれやれだぜ peepoJoJo";
                break;
            //  Video
            //	    Oh Shit
            case "ohshit":
                str_media = "Jojo_Awaken";
                str_msg += "monkaW ?";
                str_marker += " MENACING";
                str_vid[1] += "Menacing";
                int_wait = 7090;
                break;
            //  TTS    
            //	    Thanks
            case "nice job data":
                str_tts = "You're welcome Q-Mander!";
                str_msg += "PETTHEMODS You\'re welcome Q-mander! peepoShy";
                break;
            //	    Insult Data
            case "data you ignorant slut":
                str_tts = "You're the one who programmed me, asshole.";
                str_msg += "POUTING You're the one who programmed me, asshole. OhDear";
                break;
        }//switch

        //If there is an action to run...
        if (!string.IsNullOrEmpty(str_act))
        {
            //... run it and exit.
            CPH.RunAction(str_act);
            return true;
        }//if

        //If I'm live and there is a marker...
        if (CPH.ObsIsStreaming() &&
            !string.IsNullOrEmpty(str_marker))
        {
            //... create a marker.
            CPH.CreateStreamMarker(str_marker);
        }//if

        //If there is media to run...
        if (!string.IsNullOrEmpty(str_media))
        {
            //... run it.
            CPH.PlaySound(str_path + str_media + ".mp3", f_vol, false);
        }//if


        //If there is TTS to run...
        if (!string.IsNullOrEmpty(str_tts))
        {
            //... run it.
            CPH.TtsSpeak("Brian", str_tts);
        }//if

        //Feedback
        CPH.SendMessage(str_msg);

        //If there is video to run...
        if (!string.IsNullOrEmpty(str_vid[1]))
        {
            //... run it, wait, and hide it.
            CPH.ObsShowSource(str_vid[0], str_vid[1]);
            CPH.Wait(int_wait);
            CPH.ObsHideSource(str_vid[0], str_vid[1]);
        }//if

        return true;
    }//Execute()
}//CPHInline