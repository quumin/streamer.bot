using System;
using System.IO;
using System.Collections.Generic;

public class CPHInline
{
    public bool Execute()
    {
        //Decalarations
        List<string>[] riddle = new List<string>[8];

        //Init array
        for (int b = 0; b < riddle.Length; b++)
            riddle[b] = new List<string>();

        //Store the Globals
        CPH.SetGlobalVar("questionsOne", riddle[0]);
        CPH.SetGlobalVar("questionsTwo", riddle[1]);
        CPH.SetGlobalVar("questionsThr", riddle[2]);
        CPH.SetGlobalVar("questionsFou", riddle[3]);
        CPH.SetGlobalVar("questionsFiv", riddle[4]);
        CPH.SetGlobalVar("questionsSix", riddle[5]);
        CPH.SetGlobalVar("questionsSev", riddle[6]);
        CPH.SetGlobalVar("ansWer", riddle[7]);

        //Post Debug Info
        CPH.LogInfo("『R I D D L E S』 Riddles Cleared Successfully.");

        return true;
    }
}
