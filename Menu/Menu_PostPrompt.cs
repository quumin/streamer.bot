using System;

/*Menu - Post Prompt
 * 
 *  Develop a prompt and output it to the chat, then change the chat state to watch for response from the streamer.
 *  LU: 06-may-2023
 *  
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string firstLine, streamer, menuType, currentGame;
        string[] menuPrompt, usedGlobals;

        //Intializations
        // Global List
        usedGlobals = new string[]
        {
            "qminMenuPrompt",
            "qminMenuType",
            "qminChatState",
            "qminBroadCaster",
            "qminCurrentGame"
        };
        // SB Args
        menuType = CPH.GetGlobalVar<string>(usedGlobals[1]);
        streamer = CPH.GetGlobalVar<string>(usedGlobals[3]);
        currentGame = CPH.GetGlobalVar<string[]>(usedGlobals[4])[0];

        //Switch prompt based on type of menu (scalable).
        switch (menuType)
        {
            //	Game
            case "gameType":
                firstLine = $"What type of game is {currentGame} @{streamer}?";
                menuPrompt = new string[]
                {
                    "[1] Serious Game",
                    "[2] Horror",
                    "[3] Unique",
                    "[Q] Normal Game"
                };
                break;
            //	Platform
            case "platForm":
                firstLine = $"What platform is {currentGame} on @{streamer}?";
                menuPrompt = new string[]
                {
                    "[1] Steam",
                    "[2] Xbox Game Pass",
                    "[3] EPIC Games",
                    "[4] Nintendo Switch",
                    "[Q] Other (PC)"
                };
                break;
            //  Stream Setup
            case "streamSetup":
                firstLine = $"Before starting, what setting(s) would you like to enable @{streamer}?";
                menuPrompt = new string[]
                {
                    "[1] Record the Stream",
                    "[2] Timed Poll",
                    "[3] Backseating Allowed",
                    "[Q] None (Normal Stream)"
                };
                break;
            //  Yes/No
            case "menuYesNo":
                firstLine = $"Are you sure @{streamer}?";
                menuPrompt = new string[]
                {
                    "[Y] Yes",
                    "[Q] No"
                };
                break;
            //	Error
            default:
                CPH.SendMessage($"dataHuh Q-Mander, you did not set the global \'{usedGlobals[1]}\' ");
                return true;
                break;
        }//switch()


        //Send first line.
        CPH.SendMessage(firstLine, true);
        //Send each next line.
        for (int i = 0; i < menuPrompt.Length; i++)
        {
            CPH.Wait(500);
            CPH.SendMessage(menuPrompt[i], true);
        }//for()

        //Update globals qminMenuType anbd qminChatState.
        CPH.SetGlobalVar(usedGlobals[0], menuPrompt);
        CPH.SetGlobalVar(usedGlobals[2], "menu_on");

        return true;
    }//Execute()
}//CPHInline