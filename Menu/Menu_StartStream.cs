using System;

/*Start Stream
 * 
 *  Make a menu prompt to change settings before starting the stream.
 *  LU: 31-dec-23
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        string[] usedGlobals, usedActions;

        //Initializations
        //  Global List
        usedGlobals = new string[]
        {
            "qminMenuType",
            "qminChatState"
        };
        CPH.SetGlobalVar(usedGlobals[0], "streamSetup");
        CPH.SetGlobalVar(usedGlobals[1], "menu_on");
        //  Actions List
        usedActions = new string[]
        {
            "Menu - Post Prompt"
        };

        CPH.RunAction(usedActions[0], true);

        return true;
    }//Execute()
}//public class CPHInline