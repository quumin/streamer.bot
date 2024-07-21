using System;
using System.IO;
using System.Collections.Generic;

/*Riddles - Start
 * 
 *  Grab & post a riddle.
 *  LU: 21-jul-2024
 *  
 */

public class CPHInline
{
    public bool Execute()
    {
        //Log Execution Started
        CPH.LogInfo("『RIDDLES』 \'Riddles - Start\' EXECUTING...");
        //Declarations
        //  Common Variables
        string qminChatState;
        string qminRiddleLineOne, qminRiddleLineTwo, qminRiddleLineThree, qminRiddleLineFour, qminRiddleLineFive, qminRiddleLineSix, qminRiddleLineSeven, qminRiddleAnswers, qminRiddleCorrect;
        string[] qminRiddle;
        string usedAction, usedTimer;
        int twitchDelay;

        //  Specific
        Random rnd;
        List<string>[] riddleList;
        string riddleOut, riddleAns;
        int riddleIndex, loadEscape;

        //Initializations
        //  Common Variables        
        qminChatState = "qminChatState";
        qminRiddle = new string[]
        {
            qminRiddleLineOne = "qminRiddleLineOne",
            qminRiddleLineTwo = "qminRiddleLineTwo",
            qminRiddleLineThree = "qminRiddleLineThree",
            qminRiddleLineFour = "qminRiddleLineFour",
            qminRiddleLineFive = "qminRiddleLineFive",
            qminRiddleLineSix = "qminRiddleLineSix",
            qminRiddleLineSeven = "qminRiddleLineSeven",
            qminRiddleAnswers = "qminRiddleAnswers",
            qminRiddleCorrect = "qminRiddleCorrect"
        };
        usedAction = "Riddles - Load File";
        usedTimer = "Riddles - Timer";
        // Specific
        rnd = new Random();
        riddleList = new List<string>[8];
        for (int b = 0; b < riddleList.Length; b++)
            riddleList[b] = new List<string>();
        riddleOut = "/me ";
        riddleAns = "";
        riddleIndex = loadEscape = 0;
        twitchDelay = 750;

    //Load in the globals.
    qminLoadGlobals:
        for (int b = 0; b < riddleList.Length; b++)
        {
            riddleList[b] = CPH.GetGlobalVar<List<string>>(qminRiddle[b]);
        }//for()
        // Check if iterator is already triggered once.
        if (loadEscape > 1)
        {
            CPH.LogWarn("『RIDDLES』 Riddles failed to load! (RED_ALERT) ");
            CPH.SendMessage("/me loading failed! RED ALERT");
            CPH.Wait(twitchDelay);
            goto qminEndAction;
        }//if()
        //Iterate to prevent infinite loop.
        loadEscape++;

        //If the list is empty...
        if (riddleList[0].Count <= 0)
        {
            //... inform the user.
            CPH.LogWarn("『RIDDLES』 Riddles were not loaded in advance!");
            CPH.SendMessage("/me Riddles not Loaded! One moment...");
            CPH.Wait(twitchDelay);
            //... load the riddles.
            CPH.RunAction(usedAction);
            //... try again.
            goto qminLoadGlobals;
        }//if

        //Get Random Index and Set Answer
        riddleIndex = rnd.Next(riddleList[0].Count);
        riddleAns += riddleList[7][riddleIndex];

        //Generate Response
        CPH.SendMessage("/me Answer the riddle to prove you're better, it can be words or a single letter:");
        CPH.Wait(twitchDelay);

        //Iterate through indices
        for (int i = 0; i < riddleList.Length; i++)
        {
            //Check if the string is not empty and not the answer...
            if (!string.IsNullOrEmpty(riddleList[i][riddleIndex]) &&
                i != 7)
            {
                //... add it to the response.
                riddleOut = $"/me [Line {(i + 1)} ] {riddleList[i][riddleIndex]}";
            }//if

            //Check if the index is less than the maximum indices of riddle...
            if (i + 1 < 7)
            {
                //... and if the string is NOT empty and NOT punctuated...
                if (!string.IsNullOrEmpty(riddleList[i + 1][riddleIndex]) &&
                    !riddleList[i][riddleIndex].EndsWith("?") &&
                    !riddleList[i][riddleIndex].EndsWith(",") &&
                    !riddleList[i][riddleIndex].EndsWith("!") &&
                    !riddleList[i][riddleIndex].EndsWith("."))
                {
                    //... add a comma separator.
                    riddleOut += ",";
                }//if
            }//if

            //Check if the string is not empty and not the answer...
            if (!string.IsNullOrEmpty(riddleList[i][riddleIndex]) &&
                i != 7)
            {
                //... send the (comma-delimited) response.
                CPH.SendMessage(riddleOut);
                CPH.Wait(twitchDelay);
            }//if

            //Delete Riddle to Prevent Duplicates during stream.
            riddleList[i].RemoveAt(riddleIndex);
        }//for

        //Update lists in Globals to deal with deleted riddle.
        for (int b = 0; b < riddleList.Length; b++)
        {
            CPH.SetGlobalVar(qminRiddle[b], riddleList[b]);
            CPH.LogVerbose($"『RIDDLES』 {b}: {riddleList[0]}");
        }//for()

        //Logging
        CPH.LogInfo($"『RIDDLES』 Riddle posted succesfully - answer is: \'{riddleAns}\'.");

        //Enable Timer and Start the Game
        CPH.SendMessage("/me You have 60 seconds to respond. Glory to the victor!");
        CPH.Wait(twitchDelay);
        CPH.SetGlobalVar(qminRiddleCorrect, riddleAns);
        CPH.SetGlobalVar(qminChatState, "riddle_on");
        CPH.EnableTimer(usedTimer);

    //Log Execution Ended
    qminEndAction:
        CPH.LogInfo("『RIDDLES』 \'Riddles - Start\' EXECUTED!");
        return true;
    }//Execute()

}//CPHInline