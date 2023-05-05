using System;
using System.Collections.Generic;

/*Shoutout Timer
 * 
 *	When the timer runs out, allow ShoutOuts again.
 * 
 */

public class CPHInline
{
	public bool Execute()
	{
		CPH.SetGlobalVar("globalSoActive", false, false);
		CPH.SendMessage("/me dataScrooging Shoutouts are back on the menu, boys LETSGO", true);

		return true;
	}//Execute()
}//CPHInline