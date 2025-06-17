using System;

/*Riddles - Check Chat
 * 
 *	Check the chat for the answer to the riddle.
 *  LU: 21-jul-2024
 *  
 */

public class CPHInline
{

    public bool Execute()
    {
        //Log Execution Started
        CPH.LogInfo("『RIDDLES』 \'Riddles - Check Chat\' EXECUTING...");
        //Declarations
        //  Common Variables
        string qminChatState;
        string qminRiddleCorrect;
        string[] usedTimers;
        //  Specific
        string user, rawInput, correctAns;

        //Initializations
        //  Common Variables        
        qminChatState = "qminChatState";
        qminRiddleCorrect = "qminRiddleCorrect";
        usedTimers = new string[]
        {
            "Riddles - Timer"
        };
        //  Specific
        user = args["user"].ToString();
        rawInput = args["rawInput"].ToString();
        correctAns = CPH.GetGlobalVar<string>(qminRiddleCorrect);


        //If the answer is correct...
        if (string.Compare(rawInput, correctAns, StringComparison.CurrentCultureIgnoreCase) == 0)
        {
            //... end the game.
            CPH.SendMessage($"/me {user} wins! Nerdge The correct answer was: \"{correctAns}\"! EZ Clap");
            CPH.SetGlobalVar(qminChatState, "default");
            CPH.DisableTimer(usedTimers[0]);
        }//if

    //Log Execution Ended
    qminEndAction:
        CPH.LogInfo("『RIDDLES』 \'Riddles - Check Chat\' EXECUTED!");
        return true;
    }//Execute()
}//CPHInline