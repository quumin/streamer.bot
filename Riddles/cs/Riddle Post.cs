using System;
using System.IO;
using System.Collections.Generic;

/*Riddle Post
 * 
 *  Grab & post a riddle.
 *  LU: 31-oct-2023
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        Random rnd;
        List<string>[] list_riddles;
        string[] str_uG, str_uT;
        string str_ridOut, str_ans;
        int int_index;

        //Initializations
        // Global List
        str_uG = new string[]
        {
            "qminChatState",
            "qminRiddleLineOne",
            "qminRiddleLineTwo",
            "qminRiddleLineThree",
            "qminRiddleLineFour",
            "qminRiddleLineFive",
            "qminRiddleLineSix",
            "qminRiddleLineSeven",
            "qminRiddleAnswers",
            "qminRiddleCorrect"
        };
        // Timer List
        str_uT = new string[]
        {
            "RiddleTimer"
        };
        // Specific
        rnd = new Random();
        str_ridOut = str_ans = "/me ";
        int_index = 0;
        list_riddles = new List<string>[8];
        for (int b = 0; b < list_riddles.Length; b++)
            list_riddles[b] = new List<string>();

        //Catch unloaded Riddles to prevent Error
        if (CPH.GetGlobalVar<List<string>>(str_uG[1]).Count <= 0)
        {
            CPH.LogWarn("『R I D D L E S』 Riddles were not loaded in advance!");
            CPH.RunAction("Riddles - Load File");
        }//if

        //Update lists in Globals to deal with deleted riddle.
        for (int b = 0; b < list_riddles.Length; b++)
            list_riddles[b] = CPH.GetGlobalVar<List<string>>(str_uG[b + 1]);

        //Get Random Index and Set Answer
        int_index = rnd.Next(list_riddles[0].Count);
        str_ans += list_riddles[7][int_index];

        //Generate Response
        CPH.SendMessage("/me Answer the riddle to prove you're better, it can be words or a single letter:");

        //Iterate through indices
        for (int i = 0; i < list_riddles.Length; i++)
        {
            //Check if the string is not empty and not the answer...
            if (!string.IsNullOrEmpty(list_riddles[i][int_index]) &&
                i != 7)
            {
                //... add it to the response.
                str_ridOut = "/me [Line " + (i + 1) + "] " + list_riddles[i][int_index];
            }//if

            //Check if the index is less than the maximum indices of riddle...
            if (i + 1 < 7)
            {
                //... and if the string is NOT empty and NOT punctuated...
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
        for (int b = 0; b < list_riddles.Length; b++)
        {
            CPH.SetGlobalVar(str_uG[b + 1], list_riddles[b]);
            CPH.LogVerbose($"『R I D D L E S』 {b}: {list_riddles[0]}");
        }//for()

        //Logging
        CPH.LogInfo($"『R I D D L E S』 Riddle posted succesfully - answer is: \'{str_ans}\'.");

        //Enable Timer and Start the Game
        CPH.SetGlobalVar(str_uG[9], str_ans);
        CPH.SetGlobalVar(str_uG[0], "riddle_on");
        CPH.SendMessage("/me You have 60 seconds to respond. Glory to the victor!");
        CPH.EnableTimer(str_uT[0]);
        return true;
    }//Execute()

}//CPHInline