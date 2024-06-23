using System;

/*Deck - Yipee
 * 
 *  Grunts Birthday Party, celebrate in style.
 *  LU: 23-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string filePath, markerInfo, msgOut, mediaOut;
        float vol;

        //Initializations
        filePath = CPH.GetGlobalVar<string>("mediaRoot");
        vol = CPH.GetGlobalVar<float>("mediaVolume");
        markerInfo = "『SOUNDBOARD』 " + "Yippee!";
        msgOut = "/me !showemote Nerdge";
        mediaOut = "GruntBirthday.mp3";

        //If I'm live...
        if (CPH.ObsIsStreaming())
        {
            //... create a marker.
            CPH.CreateStreamMarker(markerInfo);
        }//if

        CPH.PlaySound(filePath + mediaOut, vol, false);
        CPH.SendMessage(msgOut);

        return true;
    }//Execute
}//CPHInline