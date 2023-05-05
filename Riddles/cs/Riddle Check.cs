using System;

/*Riddle Check
 * 
 *	Check the chat for the answer to the riddle.
 * 
 */

public class CPHInline
{

    public bool Execute()
    {
        checkAnswer(CPH.GetGlobalVar<string>("correctAnswer"));
        return true;
    }//Execute()
    void checkAnswer(string str_ans)
    {
        //Declarations
        string usr_ri, str_usr;

        //Initializations
        usr_ri = args["rawInput"].ToString();
        str_usr = args["user"].ToString();

        //If the answer is correct...
        if (string.Compare(usr_ri, str_ans, StringComparison.CurrentCultureIgnoreCase) == 0)
        {
            //... end the game.
            CPH.SendMessage("/me " + str_usr + " wins! Nerdge The correct answer was: \"" + str_ans + "!\" EZ");
            CPH.SetGlobalVar("chatState", "default");
            CPH.DisableTimer("RiddleTimer");
        }//if
    }//checkAnswer
}//CPHInline