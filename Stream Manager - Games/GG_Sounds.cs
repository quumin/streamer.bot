using System;
using System.IO;
using System.Collections.Generic;

public class CPHInline
{
	public bool Execute()
	{
		//Declarations
		List<string> list_actions;

		//Initializations
		list_actions = new List<string> {
			"Best of Both Worlds",
			"Bing Chilling",
			"Graphic Design",
			"KEKW",
			"Kira",
			"Torture Dance",
			"Unlurk",
			"EZ Clap",
			"Fuck you Data",
			"Oh No!",
			"OMT",
			"Thanks Data",
			"YareYare"
			};

		//Set Global
		CPH.SetGlobalVar("soundInteractActions", list_actions, true);

		return true;
	}//public bool Execute()
}//public class CPHInline