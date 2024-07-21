using System;

/*Riddle End
 * 
 *  End the game.
 *  LU: 23-jun-2024
 *  
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string[] usedGlobals;
        string correctAns;

        //Initializations
        // Global List
        usedGlobals = new string[]
        {
            "qminChatState",
            "qminRiddleCorrect"
        };
        correctAns = CPH.GetGlobalVar<string>(usedGlobals[1]);
        CPH.SetGlobalVar(usedGlobals[0], "default");
        CPH.SendMessage($"/me dataHuh Nobody won! The correct answer was \"{correctAns}\" foolish meatbags LUL");
        return true;
    }//Execute()
}//CPHInline