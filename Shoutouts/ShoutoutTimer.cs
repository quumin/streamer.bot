using System;
using System.Collections.Generic;


public class CPHInline
{
	public bool Execute()
	{
		CPH.SetGlobalVar("globalSoActive", false, false);
		CPH.SendMessage("dataScrooging Shoutouts are back on the menu, boys LETSGO", true);

		return true;
	}
}
