using System;
using System.IO;
using System.Collections.Generic;

/*Riddles - Load File
 * 
 *  Load riddles from ./external_files/riddles.csv.
 *  LU: 21-jul-2024
 *
 */

public class CPHInline
{
    public bool Execute()
    {
        //Log Execution Started
        CPH.LogInfo("『RIDDLES』 \'Riddles - Load File\' EXECUTING...");
        //Declarations
        //  Common Variables
        string[] qminRiddle;
        //  Specific
        List<string>[] riddleLists;
        string msgOut;
        int totalLines;

        //Initializations
        //  Common Variables
        qminRiddle = new string[]
        {
            "qminRiddleLineOne",
            "qminRiddleLineTwo",
            "qminRiddleLineThree",
            "qminRiddleLineFour",
            "qminRiddleLineFive",
            "qminRiddleLineSix",
            "qminRiddleLineSeven",
            "qminRiddleAnswers"
        };
        //  Specific
        riddleLists = new List<string>[8];
        for (int i = 0; i < riddleLists.Length; i++)
            riddleLists[i] = new List<string>();
        msgOut = "/me ";
        totalLines = 0;

        try
        {
            //Send message
            CPH.SendMessage($"{msgOut}LETHIMCOOK Loading riddles...");
            using (var reader = new StreamReader(@".\\external_files\\riddles.csv"))
            {
                //Try to find the file
                //Populate the Lists
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    for (int i = 0; i < riddleLists.Length; i++)
                    {
                        riddleLists[i].Add(values[i]);
                        CPH.SetGlobalVar(qminRiddle[i], riddleLists[i]);
                    }//for()
                    totalLines++;
                }//while()

                //Feedback
                CPH.LogInfo($"『RIDDLES』 All {totalLines} Riddles Loaded Successfully.");
                msgOut += "Riddles loaded successfully Q-Mander dataMask";
            }//using
        }//try
        catch (Exception ex) when (ex is FileNotFoundException || ex is DirectoryNotFoundException)
        {
            //Catch when the directory and/or file is incorrect.
            CPH.LogWarn("『RIDDLES』 Riddle file failed to load! Is the directory correctly set?");
            msgOut += "dataHuh The Riddles file could not be found, sir.";
        }//catch

        //Send message
        CPH.SendMessage(msgOut);

    //Log Execution Ended
    qminEndAction:
        CPH.LogInfo("『RIDDLES』 \'Riddles - Load File\' EXECUTED!");
        return true;
    }//Execute()
}//CPHInline