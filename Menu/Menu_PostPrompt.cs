using System;

/*Menu - Post Prompt
 * 
 *  Develop a prompt and output it to the chat, then change the chat state to watch for response from the streamer.
 *  LU: 29-jun-2024
 *  
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        //  Common Variables
        string qminCurrentGame, qminMenuType, qminChatState, qminBroadCaster, qminMenuPrompt;
        //Specific
        string[] menuPrompt;
        string firstLine, streamer, menuType, currentGame;
        int msgWaitTime;

        //Intializations
        //  Common Variables
        qminCurrentGame = "qminCurrentGame";
        qminMenuType = "qminMenuType";
        qminChatState = "qminChatState";
        qminMenuPrompt = "qminMenuPrompt";
        qminBroadCaster = "qminBroadCaster";
        //  Specific
        firstLine = "/me ";
        menuType = CPH.GetGlobalVar<string>(qminMenuType);
        streamer = args["broadcastUserName"].ToString();
        CPH.SetGlobalVar(qminBroadCaster, streamer);
        currentGame = CPH.GetGlobalVar<string[]>(qminCurrentGame)[0];
        msgWaitTime = 750;

        //Switch prompt based on type of menu (scalable).
        switch (menuType)
        {
            //	Game
            case "gameType":
                firstLine += $"What type of game is {currentGame}, @{streamer}?";
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
                firstLine += $"What platform is {currentGame} on, @{streamer}?";
                menuPrompt = new string[]
                {
                    "[1] Steam",
                    "[2] Xbox Game Pass",
                    "[3] EPIC Games",
                    "[4] Nintendo Switch",
                    "[Q] Other (PC)"
                };
                break;
            //  Backseating
            case "backSeating":
                firstLine += $"Is backseating allowed while playing {currentGame}, @{streamer}?"; ;
                menuPrompt = new string[]
                {
                    "[N] Backseating not allowed.",
                    "[Y] Backseating allowed."
                };
                break;
            //  Stream Setup
            case "streamSetup":
                firstLine += $"Before starting, what setting(s) would you like to enable, @{streamer}?";
                menuPrompt = new string[]
                {
                    "[1] Adult Timer",
                    "[2] Timed Poll",
                    "[3] Record the Stream",
                    "[Q] None (Normal Stream)"
                };
                break;
            //  Yes/No
            case "menuYesNo":
                firstLine += $"Are you sure, @{streamer}?";
                menuPrompt = new string[]
                {
                    "[Y] Yes",
                    "[N] No"
                };
                break;
            case "noMenu":
                CPH.LogVerbose($"『MENU』 Global not set '{qminMenuType}' intentionally.");
                return true;
            //	Error
            default:
                CPH.SendMessage($"dataHuh Q-Mander, you did not set the global \'{qminMenuType}\' to a known menu protocol!");
                return true;
                break;
        }//switch()

        //Send first line.
        CPH.SendMessage(firstLine, true);
        CPH.Wait(msgWaitTime);
        //Send each next line.
        for (int i = 0; i < menuPrompt.Length; i++)
        {
            CPH.SendMessage(menuPrompt[i], true);
            CPH.Wait(msgWaitTime);
        }//for()

        //Update globals qminMenuType anbd qminChatState.
        CPH.SetGlobalVar(qminMenuPrompt, menuPrompt);
        CPH.SetGlobalVar(qminChatState, "menu_on");

        return true;
    }//Execute()
}//CPHInline