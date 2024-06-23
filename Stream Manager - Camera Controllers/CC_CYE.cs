using System;

/*Cam Controller - Curb your Enthusiasm
 * 
 *  Adjust the camera with Curb Your Enthusiasm.
 *  LU: 23-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        int cameraPos;
        string obScene, obFilter, postFix;

        //Initializations
        cameraPos = CPH.GetGlobalVar<int>("qminGlobalMove");
        obScene = "SS_KiyoPro_FancyCam";
        obFilter = "";
        postFix = "_CYE";

        //Check Position
        switch (cameraPos)
        {
            //	Top Left
            case 1:
            //	Top Middle
            case 2:
            //	Bottom Left
            case 7:
            //	Middle Left
            case 8:
                obFilter = "L";
                break;
            //	Top Right
            case 3:
            //	Middle Right
            case 4:
            //	Bottom Right
            case 5:
            //	Bottom Middle
            case 6:
                obFilter = "R";
                break;

        }//switch

        obFilter += postFix;

        //If the filter exists...
        if (obFilter != postFix)
        {
            //... show it.
            CPH.ObsShowFilter(obScene, obFilter);
        }//if

        return true;
    }//Execute()
}//CPHInline