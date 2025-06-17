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
        string[] menuPrompt, currentGame;
        string rawInput, trimmedPrompt, streamer, userName, deLim, menuType, retryMsg, promptOption, nextAction, chatState, quitOptionLetter;
        string selectResponse, quitResponse;
        int promptLength, selectedIndex;

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
        selectedIndex = 999;

        //If the user is the streamer...
        if (userName.Equals(streamer))
        {
            //... log Stuff.
            CPH.LogVerbose($"『MENU』 Streamer: {streamer}");
            CPH.LogVerbose($"『MENU』 # Prompts: {promptLength}");
            CPH.LogVerbose($"『MENU』 Quit Letter: {quitOptionLetter}");
            CPH.LogVerbose($"『MENU』 Menu Type: {menuType}");
            CPH.LogVerbose($"『MENU』 Check Input: {streamer} wrote \'{rawInput}\'.");

            //... switch responses based on type of menu:
            switch (menuType)
            {
                //  Game
                case "gameType":
                    selectResponse = "Game Type";
                    quitResponse = $"Understood sir, \'{currentGame[0]}\' has no special traits.";
                    break;
                //  Platform
                case "platForm":
                    selectResponse = "Platform";
                    quitResponse = $"Understood sir, \'{currentGame[0]}\' is not on one of these platforms.";
                    break;
                //  Yes/No Menu
                case "backSeating":
                    selectResponse = "Backseating";
                    quitResponse = $"If you're sure Q-mander, the peanut gallery will enjoy backseating '{currentGame[0]}' LULdata";
                    break;
                //  Stream Setup
                case "streamSetup":
                    selectResponse = "Stream Setup";
                    quitResponse = "Very well, the stream will start without any special settings.";
                    break;
                //  Yes/No Menu
                case "menuYesNo":
                    selectResponse = "Yes";
                    quitResponse = "No";
                    break;
                //  Error
                default:
                    CPH.SendMessage("Something went wrong (Menu - Check Chat)!");
                    return true;
                    break;
            }//switch()

            //... if the input is the quit option...
            if (rawInput.Equals(quitOptionLetter, StringComparison.InvariantCultureIgnoreCase))
            {
                //... give response and set chat state back to normal. 
                CPH.LogVerbose($"『MENU』 Quit: \'{quitOptionLetter}\' selected!");
                CPH.SendMessage(quitResponse);
                //... update index to account for a quit option.
                selectedIndex = -1;
            }//if
            else
            {
                //... iterate through prompts.
                for (int i = 0; i < promptLength; i++)
                {
                    promptOption = menuPrompt[i].Substring(1, 1);
                    trimmedPrompt = menuPrompt[i].Substring(4);
                    //... debug text.
                    CPH.LogVerbose($"『MENU』 Options: \t{promptOption} | {trimmedPrompt}");
                    //... if the input is a valid option...
                    if (rawInput.Equals(promptOption, StringComparison.InvariantCultureIgnoreCase))
                    {
                        //... give feedback on selection and leave for loop.
                        CPH.LogVerbose($"『MENU』 Response: {selectResponse} = \'{trimmedPrompt}\'.");
                        CPH.SendMessage($"{selectResponse}: \'{trimmedPrompt}\' selected!");
                        selectedIndex = i;
                        break;
                    }//if())
                }//for
                //... if index is not updated...
                if (selectedIndex == 999)
                {

                    //... log it.
                    CPH.LogVerbose($"『MENU』 Check input: {rawInput} not found in {menuType}!");
                    //... re-display the prompt & exit.
                    CPH.SendMessage(retryMsg);
                    CPH.RunAction(usedActions[0]);
                    return true;
                }//if()
            }//else

            //... act based on menuType:
            switch (menuType)
            {
                //  Game
                case "gameType":
                    //... set 'Installed' to TRUE.
                    currentGame[3] = "TRUE";
                    //...   if selected index is not the quit index...
                    if (selectedIndex != -1)
                    {
                        //... set the flag for the type of game to true.
                        currentGame[4 + selectedIndex] = "TRUE";
                    }//if()
                    //... store to the 'qminCurrentGame' global.
                    CPH.SetGlobalVar(qminCurrentGame, currentGame);
                    //... update menu type to 'platForm' and post the next prompt.
                    menuType = "platForm";
                    nextAction = usedActions[0];
                    break;
                //  Platform
                case "platForm":
                    //...   if selected index is not the quit index...
                    if (selectedIndex != -1)
                    {
                        //... set the platform.
                        currentGame[2] = $"{trimmedPrompt}";
                    }//if()
                    else
                    {
                        //... set the quit option platform.
                        currentGame[2] = menuPrompt[promptLength - 1].Substring(4);
                    }//else
                    //... store to the 'qminCurrentGame' global.
                    CPH.SetGlobalVar(qminCurrentGame, currentGame);
                    //... update menu type to 'gameType' and post the next prompt.
                    menuType = "backSeating";
                    nextAction = usedActions[0];
                    break;
                //  Backseating
                case "backSeating":
                    //...   if selected index is not the quit index...
                    if (selectedIndex != -1)
                    {
                        //... disable backseating.
                        currentGame[7] = "TRUE";
                    }//if
                    //... store to the 'qminCurrentGame' global.
                    CPH.SetGlobalVar(qminCurrentGame, currentGame);
                    //... update menu type to 'noMenu' and add the game to the library.
                    menuType = "noMenu";
                    nextAction = usedActions[1];
                    //... set chat state back to normal.
                    chatState = "default";
                    break;
                case "streamSetup":
                    //To Do
                    break;
                //  Error
                default:
                    menuType = "noMenu";
                    chatState = "default";
                    break;
            } //switch()
            CPH.SetGlobalVar(qminMenuType, menuType);
            CPH.SetGlobalVar(qminChatState, chatState);
            CPH.LogVerbose($"『MENU』: Menu Type changed to {menuType} with {chatState} as Chat State.");
            CPH.RunAction(nextAction);
        }//if()
        else
        {
            CPH.LogVerbose($"『MENU』 Not Streamer: {userName} does not equal {streamer}.");
        }//else
        return true;
    }//Execute()
} //CPHInline