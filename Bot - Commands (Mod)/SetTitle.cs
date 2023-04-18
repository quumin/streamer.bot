using System;
using System.Collections.Generic;

public class CPHInline
{
	public bool Execute()
	{
		//Declarations
		string title, beer, wine, nbs, oldTitle, alwaysIn, cmdSource, usr;
		List<string> squad = new List<string>();
		string[] both;
		int tcl;

		//Initializations
		tcl = 140;  //Twitch Character Limit 
		alwaysIn = " (!7tv, !discord";
		nbs = "- No Backseating, Please quuminL";

		// Args
		title = args["rawInput"].ToString();
		cmdSource = args["command"].ToString();
		oldTitle = args["currentTitle"].ToString();
		usr = args["user"].ToString();

		// Globals
		beer = CPH.GetGlobalVar<string>("beerCurrent");
		wine = CPH.GetGlobalVar<string>("wineCurrent");
		squad = CPH.GetGlobalVar<List<string>>("squadCurrent");

		//Check the source of the trigger...
		switch (cmdSource)
		{
			//... is it a Title only?
			case "!settitle":
				//Do nothing, title already input.
				break;
			//... is it both?
			case "!setboth":
				//Split the input.
				both = title.Split(new string[] { " || " }, StringSplitOptions.None);
				//If the input is not split...
				if (both[0] != title)
				{
					//... set the title and game accordingly.
					title = both[0];
					CPH.SetChannelGame(both[1]);
				}
				else
				{
					//... delimiter used incorrectly!
					CPH.SendMessage("dataHuh @" + usr + " you used the wrong delimiter, try \' || \'!", true);
					return true;
				}
				break;
			//... is it some other command?
			default:
				//Split before the basic commands and keep that.
				title = oldTitle.Split(new string[] { alwaysIn }, StringSplitOptions.None)[0];
				break;
		}

		//Add in the basic commands
		title += alwaysIn;

		//Check if is there is a squad...
		if (squad.Count > 0)
		{
			//... and add the command
			title += ", !squad";
		}

		//Check if is there is a beer...
		if (!string.IsNullOrEmpty(beer))
		{
			//... and add the command
			title += ", !beer";
		}

		//Check if is there is a wine...
		if (!string.IsNullOrEmpty(wine))
		{
			//... and add the command
			title += ", !wine";
		}


		//Enclose basic commands
		title += ") ";


		//If there's enough space...
		if (title.Length < (tcl - nbs.Length))
		{
			// ... add the backseating message.
			title += nbs;
		}

		//Check character limit...
		if (title.Length < tcl)
		{
			//... and update Channel Title
			CPH.SetChannelTitle(title);
		}
		else
		{
			//... or title is too long!
			CPH.TwitchAnnounce("dataHuh that title is too long Q-mander!", true, "orange");
		}

		return true;
	}
}
