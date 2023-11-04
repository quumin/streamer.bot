using System;

/*Menu - Post Prompt
 * 
 *  Develop a prompt and output it to the chat, then change the chat state to watch for response from the streamer.
 *  LU: 31-oct-2023
 *  
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string str_fLine, str_eamer, str_quit, str_type, str_game;
        string[] str_prompt, str_uG;

        //Intializations
        // Global List
        str_uG = new string[]
        {
            "qminMenuPrompt",
            "qminMenuType",
            "qminChatState"
        };
        // SB Args
        str_eamer = args["broadcastUserName"].ToString();
        str_game = args["gameName"].ToString();
        // Specific
        str_quit = "[Q] Quit";

        //Try to get global qminMenuPrompt.
        try
        {
            str_type = CPH.GetGlobalVar<string>(str_uG[0]);
        }//try
        catch
        {
            // Otherwise, enforce.
            str_type = "gameType";
            CPH.SetGlobalVar(str_uG[0], str_type);
        }//catch

        //Switch prompt based on type of menu (scalable).
        switch (str_type)
        {
            //	Game
            case "gameType":
                str_prompt = new string[]
                {
                    "[1] Normal Game",
                    "[2] Serious Game",
                    "[3] Horror",
                    "[4] Unique"
                };
                str_fLine = $"What type of game is {str_game} @{str_eamer}?";
                break;
            //	Platform
            case "platForm":
                str_prompt = new string[]
                {
                    "[1] Steam",
                    "[2] Xbox Game Pass",
                    "[3] EPIC Games",
                    "[4] Nintendo Switch",
                    "[5] Other (PC)"
                };
                str_fLine = $"What platform is {str_game} on @{str_eamer}?";
                break;
            //	Error
            default:
                CPH.SendMessage("Something went wrong making the menu!");
                return true;
                break;
        }//switch()


        //Send first line.
        CPH.SendMessage(str_fLine, true);
        //Send each next line.
        for (int i = 0; i < str_prompt.Length; i++)
        {
            CPH.SendMessage(str_prompt[i], true);
        }//for()
        //Send quit line.
        CPH.SendMessage(str_quit, true);

        //Update globals qminMenuType anbd qminChatState.
        CPH.SetGlobalVar(str_uG[1], str_prompt);
        CPH.SetGlobalVar(str_uG[2], "menu_on");

        return true;
    }//Execute()
}//CPHInline