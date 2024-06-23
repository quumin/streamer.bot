using System;
using System.Collections.Generic;

/*Set Title
 * 
 *  Set the title of your stream, retaining options after parentheses.
 *  LU: 21-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string[] metaInfo, specialInfo, deLim, bothOptions;
        bool[] splitFlag;
        List<string> squadCurrent;
        string streamTitle, noBackseating, alwaysIn;
        int charLimit;

        //Initializations
        metaInfo = new string[]
        {
            args["user"].ToString(),		//User
        	args["rawInput"].ToString(),	//Raw Input (Title)
			args["command"].ToString(),		//Command (!settitle, !setboth)
			args["currentTitle"].ToString()	//Old Title
		};
        specialInfo = new string[]
        {
            CPH.GetGlobalVar<string>("qminBeerCurrent"),
            CPH.GetGlobalVar<string>("qminWineCurrent")
        };
        deLim = new string[]
        {
            " || ",							//Both
			" |bs| ",						//Backseating
			" |p| "							//Poll
		};

        //Split out Both from Raw Input
        bothOptions = metaInfo[1].Split(new string[] { deLim[0] }, StringSplitOptions.None);

        splitFlag = new bool[]
        {
            false,							//Backseating
			false							//Poll
		};

        squadCurrent = CPH.GetGlobalVar<List<string>>("qminSquadCurrent");
        noBackseating = "- No Backseating, Please quuminL";
        alwaysIn = " (!7tv, !discord";
        charLimit = 140;

        //Split out the Poll Optiion
        (streamTitle, splitFlag[1]) = nbs_Breakdown(bothOptions[0], deLim[2]);

        //Split out the Backseating Option
        (streamTitle, splitFlag[0]) = nbs_Breakdown(streamTitle, deLim[1]);

        //Check the source of the trigger...
        switch (metaInfo[2])
        {
            //	Set Title
            case "!settitle":
                //Do nothing, title will be built in a moment.
                break;
            //	Set Both
            case "!setboth":
                //If the input splits correctly...
                if (bothOptions.Length > 2)
                {
                    CPH.SendMessage($"/me dataHuh @{metaInfo[0]} you used too many \'{deLim[0]}\'!");
                    return true;
                }//if
                else if (bothOptions[0] != streamTitle)
                {
                    //... set the title and game accordingly.
                    CPH.SetChannelGame(bothOptions[1]);
                }//if
                else
                {
                    //... delimiter used incorrectly!
                    CPH.SendMessage($"/me dataHuh @{metaInfo[0]} you used the wrong delimiter, try \'{deLim[0]}\'!");
                    return true;
                }//else
                break;
            //	Other
            default:
                //Split before the basic commands and keep that.
                streamTitle = metaInfo[3].Split(new string[] { alwaysIn }, StringSplitOptions.None)[0];
                if (metaInfo[3].Split(new string[] { alwaysIn }, StringSplitOptions.None)[1].Contains(noBackseating))
                {
                    splitFlag[0] = true;
                }//if
                break;
        }//switch

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

        //Check if is there is a beer...
        if (!string.IsNullOrEmpty(specialInfo[0]))
        {
            //... and add the command
            streamTitle += ", !beer";
        }//if

        //Check if is there is a wine...
        if (!string.IsNullOrEmpty(specialInfo[1]))
        {
            //... and add the command
            streamTitle += ", !wine";
        }//if

        //Enclose basic commands
        streamTitle += ") ";

        //If there's enough space...
        if (streamTitle.Length < (charLimit - noBackseating.Length) && splitFlag[0])
        {
            // ... add the backseating message.
            streamTitle += noBackseating;
        }//if

        if (splitFlag[1])
        {
            CPH.SendMessage("/me dataMask Poll's enabled, sir!");
            CPH.EnableTimer("ChangeGames");
        }//if
        else
        {
            CPH.DisableTimer("ChangeGames");
        }//else

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

    (string str_trimmed, bool bool_nbs) nbs_Breakdown(string str_title, string deLim)
    {
        //Declarations
        string[] str_full;
        string str_trimmed;
        bool bool_nbs;

        //Initializations
        str_full = str_title.Split(new string[] { deLim }, StringSplitOptions.None);
        str_trimmed = str_full[0];

        try
        {
            bool_nbs = Convert.ToBoolean(str_full[1]);
        }//try
        catch
        {
            bool_nbs = false;
        }//catch

        return (str_trimmed, bool_nbs);
    }//nbs_Breakdown(string str_title)

}//CPHInline