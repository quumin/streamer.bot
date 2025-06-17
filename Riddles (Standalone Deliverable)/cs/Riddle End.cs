using System;

/*Riddles - Fail and End
 * 
 *  End the game because time ran out.
 *  LU: 21-jul-2024
 *  
 */

public class CPHInline
{
    public bool Execute()
    {
        //Log Execution Started
        CPH.LogInfo("『RIDDLES』 \'Riddles - Fail and End\' EXECUTING...");

        //Declarations
        //  Common Variables
        string qminChatState;
        string qminRiddleCorrect;
        //  Specific
        string riddleAns;

        //Initializations      
        //  Common Variables        
        qminChatState = "qminChatState";
        qminRiddleCorrect = "qminRiddleCorrect";
        //  Specific
        riddleAns = CPH.GetGlobalVar<string>(qminRiddleCorrect);

        //Set chat back to default and tell them
        CPH.SetGlobalVar(qminChatState, "default");
        CPH.SendMessage($"/me dataHuh Nobody won! The correct answer was \"{riddleAns}\" foolish meatbags LULdata");

    //Log Execution Ended
    qminEndAction:
        CPH.LogInfo("『RIDDLES』 \'Riddles - Fail and End\' EXECUTED!");
        return true;
    }//Execute()
}//CPHInline