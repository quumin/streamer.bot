using System;
using System.IO;
using System.Collections.Generic;

/*Riddle Clear
 * 
 *  Clear riddles for debugging.
 *  LU: 23-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Decalarations
        string[] usedGlobals;
        List<string>[] riddleLists;

        //Initializations
        // Global List
        usedGlobals = new string[]
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
        riddleLists = new List<string>[8];
        
        for (int b = 0; b < riddleLists.Length; b++)
        {
            riddleLists[b] = new List<string>();
            CPH.SetGlobalVar(usedGlobals[b], riddleLists[b]);
        }//for()

        //Post Debug Info
        CPH.LogInfo("『R I D D L E S』 Riddles Cleared Successfully.");

        return true;
    }//Execute()
}//CPHInline