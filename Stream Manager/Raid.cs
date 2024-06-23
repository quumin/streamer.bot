using System;

/*Raid
 * 
 *  Play me out Data!
 *  LU: 23-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string filePath, markerInfo, usrName;
        float vol;

        //Initializations
        filePath = CPH.GetGlobalVar<string>("qminMediaRoot") + "SH_Raid.mp3";
        markerInfo = "『RAID』";
        usrName = args["raidUser"].ToString();
        vol = CPH.GetGlobalVar<float>("qminMediaVolume");

        CPH.CreateStreamMarker(markerInfo);
        CPH.PlaySound(filePath, vol, false);
        CPH.SendMessage($"/me  quuminPOG Q has started a raid on {usrName}! Copy/paste the following to show some quuminL !");
        CPH.SendMessage("/ me For subs: quuminL QMIN IN TO SPICE UP YOUR LIFE quuminL");
        CPH.SendMessage("/ me For non-subs: <3 QMIN IN TO SPICE UP YOUR LIFE <3");

        return true;
    }//Execute
}//CPHInline