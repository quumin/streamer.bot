using System;

/*ChatChecker
 * 
 *  Check the chat based on the global state.
 *  LU: 4-nov-2023
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        string[] str_uG, str_uA;
        string str_bot, str_eamer, str_state, str_usr;

        //Initializations
        // Global List
        str_uG = new string[]
        {
            "qminChatState"
        };
        str_state = CPH.GetGlobalVar<string>(str_uG[0]);
        // Actions List
        str_uA = new string[]
        {
            "Chat - AutoShouts (Code)",
            "Riddles - Check Chat",
            "Menu - Check Chat"
        };
        // SB Args
        str_eamer = args["broadcastUserName"].ToString();
        str_usr = args["userName"].ToString();

        // Specific
        str_bot = "ltqmanderdata";

        if (str_usr.Equals(str_bot))
        {
            return true;
        }//if()

        switch (str_state)
        {
            //	Riddles
            case "riddle_on":
                CPH.RunAction(str_uA[1]);
                break;
            // 	Menu
            case "menu_on":
                CPH.RunAction(str_uA[2]);
                break;
        }//switch()

        CPH.RunAction(str_uA[0]);

        return true;
    }//Execute()
}//CPHInline