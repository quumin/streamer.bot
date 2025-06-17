using System;
using System.IO;
using System.Collections.Generic;
using Streamer.bot.Plugin.Interface;
using System.Linq;
using QminBotDLL;

/*Poll Dancer
 * 
 *  Create a poll to select what you do next based on your currently installed games.
 *  LU: 17-jun-2025
 * 
 */

public class CPHInline
{
    public void Init()
    {
        //Set Static Class in QnamicLib to active instance of CPH
        QnamicLib.CPH = CPH;
    }//Init()

    public bool Execute()
    {
        //Declarations
        Random rnd;
        List<string>[] gamesList;
        string[] currentGame;
        List<string> pollOptions;
        string pollQuestion, externalFilePath, gamesFile, gameName;
        int listIndex, durAtion, cpCost, pollCount, pollTotal;
        char deLim;

        //Initializations
        rnd = new Random();
        pollOptions = new List<string>
        {
            "Keep on keepin' on."
        };
        gamesList = new List<string>[2];
        gamesList[0] = new List<string>();
        gamesList[1] = new List<string>();
        pollQuestion = "What Q-rated game should Q do next?";
        externalFilePath = ".\\external_files\\";
        gamesFile = "Schmoopies.csv";
        listIndex = 0;
        durAtion = 90;
        cpCost = 0;
        deLim = ';';
        pollCount = 1;
        pollTotal = 5;
        currentGame = CPH.GetGlobalVar<string[]>("qminCurrentGame");
        gameName = currentGame[0];

        try
        {
            // ... to Read CSV.
            string[] lines = File.ReadAllLines($"{externalFilePath}{gamesFile}");
            //For each line after the first...
            foreach (string line in lines.Skip(1))
            {
                //... split by delimiter.
                string[] values = line.Split(deLim);
                //... add to list.
                for (int i = 0; i < gamesList.Length; i++)
                {
                    gamesList[i].Add(values[i]);
                }//for
            }//foreach
            if (gamesList[0].Count < 5)
            {
                pollTotal = gamesList.Length;
            }//if
            CPH.LogInfo($"『P O L L』 {gamesFile} loaded Successfully with a total of {gamesList[0].Count} games.");
        }//try
        catch (Exception ex) when (ex is FileNotFoundException || ex is DirectoryNotFoundException)
        {
            //... when the directory and/or file is incorrect.
            CPH.LogWarn($"『P O L L』 {gamesFile} failed to load!");
            CPH.SendMessage($"/me dataHuh The {gamesFile} could not be found, sir.");
            return true;
        }//catch
        catch (Exception ex) when (ex is IOException)
        {
            //... when the directory and/or file is incorrect.
            CPH.LogWarn($"『P O L L』 {gamesFile} was open!");
            CPH.SendMessage($"/me dataHuh Close {gamesFile} sir!");
            return true;
        }//catch

        //Generate Games.
        while (pollCount < pollTotal)
        {
            listIndex = rnd.Next(gamesList[0].Count);
            CPH.LogInfo($"『P O L L』 Polled games (Schmoopies edition!) #{listIndex}: {gamesList[0][listIndex]}");

            //If the selected index is not empty...
            if (gamesList[0][listIndex] != null && !gamesList[0][listIndex].Equals(gameName))
            {
                //... then add to options and remove from the list to prevent duplicates.
                pollOptions.Add(gamesList[0][listIndex]);
                gamesList[0].RemoveAt(listIndex);
                pollCount++;
            }//if
            else
            {
                //... else remove from list.
                gamesList[0].RemoveAt(listIndex);
            }//else
        }//for

        if (CPH.ObsIsStreaming())
        {
            CPH.TwitchPollCreate(pollQuestion, pollOptions, durAtion, cpCost);
        }//if
        return true;
    }//Execute()
}//CPHInline