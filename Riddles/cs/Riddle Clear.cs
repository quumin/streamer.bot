using System;
using System.IO;
using System.Collections.Generic;

public class CPHInline
{
    public bool Execute()
    {
        //Decalarations
        List<string>[] list_riddle;

        //Init array
        list_riddle = new List<string>[8];
        for (int b = 0; b < list_riddle.Length; b++)
            list_riddle[b] = new List<string>();

        //Store the Globals
        CPH.SetGlobalVar("questionsOne", list_riddle[0]);
        CPH.SetGlobalVar("questionsTwo", list_riddle[1]);
        CPH.SetGlobalVar("questionsThr", list_riddle[2]);
        CPH.SetGlobalVar("questionsFou", list_riddle[3]);
        CPH.SetGlobalVar("questionsFiv", list_riddle[4]);
        CPH.SetGlobalVar("questionsSix", list_riddle[5]);
        CPH.SetGlobalVar("questionsSev", list_riddle[6]);
        CPH.SetGlobalVar("ansWer", list_riddle[7]);

        //Post Debug Info
        CPH.LogInfo("『R I D D L E S』 Riddles Cleared Successfully.");

        return true;
    }//Execute()
}//CPHInline