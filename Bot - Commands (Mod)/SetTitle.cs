using System;
using System.Collections.Generic;

/*Set Title
 * 
 *  Set the title of your stream, retaining options after parentheses.
 *  LU: 21-sep-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        //  Common Variables
        string qminBeerCurrent, qminWineCurrent, qminSquadCurrent, qminCurrentGame;
        //  Specific
        string[] currentGame, bothOptions;
        List<string> squadCurrent;
        string streamTitle, noBackseating, alwaysIn, deLim;
        string beerCurrent, wineCurrent, user, rawInput, command, currentTitle;
        int charLimit;

        //Initializations
        //  Common Variables
        qminBeerCurrent = "qminBeerCurrent";
        qminSquadCurrent = "qminSquadCurrent";
        qminWineCurrent = "qminWineCurrent";
        qminCurrentGame = "qminCurrentGame";
        beerCurrent = CPH.GetGlobalVar<string>(qminBeerCurrent);
        squadCurrent = CPH.GetGlobalVar<List<string>>(qminSquadCurrent);
        wineCurrent = CPH.GetGlobalVar<string>(qminWineCurrent);
        currentGame = CPH.GetGlobalVar<string[]>(qminCurrentGame);
        //  Specific
        user = args["user"].ToString();
        currentTitle = args["currentTitle"].ToString();
        CPH.LogVerbose("Debug: currentTitle OK");
        deLim = " || ";
        noBackseating = "- No Backseating!";
        alwaysIn = " (!7tv, !discord";
        charLimit = 140;
        streamTitle = "";

        try
        {
            //Check if it was a command...
            rawInput = args["rawInput"].ToString();
            command = args["command"].ToString();

            //Split out Both from Raw Input
            bothOptions = rawInput.Split(new string[] { deLim }, StringSplitOptions.None);
            CPH.LogVerbose("『STATUS UPDATE』 Input:");

            if (bothOptions.Length > 1)
            {
                for (int j = 0; j < bothOptions.Length - 1; j++)
                {
                    CPH.LogVerbose($"『STATUS UPDATE』 Input #{j + 1}: {bothOptions[j]}");
                }//for
            }//if
            else
            {
                CPH.LogVerbose($"『STATUS UPDATE』 Input 1: {bothOptions[0]}");
            }//else


            //Check the source of the trigger...
            switch (command)
            {
                //	Set Title
                case "!settitle":
                    streamTitle = bothOptions[0];
                    break;
                //	Set Both
                case "!setboth":
                    //If the input splits correctly...
                    if (bothOptions.Length > 2)
                    {
                        CPH.SendMessage($"/me dataHuh @{user} you used too many \'{deLim}\'!");
                        return true;
                    }//if
                    else
                    {
                        //... set the title and game accordingly.
                        streamTitle = bothOptions[0];
                        CPH.SetChannelGame(bothOptions[1]);
                        CPH.Wait(1000);
                        currentGame = CPH.GetGlobalVar<string[]>(qminCurrentGame);
                    }//if
                    break;
                //	Other
                default:
                    CPH.LogVerbose("『STATUS UPDATE』 Error setting the title!");
                    return true;
                    break;
            }//switch
        }//try
        catch (Exception e)
        {
            CPH.LogVerbose("『STATUS UPDATE』 Executing non-triggered command.");
            //Split before the basic commands and keep that.
            streamTitle = currentTitle.Split(new string[] { alwaysIn }, StringSplitOptions.None)[0];
        }//catch

        //Add in the basic commands
        streamTitle += alwaysIn;

        try
        {
            //Check if is there is a squad...
            if (squadCurrent.Count >= 1)
            {
                //... and add the command
                streamTitle += ", !squad";
            }//if
        }//try
        catch (Exception e)
        {
            //Do nothing.
        }//catch


        try
        {
            //Check if is there is a beer...
            if (!string.IsNullOrEmpty(beerCurrent))
            {
                //... and add the command
                streamTitle += ", !beer";
            }//if
        }//try
        catch (Exception e)
        {
            //Do nothing.
        }//catch

        try
        {
            //Check if is there is a wine...
            if (!string.IsNullOrEmpty(wineCurrent))
            {
                //... and add the command
                streamTitle += ", !wine";
            }//if
        }//try
        catch (Exception e)
        {
            //Do nothing.
        }//catch

        //Enclose basic commands
        streamTitle += ") ";

        //If the game has the NBS flag...
        if (string.Compare(currentGame[7], "TRUE") == 0)
        {
            // ... add the backseating message.
            streamTitle += noBackseating;
        }//if

        //Check character limit...
        if (streamTitle.Length < charLimit)
        {
            //... and update Channel Title
            CPH.SetChannelTitle(streamTitle);
        }//if
        else
        {
            //... or title is too long!
            CPH.SendMessage("/me dataHuh that title is too long Q-mander!");
        }//if

        return true;
    }//Execute()
}//CPHInline