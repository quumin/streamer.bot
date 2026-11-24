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
        int currentOpacity, opacityLevels;
        string obScene, obSource, postFix;

        //Initializations
        currentOpacity = CPH.GetGlobalVar<int>("qminGlobalOpacity");
        obScene = "SS_KiyoPro_FancyCam";
        postFix = "_Opacity";
        obSource = "SS_KP_MaskedCam";
        opacityLevels = 3;

        //Turn off all Opacity First
        for (int i = 1; i <= opacityLevels; i++)
        {
            CPH.ObsHideFilter(obScene, obSource, $"{i}{postFix}");
        }//for

        //If Opacify is enabled...
        if (currentOpacity != 4)
        {
            //... turn on Specific Opacity.
            CPH.ObsShowFilter(obScene, obSource, $"{currentOpacity}{postFix}");

            //Feedback
            CPH.LogInfo($"『C C』 Opacity changed to \'{currentOpacity}\' successfully!");
        }//if
        else
        {
            //Feedback
            CPH.LogInfo("『C C』 Opacity turned off successfully!");
        }//else
        return true;
    }//Execute()
}//CPHInline