using System;

public class CPHInline
{
    public bool Execute()
    {
        CPH.SetGlobalVar("chatState", "default");
        CPH.SendMessage("dataHuh Nobody won! The correct answer was \"" +
			CPH.GetGlobalVar<string>("correctAnswer") +
			",\" foolish meatbags LUL");
        return true;
    }//Execute()
}//CPHInline