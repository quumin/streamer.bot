using System;

/*Cam Controller - Opacify
 * 
 *  Adjust opacity.
 *  LU: 23-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        int currentOpacity;
        string obScene, postFix;
        string[] obSource, obFilter;

        //Initializations
        currentOpacity = CPH.GetGlobalVar<int>("qminGlobalOpacity");
        obScene = "SS_KiyoPro_FancyCam";
        postFix = "_Opacity";
        obSource = new string[]
        {
            "Camera",
            "Streamboss",
            "Chatbox"
        };
        obFilter = new string[]
        {
            "1" + postFix,
            "2" + postFix,
            "3" + postFix
        };

        //Turn off all Opacity First
        for (int i = 0; i < obSource.Length; i++)
        {
            for (int j = 0; j < obFilter.Length; j++)
            {
                CPH.ObsHideFilter(obScene, obSource[i], obFilter[j]);
            }//for
        }//for

        //If Opacify is enabled...
        if (currentOpacity != 4)
        {
            //... turn on Specific Opacity.
            for (int i = 0; i < obSource.Length; i++)
            {
                CPH.ObsShowFilter(obScene, obSource[i], obFilter[currentOpacity - 1]);
            }//for

            //Feedback
            CPH.LogInfo("『C C』 Opacity changed to \'" + obFilter[currentOpacity - 1] + "\' successfully!");
        }//if
        else
        {
            //Feedback
            CPH.LogInfo("『C C』 Opacity turned off successfully!");
        }//else
        return true;
    }//Execute()
}//CPHInline