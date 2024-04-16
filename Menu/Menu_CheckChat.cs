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
        string[] usedGlobals, usedActions, menuPrompt, currentGame, promptResponse;
        string rawInput, trimmedPrompt, streamer, userName, deLim, menuType, retryMsg, promptOption, nextAction, chatState, quitOptionLettter;
        bool[] usedActionsExist;
        int promptLength;

        //Initializations
        // Global List
        usedGlobals = new string[]
        {
            "qminMenuPrompt",
            "qminMenuType",
            "qminChatState",
            "qminCurrentGame"
        };
        menuPrompt = CPH.GetGlobalVar<string[]>(usedGlobals[0]);
        menuType = CPH.GetGlobalVar<string>(usedGlobals[1]);
        currentGame = CPH.GetGlobalVar<string[]>(usedGlobals[3]);
        // Actions Used
        usedActions = new string[]
        {
            "Menu - Post Prompt",
            "Menu - Add Game to Library"
        };
        usedActionsExist = QnamicLib.CheckCPHActions(usedActions);
        // SB Args
        rawInput = args["rawInput"].ToString();
        userName = args["userName"].ToString();
        streamer = args["broadcastUserName"].ToString();
        // Specific
        deLim = ";";
        promptLength = menuPrompt.Length;
        retryMsg = "Qmander, apologies, but I did not understand...";
        promptOption = trimmedPrompt = nextAction = "";
        quitOptionLettter = menuPrompt[promptLength].Substring(4));
        chatState = "menu_on";

        //Switch responses based on type of menu
        switch (menuType)
        {
            //  Game
            case "gameType":
                promptResponse = new string[]
                {
                    "Game Type: ",
                    $"Understood sir, \'{currentGame[0]}\' has no special traits.",
                };
                break;
            //  Platform
            case "platForm":
                promptResponse = new string[]
                {
                    $"Platform: ",
                    $"Understood sir, \'{currentGame[0]}\' is not on one of these platforms.",
                };
                break;
            case "streamSetup":
                promptResponse = new string[]
                {
                    $"Stream Setup: ",
                    $"Very well, the stream will start without any special settings.",
                };
                break;
            case "menuYesNo":
                promptResponse = new string[]
                {
                    $"Yes/No: ",
                    "",
                };
                break;
            //  Error
            default:
                CPH.SendMessage("Something went wrong (Menu - Check Chat)!");
                return true;
                break;
        }//switch()

        //If the user is the streamer...
        if (userName.Equals(streamer))
        {

            //... iterate through prompts...
            for (int i = 0; i < promptLength; i++)
            {
                promptOption = menuPrompt[i].Substring(1, 1);
                //... debug text.
                CPH.SendMessage($"Test: {promptOption}");
                //... if the input is OK and not the quit option...
                if (rawInput.Equals(promptOption) && !rawInput.Equals(quitOptionLettter))
                {
                    //... give feedback on selection.
                    trimmedPrompt = menuPrompt[i].Substring(4);
                    CPH.SendMessage($"{promptResponse[0]}\'{trimmedPrompt}\' selected!");
                    //... switch action based on type of menu...
                    switch (menuType)
                    {
                        //  Game
                        case "gameType":
                            //... set 'Installed' to TRUE.
                            currentGame[3] = "TRUE";
                            //... if the input is not the final option...
                            if (i != promptLength)
                            {
                                //... set the flag for gametype to true.
                                currentGame[4 + i] = "TRUE";
                            }//if()
                            //... store to the 'qminCurrentGame' global.
                            CPH.SetGlobalVar(usedGlobals[3], currentGame);
                            //... update menu type and store the next action to be used.
                            menuType = "platForm";
                            nextAction = usedActions[0];
                            break;
                        //  Platform
                        case "platForm":
                            //... store to the 'qminCurrentGame' global.
                            currentGame[2] = $"\'{trimmedPrompt}\'";
                            CPH.SetGlobalVar(usedGlobals[3], currentGame);
                            //... update menu type and store the next action to be used.
                            menuType = "gameType";
                            nextAction = usedActions[1];
                            //... set chat state back to normal.
                            chatState = "default";
                            break;
                        case "streamSetup":
                            //To Do
                            break;
                        //  Error
                        default:
                            // Set to Default (ToBeFixed)
                            menuType = "gameType";
                            break;
                    } //switch()
                    CPH.SetGlobalVar(usedGlobals[1], menuType);
                    CPH.SetGlobalVar(usedGlobals[2], chatState);
                    CPH.RunAction(nextAction);
                    return true;
                }//if()
                else if (rawInput.Equals(quitOptionLettter))
                {
                    //... give response and set chat state back to normal.
                    chatState = "default";
                    CPH.SendMessage(promptResponse[1]);
                    CPH.SetGlobalVar(usedGlobals[2], chatState);
                    return true;
                }//if
                else
                {
                    //... re-display the prompt.
                    CPH.SendMessage(retryMsg);
                    CPH.RunAction(usedActions[0]);
                }//else if()
            }//if()
        }//for()
        return true;
    }//Execute()
} //CPHInline