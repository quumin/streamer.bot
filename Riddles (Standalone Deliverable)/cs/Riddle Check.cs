using System;

/*Riddle Check
 * 
 *	Check the chat for the answer to the riddle.
 *  LU: 23-jun-2024
 *  
 */

public class CPHInline
{

    public bool Execute()
    {
        //Declarations
        string[] usedGlobals, usedTimers;
        string usrName, rawInput, correctAns;

        //Initializations
        // Global List
        usedGlobals = new string[]
        {
            "qminChatState",
            "qminRiddleCorrect"
        };
        correctAns = CPH.GetGlobalVar<string>(usedGlobals[1]);
        // Timer List
        usedTimers = new string[]
        {
            "RiddleTimer"
        };
        // SB Args
        rawInput = args["rawInput"].ToString();
        usrName = args["user"].ToString();

        //If the answer is correct...
        if (string.Compare(rawInput, correctAns, StringComparison.CurrentCultureIgnoreCase) == 0)
        {
            //... end the game.
            CPH.SendMessage("/me " + usrName + " wins! Nerdge The correct answer was: \"" + correctAns + "!\" EZ Clap");
            CPH.SetGlobalVar(usedGlobals[0], "default");
            CPH.DisableTimer(usedTimers[0]);
        }//if

        return true;
    }//Execute()
}//CPHInline