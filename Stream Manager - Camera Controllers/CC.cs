using System;

/*Camera Controller
 * 
 *  Adjust the location of the camera (and others) using OBS Move Plugin.
 *  LU: 4-nov-2023
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        int int_pos;
        string[] str_scene, str_filter, str_postfix;

        //Initializations
        int_pos = CPH.GetGlobalVar<int>("qminGlobalMove");
        str_scene = new string[]
        {
            "SS_KiyoPro_FancyCam",
            "SS_MidScreen"
        };
        str_filter = new string[]
        {
            "",
            ""
        };
        str_postfix = new string[]
        {
            "_Busta",
            "_WatchParty"
        };

        //Check Position
        switch (int_pos)
        {
            //	Top Left
            case 1:
                str_filter[0] = "TL";
                str_filter[1] = "BR";
                break;
            //	Top Middle
            case 2:
                str_filter[0] = "TM";
                str_filter[1] = "BR";
                break;
            //	Top Right
            case 3:
                str_filter[0] = "TR";
                str_filter[1] = "BL";
                break;
            //	Middle Right
            case 4:
                str_filter[0] = "MR";
                str_filter[1] = "BL";
                break;
            //	Bottom Right
            case 5:
                str_filter[0] = "BR";
                str_filter[1] = "TL";
                break;
            //	Bottom Middle
            case 6:
                str_filter[0] = "BM";
                str_filter[1] = "TL";
                break;
            //	Bottom Left
            case 7:
                str_filter[0] = "BL";
                str_filter[1] = "TR";
                break;
            //	Middle Left
            case 8:
                str_filter[0] = "ML";
                str_filter[1] = "TR";
                break;
            //  Other
            default:
                CPH.LogInfo("Cam Controller went out of bounds!");
                return true;
                break;
        }//switch

        //Show
        for (int i = 0; i < str_filter.Length; i++)
        {
            str_filter[i] += str_postfix[i];
            CPH.ObsShowFilter(str_scene[i], str_filter[i]);
        }//for

        //Run the Spotify checker to move Now Playing.
        CPH.RunAction("Cam Controller - Now Playing");

        return true;
    }//Execute()
}//CPHInline