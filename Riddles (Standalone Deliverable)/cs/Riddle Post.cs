using System;
using System.IO;
using System.Collections.Generic;

/*Riddle Post
 * 
 *  Grab & post a riddle.
 *  LU: 23-jun-2024
 *  
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        Random rnd;
        List<string>[] ristLiddles;
        string[] usedGlobals, usedTimers;
        string riddleOut, correctAns;
        int riddleIndex;

        //Initializations
        // Global List
        usedGlobals = new string[]
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
        usedTimers = new string[]
        {
            "RiddleTimer"
        };
        // Specific
        rnd = new Random();
        riddleOut = "/me ";
        correctAns = "";
        riddleIndex = 0;
        ristLiddles = new List<string>[8];
        for (int b = 0; b < ristLiddles.Length; b++)
            ristLiddles[b] = new List<string>();

        //Catch unloaded Riddles to prevent Error
        if (CPH.GetGlobalVar<List<string>>(usedGlobals[1]).Count <= 0)
        {
            CPH.LogWarn("『R I D D L E S』 Riddles were not loaded in advance!");
            CPH.RunAction("Riddles - Load File");
        }//if

        //Update lists in Globals to deal with deleted riddle.
        for (int b = 0; b < ristLiddles.Length; b++)
            ristLiddles[b] = CPH.GetGlobalVar<List<string>>(usedGlobals[b + 1]);

        //Get Random Index and Set Answer
        riddleIndex = rnd.Next(ristLiddles[0].Count);
        correctAns += ristLiddles[7][riddleIndex];

        //Generate Response
        CPH.SendMessage("/me Answer the riddle to prove you're better, it can be words or a single letter:");

        //Iterate through indices
        for (int i = 0; i < ristLiddles.Length; i++)
        {
            //Check if the string is not empty and not the answer...
            if (!string.IsNullOrEmpty(ristLiddles[i][riddleIndex]) &&
                i != 7)
            {
                //... add it to the response.
                riddleOut = "/me [Line " + (i + 1) + "] " + ristLiddles[i][riddleIndex];
            }//if

            //Check if the index is less than the maximum indices of riddle...
            if (i + 1 < 7)
            {
                //... and if the string is NOT empty and NOT punctuated...
                if (!string.IsNullOrEmpty(ristLiddles[i + 1][riddleIndex]) &&
                    !ristLiddles[i][riddleIndex].EndsWith("?") &&
                    !ristLiddles[i][riddleIndex].EndsWith(",") &&
                    !ristLiddles[i][riddleIndex].EndsWith("!") &&
                    !ristLiddles[i][riddleIndex].EndsWith("."))
                {
                    //... add a comma separator.
                    riddleOut += ",";
                }//if
            }//if

            //Check if the string is not empty and not the answer...
            if (!string.IsNullOrEmpty(ristLiddles[i][riddleIndex]) &&
                i != 7)
            {
                //... send the (comma-delimited) response.
                CPH.Wait(750);
                CPH.SendMessage(riddleOut);
            }//if

            //Delete Riddle to Prevent Duplicates during stream.
            ristLiddles[i].RemoveAt(riddleIndex);
        }//for

        //Update lists in Globals to deal with deleted riddle.
        for (int b = 0; b < ristLiddles.Length; b++)
        {
            CPH.SetGlobalVar(usedGlobals[b + 1], ristLiddles[b]);
            CPH.LogVerbose($"『R I D D L E S』 {b}: {ristLiddles[0]}");
        }//for()

        //Logging
        CPH.LogInfo($"『R I D D L E S』 Riddle posted succesfully - answer is: \'{correctAns}\'.");

        //Enable Timer and Start the Game
        CPH.SendMessage("/me You have 60 seconds to respond. Glory to the victor!");
        CPH.SetGlobalVar(usedGlobals[9], correctAns);
        CPH.SetGlobalVar(usedGlobals[0], "riddle_on");
        CPH.EnableTimer(usedTimers[0]);
        //Delay to wait for Twitch response.
        CPH.Wait(750);
        return true;
    }//Execute()

}//CPHInline