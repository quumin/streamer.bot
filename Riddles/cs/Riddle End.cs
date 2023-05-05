using System;

/*Riddle End
 * 
 *   End the game.
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        CPH.SetGlobalVar("chatState", "default");
        CPH.SendMessage("/me dataHuh Nobody won! The correct answer was \"" +
            CPH.GetGlobalVar<string>("correctAnswer") +
            ",\" foolish meatbags LUL");
        return true;
    }//Execute()
}//CPHInline