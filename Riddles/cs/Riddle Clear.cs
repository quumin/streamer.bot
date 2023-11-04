using System;
using System.IO;
using System.Collections.Generic;

/*Riddle Clear
 * 
 *  Clear riddles for debugging.
 *  LU: 30-oct-2023
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Decalarations
        string[] str_uG;
        List<string>[] list_riddle;

        //Initializations
        // Global List
        str_uG = new string[]
        {
            "qminRiddleLineOne",
            "qminRiddleLineTwo",
            "qminRiddleLineThree",
            "qminRiddleLineFour",
            "qminRiddleLineFive",
            "qminRiddleLineSix",
            "qminRiddleLineSeven",
            "qminRiddleAnswers",
        };
        // Specific
        list_riddle = new List<string>[8];
        
        for (int b = 0; b < list_riddle.Length; b++)
        {
            list_riddle[b] = new List<string>();
            CPH.SetGlobalVar(str_uG[b], list_riddle[b]);
        }//for()

        //Post Debug Info
        CPH.LogInfo("『R I D D L E S』 Riddles Cleared Successfully.");

        return true;
    }//Execute()
}//CPHInline