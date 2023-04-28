using System;
using System.Collections.Generic;

public class CPHInline
{
	public bool Execute()
	{
        //Declarations
        List<string> list_squad;
        string[] str_both;
        string str_title, str_beer, str_wine, str_nbs, str_oldTitle, str_alwaysIn, str_source, str_usr;
		int int_tcl;

        //Initializations
        list_squad = CPH.GetGlobalVar<List<string>>("squadCurrent");
        str_title = args["rawInput"].ToString();
        str_beer = CPH.GetGlobalVar<string>("beerCurrent");
        str_wine = CPH.GetGlobalVar<string>("wineCurrent");
        str_nbs = "- No Backseating, Please quuminL";
        str_oldTitle = args["currentTitle"].ToString();
        str_alwaysIn = " (!7tv, !discord";
		str_source = args["command"].ToString();
		str_usr = args["user"].ToString();
        int_tcl = 140;

        //Check the source of the trigger...
        switch (str_source)
		{
			//	Set Title
			case "!settitle":
				//Do nothing, title will be built in a moment.
				break;
			//	Set Both
			case "!setboth":
				//Split the input.
				str_both = str_title.Split(new string[] { " || " }, StringSplitOptions.None);
				
				//If the input splits correctly...
				if (str_both[0] != str_title)
				{
					//... set the title and game accordingly.
					str_title = str_both[0];
					CPH.SetChannelGame(str_both[1]);
				}//if
				else
				{
					//... delimiter used incorrectly!
					CPH.SendMessage("/me dataHuh @" + str_usr + " you used the wrong delimiter, try \' || \'!", true);
					return true;
				}//else
				break;
			//	Other
			default:
				//Split before the basic commands and keep that.
				str_title = str_oldTitle.Split(new string[] { str_alwaysIn }, StringSplitOptions.None)[0];
				break;
		}//switch

		//Add in the basic commands
		str_title += str_alwaysIn;

		//Check if is there is a squad...
		if (list_squad.Count > 0)
		{
			//... and add the command
			str_title += ", !squad";
		}//if

		//Check if is there is a beer...
		if (!string.IsNullOrEmpty(str_beer))
		{
			//... and add the command
			str_title += ", !beer";
		}//if

		//Check if is there is a wine...
		if (!string.IsNullOrEmpty(str_wine))
		{
			//... and add the command
			str_title += ", !wine";
		}//if

		//Enclose basic commands
		str_title += ") ";

		//If there's enough space...
		if (str_title.Length < (int_tcl - str_nbs.Length))
		{
			// ... add the backseating message.
			str_title += str_nbs;
		}//if

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
}//CPHInline