using System;
using System.IO;
using System.Collections.Generic;

/*Generate Globals - Shout Outs
 * 
 *  Generate the global variable for all shoutouts.
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        List<string> list_actions;

        //Initializations
        list_actions = new List<string>
        {
            "aypoci",
            "bobotucci",
            "cactuarmike",
            "claymorefenrir",
            "earthtothien",
            "galaxy19",
            "gryze_wolf",
            "grzajabarkus",
            "harukunsama",
            "hots_for_kuku",
            "mechamayfly",
            "muhgoop",
            "ogweirdbeard",
            "owsgt",
            "sharkiemarki3",
            "thepenguinbean",
            "toothpicksforrobots",
            "whymusticryy"
        };

        //Set Global
        CPH.SetGlobalVar("autoShouts", list_actions, true);

        return true;
    }//Execute()
}//CPHInline