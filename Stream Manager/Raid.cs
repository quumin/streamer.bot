using System;

/*Raid
 * 
 *  Play me out Data!
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string str_path, str_marker, str_usr;
        float f_vol;

        //Initializations
        str_path = CPH.GetGlobalVar<string>("mediaRoot") + "SH_Raid.mp3";
        str_marker = "『RAID』";
        str_usr = args["raidUser"].ToString();
        f_vol = CPH.GetGlobalVar<float>("mediaVolume");

        CPH.CreateStreamMarker(str_marker);
        CPH.PlaySound(str_path, f_vol, false);
        CPH.SendMessage($"/me  quuminPOG Q has started a raid on {str_usr}! Copy/paste the following to show some quuminL !");
        CPH.SendMessage("/ me For subs: quuminL QMIN IN TO SPICE UP YOUR LIFE quuminL");
        CPH.SendMessage("/ me For non-subs: <3 QMIN IN TO SPICE UP YOUR LIFE <3");

        return true;
    }//Execute
}//CPHInline