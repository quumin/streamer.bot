using System;
using System.Collections.Generic;

/*Shoutout Timer
 * 
 *	When the timer runs out, allow ShoutOuts again.
 *	LU: 4-nov-2023
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        CPH.SetGlobalVar("qminSoActive", false, false);
        CPH.SendMessage("/me dataScrooging Shoutouts are back on the menu, boys LETSGO", true);

        return true;
    }//Execute()
}//CPHInline