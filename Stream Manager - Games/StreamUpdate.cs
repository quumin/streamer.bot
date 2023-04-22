using System;

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string[] str_game;
        string str_stat, str_msg, str_srs, str_scene;
        bool bool_gam, bool_tit, bool_serious;
        int int_wait;

        //Initializations
        str_game = new string[3];
        str_stat = args["status"].ToString();
        str_msg = "";
        str_srs = "seriousMode";
        str_scene = "SS_MidScreen";
        bool_gam = Convert.ToBoolean(args["gameUpdate"]);
        bool_tit = Convert.ToBoolean(args["statusUpdate"]);
        int_wait = 2000;

        try
        {
            //... to get serious.
            bool_serious = CPH.GetGlobalVar<bool>(str_srs);
        }//try
        catch (Exception e)
        {
            //... key exception.
            bool_serious = false;
            CPH.SetGlobalVar(str_srs, false, true);
        }//catch (Exception e)

        //If the game is not serious...
        if (!bool_serious)
        {
            //... update message to be only the "game."
            str_msg = "『SYSTEM CHECK』 ";
        }//if(!bool_serious)	
        else
        {
            //... update message to be "serious game."
            str_msg = "『SERIOUS UPDATE』 ";
        }//else

        CPH.LogInfo("『SERIOUS CHECK』: " + bool_serious);

        //If the title updates...
        if (bool_tit)
        {
            //... add status to the message.
            str_msg += str_stat;
            //... log stuff.
            CPH.LogInfo("『STATUS UPDATE』: Title update to \'" + str_stat + "\'!");
            CPH.LogInfo("『MARKER』: STATUS_UPDATE");
        }//if (bool_tit)

        //If both update at the same time...
        if (bool_tit && bool_gam)
        {
            //... add a divider.
            str_msg += " | ";
        }//if (bool_tit && bool_gam)  	

        //If the game updates...
        if (bool_gam)
        {
            //... get game info.
            str_game[0] = args["gameName"].ToString();
            str_game[1] = args["gameBoxArt"].ToString();
            str_game[2] = args["oldGameName"].ToString();

            //... show the game box art change.
            CPH.ObsSetBrowserSource(str_scene, "New GameBox Art", str_game[1]);
            CPH.Wait(int_wait);
            CPH.ObsHideSource(str_scene, "Old GameBox Art");
            CPH.ObsShowSource(str_scene, "New GameBox Art");
            CPH.Wait(int_wait);
            CPH.ObsHideSource(str_scene, "New GameBox Art");

            //... log stuff.
            CPH.LogInfo("『GAME UPDATE』: Game update from \'" + str_game[2] + "\' to \'" + str_game[0] + "\'!");
            str_msg += str_game[2] + " -> " + str_game[0];

            //... If OBS is streaming...
            if (CPH.ObsIsStreaming())
            {
                //... create a marker.
                CPH.CreateStreamMarker("CHANGE - " + str_game[2]);

            }//if (CPH.ObsIsStreaming())

            CPH.LogInfo("『MARKER』: GAME_UPDATE");
        }//if (bool_gam)

        //Feedback in Chat
        CPH.TwitchAnnounce(str_msg,
            true,
            "purple");

        return true;
    }//public bool Execute()
}//public class CPHInline