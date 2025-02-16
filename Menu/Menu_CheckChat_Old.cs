using System;
using System.IO;
using QminBotDLL;

/*Menu - Check Chat
 * 
 *	Check the chat for the answer to the menu prompt from the streamer.
 *  LU: 28-jun-24
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
        //  Common Variables
        string qminCurrentGame, qminMenuType, qminChatState, qminBroadCaster, qminMenuPrompt;
        string[] usedActions;
        bool[] usedActionsExist;
        //  Specific
        string[] menuPrompt, promptResponse, currentGame;
        string rawInput, trimmedPrompt, streamer, userName, deLim, menuType, retryMsg, promptOption, nextAction, chatState, quitOptionLetter;

        int promptLength;

        //Initializations
        //  Common Variables
        qminCurrentGame = "qminCurrentGame";
        qminMenuType = "qminMenuType";
        qminChatState = "qminChatState";
        qminMenuPrompt = "qminMenuPrompt";
        qminBroadCaster = "qminBroadCaster";
        usedActions = new string[]
        {
            "Menu - Post Prompt",
            "Games - Add Game to Library"
        };
        usedActionsExist = QnamicLib.CheckCPHActions(usedActions);

        // Specific
        rawInput = args["rawInput"].ToString();
        userName = args["userName"].ToString();
        streamer = CPH.GetGlobalVar<string>(qminBroadCaster);
        menuPrompt = CPH.GetGlobalVar<string[]>(qminMenuPrompt);
        menuType = CPH.GetGlobalVar<string>(qminMenuType);
        currentGame = CPH.GetGlobalVar<string[]>(qminCurrentGame);
        deLim = ";";
        promptLength = menuPrompt.Length;
        retryMsg = "Qmander, apologies, but I did not understand...";
        promptOption = trimmedPrompt = nextAction = "";
        quitOptionLetter = menuPrompt[promptLength - 1].Substring(1, 1);
        chatState = "menu_on";

        //Log Stuff
        CPH.LogVerbose($"『MENU』 Streamer: {streamer}");
        CPH.LogVerbose($"『MENU』 # Prompts: {promptLength}");
        CPH.LogVerbose($"『MENU』 Quit Letter: {quitOptionLetter}");

        //Switch responses based on type of menu
        switch (menuType)
        {
            //  Game
            case "gameType":
                promptResponse = new string[]
                {
                    "Game Type",
                    $"Understood sir, \'{currentGame[0]}\' has no special traits.",
                };
                break;
            //  Platform
            case "platForm":
                promptResponse = new string[]
                {
                    "Platform",
                    $"Understood sir, \'{currentGame[0]}\' is not on one of these platforms.",
                };
                break;
            case "streamSetup":
                promptResponse = new string[]
                {
                    "Stream Setup",
                    "Very well, the stream will start without any special settings.",
                };
                break;
            case "menuYesNo":
                promptResponse = new string[]
                {
                    "Yes/No",
                    "",
                };
                break;
            //  Error
            default:
                CPH.SendMessage("Something went wrong (Menu - Check Chat)!");
                return true;
                break;
        }//switch()

        CPH.LogVerbose($"『MENU』 Menu Type: {menuType}");


        //If the user is the streamer...
        if (userName.Equals(streamer))
        {
            //... debug.
            CPH.LogVerbose($"『MENU』: {streamer} wrote \'{rawInput}\'.");
            //... iterate through prompts...
            for (int i = 0; i < promptLength; i++)
            {
                CPH.Wait(500);
                promptOption = menuPrompt[i].Substring(1, 1);
                trimmedPrompt = menuPrompt[i].Substring(4);

                //... debug text.
                CPH.LogVerbose($"『MENU』:\t{promptOption} | {trimmedPrompt}");

                //... if the input is a valid option...
                if (rawInput.Equals(promptOption, StringComparison.InvariantCultureIgnoreCase))
                {
                    //... give feedback on selection.
                    CPH.LogVerbose($"『MENU』: {promptResponse[0]} = \'{trimmedPrompt}\'");
                    CPH.SendMessage($"{promptResponse[0]}: \'{trimmedPrompt}\' selected!");
                    //... switch action based on type of menu...
                    //... if the input is the quit option...
                    if (rawInput.Equals(quitOptionLetter, StringComparison.InvariantCultureIgnoreCase))
                    {
                        //... give response and set chat state back to normal. 
                        CPH.LogVerbose($"『MENU』: Quit letter \'{quitOptionLetter}\' selected!");
                        CPH.SendMessage(promptResponse[1]);
                    }//if
                    switch (menuType)
                    {
                        //  Game
                        case "gameType":
                            //... set 'Installed' to TRUE.
                            currentGame[3] = "TRUE";
                            //... if the input is not the final option...
                            CPH.LogVerbose($"『MENU』:\tPrompt Index {i}");
                            if (i != promptLength - 1)
                            {
                                //... set the flag for the type of game to true.
                                currentGame[4 + i] = "TRUE";
                            }//if()
                            //... store to the 'qminCurrentGame' global.
                            CPH.SetGlobalVar(qminCurrentGame, currentGame);
                            //... update menu type and store the next action to be used.
                            menuType = "platForm";
                            nextAction = usedActions[0];
                            break;
                        //  Platform
                        case "platForm":
                            //... store to the 'qminCurrentGame' global.
                            currentGame[2] = $"{trimmedPrompt}";
                            CPH.SetGlobalVar(qminCurrentGame, currentGame);
                            //... update menu type and store the next action to be used.
                            menuType = "gameType";
                            //... set chat state back to normal.
                            nextAction = usedActions[1];
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
                    CPH.SetGlobalVar(qminMenuType, menuType);
                    CPH.SetGlobalVar(qminChatState, chatState);
                    CPH.LogVerbose($"『MENU』: Menu Type changed to {menuType} with {chatState} as Chat State.");
                    CPH.RunAction(nextAction);
                    //... leave for loop.
                    return true;
                }//else if (rawInput.Equals(promptOption))
                //... else the selection is invalid...
                else
                {

                }//else
            }//for()
             //... re-display the prompt if for loop is not exited.
            CPH.LogVerbose("『MENU』: Improper response!");
            CPH.SendMessage(retryMsg);
            CPH.RunAction(usedActions[0]);
        }//if()
        else
        {
            CPH.LogVerbose($"『MENU』: {userName} does not equal {streamer}.");
        }//else
        return true;
    }//Execute()
} //CPHInline