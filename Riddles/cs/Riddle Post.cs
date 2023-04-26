using System;
using System.IO;
using System.Collections.Generic;

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        Random rnd;
        string str_ridOut, str_ans = "";
        int int_index;
        List<string>[] list_riddles;

        //Initializations
        rnd = new Random();
        str_ridOut = str_ans = "";
        int_index = 0;
        list_riddles = new List<string>[8];
        for (int b = 0; b < list_riddles.Length; b++)
            list_riddles[b] = new List<string>();
        list_riddles = LoadRiddles();

        //Get Random Index and Set Answer
        int_index = rnd.Next(list_riddles[0].Count);
        str_ans = list_riddles[7][int_index];

        //Get Random Index, Set answer, and delete answer to prevent re-posting
        index = rnd.Next(riddle[0].Count);
        correct = riddle[7][index];
        riddle[7].RemoveAt(index);


        //Generate Response
        CPH.SendMessage("Answer the riddle to prove you're better, it can be words or a single letter:");

        //Iterate through indices
        for (int i = 0; i < 8; i++)
        {
            //Check if the string is not empty and not the answer...
            if (!string.IsNullOrEmpty(list_riddles[i][int_index]) &&
                i != 7)
            {   
                //... add it to the response.
                str_ridOut = "[Line " + (i + 1) + "] " + list_riddles[i][int_index];
            }//if

            //Check if the index is less than the maximum indices of riddle...
            if (i + 1 < 7)
            {
                //... and if the string is empty or contains punctuation already...
                if (!string.IsNullOrEmpty(list_riddles[i + 1][int_index]) &&
                    !list_riddles[i][int_index].EndsWith("?") && 
                    !list_riddles[i][int_index].EndsWith(",") && 
                    !list_riddles[i][int_index].EndsWith("!") && 
                    !list_riddles[i][int_index].EndsWith("."))
                {
                    //... add a comma separator.
                    str_ridOut += ",";
                }//if
            }//if

            //Check if the string is not empty and not the answer...
            if (!string.IsNullOrEmpty(list_riddles[i][int_index]) &&
                i != 7)
            {
                //... send the (comma-delimited) response.
                CPH.SendMessage(str_ridOut);
            }//if

            //Delete Riddle to Prevent Duplicates during stream.
            list_riddles[i].RemoveAt(int_index);
        }//for

        //Update lists in Globals to deal with deleted riddle.
        CPH.SetGlobalVar("questionsOne", list_riddles[0]);
        CPH.SetGlobalVar("questionsTwo", list_riddles[1]);
        CPH.SetGlobalVar("questionsThr", list_riddles[2]);
        CPH.SetGlobalVar("questionsFou", list_riddles[3]);
        CPH.SetGlobalVar("questionsFiv", list_riddles[4]);
        CPH.SetGlobalVar("questionsSix", list_riddles[5]);
        CPH.SetGlobalVar("questionsSev", list_riddles[6]);
        CPH.SetGlobalVar("ansWer", list_riddles[7]);
        CPH.LogInfo("『R I D D L E S』 Riddle posted succesfully - answer is: " +
            str_ans);

        //Logging
        CPH.LogVerbose("『R I D D L E S』 1: " + list_riddles[0]);
        CPH.LogVerbose("『R I D D L E S』 2: " + list_riddles[1]);
        CPH.LogVerbose("『R I D D L E S』 3: " + list_riddles[2]);
        CPH.LogVerbose("『R I D D L E S』 4: " + list_riddles[3]);
        CPH.LogVerbose("『R I D D L E S』 5: " + list_riddles[4]);
        CPH.LogVerbose("『R I D D L E S』 6: " + list_riddles[5]);
        CPH.LogVerbose("『R I D D L E S』 7: " + list_riddles[6]);
        CPH.LogVerbose("『R I D D L E S』 Answer: " + list_riddles[7]);

        //Enable Timer and Start the Game
        CPH.SetGlobalVar("correctAnswer", str_ans);
        CPH.SetGlobalVar("chatState", "riddle_on");
        CPH.SendMessage("You have 60 seconds to respond. Glory to the victor!");
        CPH.EnableTimer("RiddleTimer");
        return true;
    }//Execute()
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
        }//if

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
    }//LoadRiddles()
}//CPHInline