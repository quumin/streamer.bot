using System;

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        int int_state;
        string str_ss, str_postfix;
        string[] str_src, str_filter;

        //Initializations
        int_state = CPH.GetGlobalVar<int>("globalOpacity");
        str_ss = "SS_KiyoPro_FancyCam";
        str_postfix = "_Opacity";
        str_src = new string[]
        {
            "Camera",
            "Streamboss",
            "Chatbox"
        };
        str_filter = new string[]
        {
            "1" + str_postfix,
            "2" + str_postfix,
            "3" + str_postfix
        };

        //Turn off all Opacity First
        for (int i = 0; i < str_src.Length; i++)
        {
            for (int j = 0; j < str_filter.Length; j++)
            {
                CPH.ObsHideFilter(str_ss, str_src[i], str_filter[j]);
            }//for
        }//for

        //If Opacify is enabled...
        if(int_state != 4)
        {
            //... turn on Specific Opacity.
            for (int i = 0; i < str_src.Length; i++)
            {
                CPH.ObsShowFilter(str_ss, str_src[i], str_filter[int_state - 1]);
            }//for
            
            //Feedback
            CPH.LogInfo("『C C』 Opacity changed to \'" + str_filter[int_state - 1] + "\' successfully!");
        }//if
        else
        {
            //Feedback
            CPH.LogInfo("『C C』 Opacity turned off successfully!");
        }//else
        return true;
    }//Execute()
}//CPHInline