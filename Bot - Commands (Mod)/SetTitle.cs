using System;
using System.Collections.Generic;

/*Set Title
 * 
 *  Set the title of your stream, retaining options after parentheses.
 *  LU: 4-nov-2023
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string[] str_meta, str_special, str_delims, str_both;
        bool[] bool_splits;
        List<string> list_squad;
        string str_title, str_nbs, str_alwaysIn, str_source, str_usr;
        int int_tcl;

        //Initializations
        str_meta = new string[]
        {
            args["user"].ToString(),		//User
        	args["rawInput"].ToString(),	//Raw Input (Title)
			args["command"].ToString(),		//Command (!settitle, !setboth)
			args["currentTitle"].ToString()	//Old Title
		};
        str_special = new string[]
        {
            CPH.GetGlobalVar<string>("qminBeerCurrent"),
            CPH.GetGlobalVar<string>("qminWineCurrent")
        };
        str_delims = new string[]
        {
            " || ",							//Both
			" |bs| ",						//Backseating
			" |p| "							//Poll
		};

        //Split out Both from Raw Input
        str_both = str_meta[1].Split(new string[] { str_delims[0] }, StringSplitOptions.None);

        bool_splits = new bool[]
        {
            false,							//Backseating
			false							//Poll
		};

        list_squad = CPH.GetGlobalVar<List<string>>("qminSquadCurrent");
        str_nbs = "- No Backseating, Please quuminL";
        str_alwaysIn = " (!7tv, !discord";
        int_tcl = 140;

        //Split out the Poll Optiion
        (str_title, bool_splits[1]) = nbs_Breakdown(str_both[0], str_delims[2]);

        //Split out the Backseating Option
        (str_title, bool_splits[0]) = nbs_Breakdown(str_title, str_delims[1]);

        //Check the source of the trigger...
        switch (str_meta[2])
        {
            //	Set Title
            case "!settitle":
                //Do nothing, title will be built in a moment.
                break;
            //	Set Both
            case "!setboth":
                //If the input splits correctly...
                if (str_both.Length > 2)
                {
                    CPH.SendMessage($"/me dataHuh @{str_meta[0]} you used too many \'{str_delims[0]}\'!");
                    return true;
                }//if
                else if (str_both[0] != str_title)
                {
                    //... set the title and game accordingly.
                    CPH.SetChannelGame(str_both[1]);
                }//if
                else
                {
                    //... delimiter used incorrectly!
                    CPH.SendMessage($"/me dataHuh @{str_meta[0]} you used the wrong delimiter, try \'{str_delims[0]}\'!");
                    return true;
                }//else
                break;
            //	Other
            default:
                //Split before the basic commands and keep that.
                str_title = str_meta[3].Split(new string[] { str_alwaysIn }, StringSplitOptions.None)[0];
                if (str_meta[3].Split(new string[] { str_alwaysIn }, StringSplitOptions.None)[1].Contains(str_nbs))
                {
                    bool_splits[0] = true;
                }//if
                break;
        }//switch

        //Add in the basic commands
        str_title += str_alwaysIn;

        try
        {
            //Check if is there is a squad...
            if (list_squad.Count >= 1)
            {
                //... and add the command
                str_title += ", !squad";
            }//if
        }//try
        catch (Exception e)
        {
            //Do nothing.
        }//catch

        //Check if is there is a beer...
        if (!string.IsNullOrEmpty(str_special[0]))
        {
            //... and add the command
            str_title += ", !beer";
        }//if

        //Check if is there is a wine...
        if (!string.IsNullOrEmpty(str_special[1]))
        {
            //... and add the command
            str_title += ", !wine";
        }//if

        //Enclose basic commands
        str_title += ") ";

        //If there's enough space...
        if (str_title.Length < (int_tcl - str_nbs.Length) && bool_splits[0])
        {
            // ... add the backseating message.
            str_title += str_nbs;
        }//if

        if (bool_splits[1])
        {
            CPH.SendMessage("/me dataMask Poll's enabled, sir!");
            CPH.EnableTimer("ChangeGames");
        }//if
        else
        {
            CPH.DisableTimer("ChangeGames");
        }//else

        //Check character limit...
        if (str_title.Length < int_tcl)
        {
            //... and update Channel Title
            CPH.SetChannelTitle(str_title);
        }//if
        else
        {
            //... or title is too long!
            CPH.SendMessage("/me dataHuh that title is too long Q-mander!");
        }//if

        return true;
    }//Execute()

    (string str_trimmed, bool bool_nbs) nbs_Breakdown(string str_title, string str_delim)
    {
        //Declarations
        string[] str_full;
        string str_trimmed;
        bool bool_nbs;

        //Initializations
        str_full = str_title.Split(new string[] { str_delim }, StringSplitOptions.None);
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