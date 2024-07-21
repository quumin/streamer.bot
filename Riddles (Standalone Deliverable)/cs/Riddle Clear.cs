using System;
using System.IO;
using System.Collections.Generic;

/*Riddles - Clear Globals
 * 
 *  Clear riddles for debugging.
 *  LU: 21-jul-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Log Execution Started
        CPH.LogInfo("『RIDDLES』 \'Riddles - Clear\' EXECUTING...");
        //Declarations
        //  Common Variables
        string[] qminRiddle;
        //  Specific
        List<string>[] riddleLists;

        //Initializations
        //  Common Variables     
        qminRiddle = new string[]
        {
            "qminRiddleLineOne",
            "qminRiddleLineTwo",
            "qminRiddleLineThree",
            "qminRiddleLineFour",
            "qminRiddleLineFive",
            "qminRiddleLineSix",
            "qminRiddleLineSeven",
            "qminRiddleAnswers",
            "qminRiddleCorrect"
        };
        //  Specific
        riddleLists = new List<string>[8];

        //Clear all the lists.
        for (int b = 0; b < riddleLists.Length; b++)
        {
            riddleLists[b] = new List<string>();
            CPH.SetGlobalVar(qminRiddle[b], riddleLists[b]);
        }//for()

    //Log Execution Ended
    qminEndAction:
        CPH.LogInfo("『RIDDLES』 \'Riddles - Clear\' EXECUTED!");
        return true;
    }//Execute()
}//CPHInline