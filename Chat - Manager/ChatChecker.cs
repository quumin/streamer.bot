using System;

/*ChatChecker
 * 
 *  Check the chat based on the global state.
 *  LU: 22-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        string[] usedGlobals, usedActions;
        string botName, broadCaster, chatState, usrName;

        //Initializations
        // Global List
        usedGlobals = new string[]
        {
            "qminChatState"
        };
        chatState = CPH.GetGlobalVar<string>(usedGlobals[0]);
        // Actions List
        usedActions = new string[]
        {
            "Chat - AutoShouts (Code)",
            "Riddles - Check Chat",
            "Menu - Check Chat"
        };
        // SB Args
        broadCaster = args["broadcastUserName"].ToString();
        usrName = args["userName"].ToString();

        // Specific
        botName = "ltqmanderdata";

        if (usrName.Equals(botName))
        {
            return true;
        }//if()

        switch (chatState)
        {
            //	Riddles
            case "riddle_on":
                CPH.RunAction(usedActions[1]);
                break;
            // 	Menu
            case "menu_on":
                CPH.RunAction(usedActions[2]);
                break;
        }//switch()

        CPH.RunAction(usedActions[0]);

        return true;
    }//Execute()
}//CPHInline