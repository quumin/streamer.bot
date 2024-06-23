using System;
using System.IO;
using System.Collections.Generic;
using QminBotDLL;

/*Poll Dancer
 * 
 *  Create a poll to select what you do next based on your currently installed games.
 *  LU: 23-jun-2024
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
        List<string> pollOptions;
        string pollQuestion;
        int listIndex, durAtion, cpCost;

        //Initializations
        rnd = new Random();
        pollOptions = new List<string>();
        gamesList = QnamicLib.LoadGameLibrary();
        pollQuestion = "What should Q do next?";
        listIndex = 0;
        durAtion = 90;
        cpCost = 0;

        //Create Baseline Poll Options.
        pollOptions.Add("Keep on keepin' on.");
        pollOptions.Add("Coding!");

        //Generate Games.
        for (int i = 0; i < 3; i++)
        {
            listIndex = rnd.Next(gamesList[0].Count);
            CPH.LogInfo($"『P O L L』 Polled games #{listIndex}: {gamesList[0][listIndex]}");
            
            //If the selected index is not empty...
            if (gamesList[0][listIndex] != null)
            {
                //... then add to options and remove from the list to prevent duplicates.
                pollOptions.Add(gamesList[0][listIndex]);
                gamesList[0].RemoveAt(listIndex);
            }//if
            else
            {
                //... else decrement index by 1 to handle an empty newline.
                pollOptions.Add(gamesList[0][listIndex - 1]);
                gamesList[0].RemoveAt(listIndex - 1);
            }//else

        }//for

        CPH.TwitchPollCreate(pollQuestion, pollOptions, durAtion, cpCost);

        return true;
    }//Execute()
}//CPHInline