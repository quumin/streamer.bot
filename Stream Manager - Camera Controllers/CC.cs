using System;

/*Camera Controller
 * 
 *  Adjust the location of the camera (and others) using OBS Move Plugin.
 *  LU: 23-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        int cameraPos;
        string[] obScene, obFilter, postFix;

        //Initializations
        cameraPos = CPH.GetGlobalVar<int>("qminGlobalMove");
        obScene = new string[]
        {
            "SS_KiyoPro_FancyCam",
            "SS_MidScreen"
        };
        obFilter = new string[]
        {
            "",
            ""
        };
        postFix = new string[]
        {
            "_Busta",
            "_WatchParty"
        };

        //Check Position
        switch (cameraPos)
        {
            //	Top Left
            case 1:
                obFilter[0] = "TL";
                obFilter[1] = "BR";
                break;
            //	Top Middle
            case 2:
                obFilter[0] = "TM";
                obFilter[1] = "BR";
                break;
            //	Top Right
            case 3:
                obFilter[0] = "TR";
                obFilter[1] = "BL";
                break;
            //	Middle Right
            case 4:
                obFilter[0] = "MR";
                obFilter[1] = "BL";
                break;
            //	Bottom Right
            case 5:
                obFilter[0] = "BR";
                obFilter[1] = "TL";
                break;
            //	Bottom Middle
            case 6:
                obFilter[0] = "BM";
                obFilter[1] = "TL";
                break;
            //	Bottom Left
            case 7:
                obFilter[0] = "BL";
                obFilter[1] = "TR";
                break;
            //	Middle Left
            case 8:
                obFilter[0] = "ML";
                obFilter[1] = "TR";
                break;
            //  Other
            default:
                CPH.LogInfo("Cam Controller went out of bounds!");
                return true;
                break;
        }//switch

        //Show
        for (int i = 0; i < obFilter.Length; i++)
        {
            obFilter[i] += postFix[i];
            CPH.ObsShowFilter(obScene[i], obFilter[i]);
        }//for

        //Run the Spotify checker to move Now Playing.
        CPH.RunAction("Cam Controller - Now Playing");

        return true;
    }//Execute()
}//CPHInline