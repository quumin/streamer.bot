using System;
using System.IO;
using System.Collections.Generic;

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        Random rnd = new Random();
        string bld_response = "";
        string correct = "";
        int index = 0;
        List<string>[] riddle = new List<string>[8];

        //Init array
        for (int b = 0; b < riddle.Length; b++)
            riddle[b] = new List<string>();

        riddle = LoadRiddles();

        //Get Random Index, Set answer, and delete answer to prevent re-posting
        index = rnd.Next(riddle[0].Count);
        correct = riddle[7][index];

        //Generate Response
        CPH.SendMessage("Answer the riddle to prove you're better, it can be words or a single letter:");
        for (int i = 0; i < 7; i++)
        {
            if (!string.IsNullOrEmpty(riddle[i][index]))
            {
                bld_response = "[Line " + (i + 1) + "] " + riddle[i][index];
            }

            if (i + 1 < 7)
            {
                if (!string.IsNullOrEmpty(riddle[i + 1][index]) &&
                    !riddle[i][index].EndsWith("?") && 
                    !riddle[i][index].EndsWith(",") && 
                    !riddle[i][index].EndsWith("!") && 
                    !riddle[i][index].EndsWith("."))
                {
                    bld_response += ",";
                }
            }

            if (!string.IsNullOrEmpty(riddle[i][index]))
            {
                CPH.SendMessage(bld_response);
            }

            //Delete Riddle to Prevent Duplicates
            riddle[i].RemoveAt(index);
        }
        //Update deleted Lists in Globals
        CPH.SetGlobalVar("questionsOne", riddle[0]);
        CPH.SetGlobalVar("questionsTwo", riddle[1]);
        CPH.SetGlobalVar("questionsThr", riddle[2]);
        CPH.SetGlobalVar("questionsFou", riddle[3]);
        CPH.SetGlobalVar("questionsFiv", riddle[4]);
        CPH.SetGlobalVar("questionsSix", riddle[5]);
        CPH.SetGlobalVar("questionsSev", riddle[6]);
        CPH.SetGlobalVar("ansWer", riddle[7]);
        CPH.LogInfo("『R I D D L E S』 Riddle posted succesfully - answer is: " +
            correct);

        //Logging
        CPH.LogVerbose("『R I D D L E S』 1: " + riddle[0]);
        CPH.LogVerbose("『R I D D L E S』 2: " + riddle[1]);
        CPH.LogVerbose("『R I D D L E S』 3: " + riddle[2]);
        CPH.LogVerbose("『R I D D L E S』 4: " + riddle[3]);
        CPH.LogVerbose("『R I D D L E S』 5: " + riddle[4]);
        CPH.LogVerbose("『R I D D L E S』 6: " + riddle[5]);
        CPH.LogVerbose("『R I D D L E S』 7: " + riddle[6]);
        CPH.LogVerbose("『R I D D L E S』 Answer: " + riddle[7]);

        CPH.SetGlobalVar("correctAnswer", correct);
        CPH.SetGlobalVar("chatState", "riddle_on");
        CPH.SendMessage("You have 60 seconds to respond. Glory to the victor!");
        CPH.EnableTimer("RiddleTimer");
        return true;
    }
    List<String>[] LoadRiddles()
    {
        //Init array
        List<string>[] riddle = new List<string>[8];
        for (int b = 0; b < riddle.Length; b++)
            riddle[b] = new List<string>();

        //Catch unloaded Riddles to prevent Error
        if (CPH.GetGlobalVar<List<string>>("questionsOne").Count <= 0)
        {
            CPH.LogWarn("『R I D D L E S』 Riddles were not loaded in advance!");
            CPH.RunAction("Riddles - Load File");
        }

        //Question Lines
        riddle[0] = CPH.GetGlobalVar<List<string>>("questionsOne");
        riddle[1] = CPH.GetGlobalVar<List<string>>("questionsTwo");
        riddle[2] = CPH.GetGlobalVar<List<string>>("questionsThr");
        riddle[3] = CPH.GetGlobalVar<List<string>>("questionsFou");
        riddle[4] = CPH.GetGlobalVar<List<string>>("questionsFiv");
        riddle[5] = CPH.GetGlobalVar<List<string>>("questionsSix");
        riddle[6] = CPH.GetGlobalVar<List<string>>("questionsSev");

        //Answer
        riddle[7] = CPH.GetGlobalVar<List<string>>("ansWer");

        return riddle;
    }
}