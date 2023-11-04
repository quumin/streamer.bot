using System;

/*Riddle Check
 * 
 *	Check the chat for the answer to the riddle.
 *  LU: 30-oct-2023
 *  
 */

public class CPHInline
{

    public bool Execute()
    {
        //Declarations
        string[] str_uG, str_uT;
        string str_usr, str_ri, str_ans;

        //Initializations
        // Global List
        str_uG = new string[]
        {
            "qminChatState",
            "qminRiddleCorrect"
        };
        str_ans = CPH.GetGlobalVar<string>(str_uG[1]);
        // Timer List
        str_uT = new string[]
        {
            "RiddleTimer"
        };
        // SB Args
        str_ri = args["rawInput"].ToString();
        str_usr = args["user"].ToString();

        //If the answer is correct...
        if (string.Compare(str_ri, str_ans, StringComparison.CurrentCultureIgnoreCase) == 0)
        {
            //... end the game.
            CPH.SendMessage("/me " + str_usr + " wins! Nerdge The correct answer was: \"" + str_ans + "!\" EZ Clap");
            CPH.SetGlobalVar(str_uG[0], "default");
            CPH.DisableTimer(str_uT[0]);
        }//if

        return true;
    }//Execute()
}//CPHInline