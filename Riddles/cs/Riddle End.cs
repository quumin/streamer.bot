using System;

/*Riddle End
 * 
 *  End the game.
 *  LU: 4-nov-2023
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string[] str_uG;
        string str_ans;

        //Initializations
        // Global List
        str_uG = new string[]
        {
            "qminChatState",
            "qminRiddleCorrect"
        };
        str_ans = CPH.GetGlobalVar<string>(str_uG[1]);
        CPH.SetGlobalVar(str_uG[0], "default");
        CPH.SendMessage($"/me dataHuh Nobody won! The correct answer was \"{str_ans}\" foolish meatbags LUL");
        return true;
    }//Execute()
}//CPHInline