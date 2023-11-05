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
         * public List<String>[] LoadGameLibrary(int numCol = 7, char deLim = ';')
         * 
         * public static void MediaLoad(string filePath, float mediaVol)
         * 
         * public bool[] CheckCPHActions(string[] usedActions)
         * 
         */

        return true;
    }//Execute()
}//CPHInline