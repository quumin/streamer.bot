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
         * public List<String>[] LoadGameLibrary(int int_col = 7, char chr_delim = ';')
         * 
         * public static void MediaLoad(string str_path, float f_vol)
         * 
         * public bool[] CheckCPHActions(string[] str_au)
         * 
         */

        return true;
    }//Execute()
}//CPHInline