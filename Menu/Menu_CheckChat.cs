using System;
using System.IO;
using QminBotDLL;

/*Menu - Check Chat
 * 
 *	Check the chat for the answer to the menu prompt from the streamer.
 *  LU: 31-oct-23
 * 
 */
public class CPHInline
{

    public void Init()
    {
        //Set Static Class in QnamicLib to active instance of CPH
        QnamicLib.CPH = CPH;
    }//Init()

    public bool Execute()
    {
        //Declarations
        string[] str_uG, str_au, str_prompt, str_game, str_resp;
        string str_ri, str_eamer, str_usr, str_delim, str_type;
        bool[] bool_au_ex;
        int int_count;

        //Initializations
        // Global List
        str_uG = new string[]
        {
            "qminMenuPrompt",
            "qminMenuType",
            "qminChatState",
            "qminCurrentGame"
        };
        str_prompt = CPH.GetGlobalVar<string[]>(str_uG[0]);
        str_type = CPH.GetGlobalVar<string>(str_uG[1]);
        str_game = CPH.GetGlobalVar<string[]>(str_uG[3]);
        // Actions Used
        str_au = new string[]
        {
            "Menu - Post Prompt",
            "Menu - Add Game to Library"
        };
        bool_au_ex = QnamicLib.CheckCPHActions(str_au);
        // SB Args
        str_ri = args["rawInput"].ToString();
        str_usr = args["userName"].ToString();
        str_eamer = args["broadcastUserName"].ToString();
        // Specific
        str_delim = ";";
        int_count = str_prompt.Length;

        //Switch responses based on type of menu
        switch (str_type)
        {
            //  Game
            case "gameType":
                str_resp = new string[]
                {
                    "Game Type: ",
                    $"Sir? Oh, ok... \'{str_game[0]}\' will not be added to the Library (Type).",
                    "Sorry sir, I did not understand..."
                };
                break;
            //  Platform
            case "platForm":
                str_resp = new string[]
                {
                    $"Platform: ",
                    $"Sir? Oh, ok... \'{str_game[0]}\' will not be added to the Library (Platform).",
                    "Sorry sir, I did not understand..."
                };
                break;
            //  Error
            default:
                CPH.SendMessage($"Something went wrong!");
                return true;
                break;
        } //switch()

        //If the user is the streamer...
        if (str_usr.Equals(str_eamer))
        {
            //... switch action based on type of menu.
            switch (str_type)
            {
                //  Game
                case "gameType":
                    for (int i = 0; i < int_count; i++)
                    {
                        if (str_ri.Equals($"{i + 1}"))
                        {
                            str_game[3] = "TRUE";
                            if (i != 0)
                                str_game[3 + i] = "TRUE";
                            CPH.SendMessage(str_resp[0] + $"\'{str_prompt[i].Substring(4)}\' selected!");
                            CPH.SetGlobalVar(str_uG[3], str_game);
                            str_type = "platForm";
                            CPH.SetGlobalVar(str_uG[1], str_type);
                            if (bool_au_ex[0])
                            {
                                //... run it.
                                CPH.RunAction(str_au[0]);
                            } //if
                            return true;
                        } //if
                    } //for

                    break;
                //  Platform
                case "platForm":
                    for (int i = 0; i < int_count; i++)
                    {
                        if (str_ri.Equals($"{i + 1}"))
                        {
                            str_game[2] = $"\'{str_prompt[i].Substring(4)}\'";
                            CPH.SendMessage(str_resp[0] + $"\'{str_prompt[i].Substring(4)}\' selected!");
                            CPH.SetGlobalVar(str_uG[2], "default");
                            CPH.SetGlobalVar(str_uG[3], str_game);
                            str_type = "gameType";
                            CPH.SetGlobalVar(str_uG[1], str_type);
                            CPH.RunAction(str_au[1]);
                            return true;
                        } //if
                    } //for

                    break;
                //  Error
                default:
                    CPH.SendMessage($"Something went wrong!");
                    return true;
                    break;
            } //switch()

            //... applies to all.
            if (str_ri.Equals("Q"))
            {
                CPH.SendMessage(str_resp[1]);
                CPH.SetGlobalVar(str_uG[2], "default");
                return true;
            }//if
            else if (bool_au_ex[0])
            {
                //... re-display the prompt.
                CPH.SendMessage(str_resp[2]);
                CPH.RunAction(str_au[0]);
            }//else if
        }//if

        return true;
    }//Execute()

} //CPHInline