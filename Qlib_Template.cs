using System;
using System.Collections.Generic;
using System.Collections;
using QminBotDLL;

public class CPHInline
{
    public void Init()
    {
        //Set Static Class in QnamicLib to active instance of CPH
        QnamicLib.CPH = CPH;
    }//Init()

    public bool Execute()
    {
        /*
         * Code here.
         */

        /*List of QnamicLib Methods:
         * 
         * public List<String> GameLoad()
         * 
         * public static void gg_Media(string str_path, float f_vol)
         * 
         * 
         * 
         */

        return true;
    }//Execute()
}//CPHInline