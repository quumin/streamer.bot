using System;

/*Cam Controller - Curb your Enthusiasm
 * 
 *  Adjust the camera with Curb Your Enthusiasm.
 *  LU: 4-nov-2023
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        int int_pos;
        string str_scene, str_filter, str_postfix;

        //Initializations
        int_pos = CPH.GetGlobalVar<int>("qminGlobalMove");
        str_scene = "SS_KiyoPro_FancyCam";
        str_filter = "";
        str_postfix = "_CYE";

        //Check Position
        switch (int_pos)
        {
            //	Top Left
            case 1:
            //	Top Middle
            case 2:
            //	Bottom Left
            case 7:
            //	Middle Left
            case 8:
                str_filter = "L";
                break;
            //	Top Right
            case 3:
            //	Middle Right
            case 4:
            //	Bottom Right
            case 5:
            //	Bottom Middle
            case 6:
                str_filter = "R";
                break;

        }//switch

        str_filter += str_postfix;

        //If the filter exists...
        if (str_filter != str_postfix)
        {
            //... show it.
            CPH.ObsShowFilter(str_scene, str_filter);
        }//if

        return true;
    }//Execute()
}//CPHInline