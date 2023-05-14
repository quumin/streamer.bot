using System;
using System.IO;
using System.Collections.Generic;

/*Generate Globals - Sounds
 * 
 *  Generate the global variable for all sound interaction with viewers Actions.
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
            "Best of Both Worlds",
            "Bing Chilling",
            "KEKW",
            "Kira",
            "Torture Dance",
            "Unlurk",
            "Cozy Time",
            "EZ Clap",
            "Fuck you Data",
            "Oh Shit!",
            "OMT",
            "Thanks Data",
            "YareYare"
        };

        //Set Global
        CPH.SetGlobalVar("soundInteractActions", list_actions, true);

        return true;
    }//Execute()
}//CPHInline