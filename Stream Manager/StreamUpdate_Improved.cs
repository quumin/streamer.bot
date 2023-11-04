using System;
using System.IO;
using System.Collections.Generic;
using QminBotDLL;

/*Stream Update
 * 
 *  An improved version of my previous handler - update the stream accordingly.
 *  LU: 04-nov-23
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
        bool[] updateType;
        string[] usedGlobals;
        string streamTitle, msgPrefix, msgOut;
        bool seriousMode;
        int waitTime;

        //Initializations
        //  Global List
        usedGlobals = new string[]
        {
            "qminSeriousMode"
        };
        //  SB Args
        streamTitle = args["status"].ToString();
        updateType = new bool[]
        {
             Convert.ToBoolean(args["gameUpdate"]),
             Convert.ToBoolean(args["statusUpdate"])
        };
        //  Specific
        msgPrefix = "/me 『SYSTEM CHECK』 ";
        msgOut = "";

        //If the title updates...
        if (updateType[1])
        {
            //... add status to the message.
            msgOut += streamTitle;
            //... log stuff.
            CPH.LogInfo($"『STATUS UPDATE』: Title update to \'{streamTitle}\'!");
        }//if()

        //If both update at the same time...
        if (updateType[0] && updateType[1])
        {
            //... add a divider.
            msgOut += " | ";
        }//if()  	

        //If the game updates...
        if (updateType[0])
        {
            //... log stuff.
            CPH.LogInfo("『MARKER』: GAME_UPDATE");
            //... run Game Handler.
            msgOut += gameHandler();
        }//if()

        //Check Serious Mode Global
        seriousMode = CPH.GetGlobalVar<bool>(usedGlobals[0]);
        CPH.LogInfo("『SERIOUS CHECK』: " + seriousMode);

        //If the game is serious...
        if (seriousMode)
        {
            //... update message to be "serious game."
            msgPrefix = "/me 『SERIOUS UPDATE』 ";
        }//if()

        //Feedback in Chat
        CPH.SendMessage($"{msgPrefix}{msgOut}");

        return true;
    }//Execute()

    public string gameHandler()
    {
        //Declarations
        List<string> soundInteractions;
        List<string>[] gamesLibrary;
        List<TwitchReward> twitchRewards;
        string[] usedGlobals, usedActions, currentGame, rewGroups, obsScenes, obsSources, gameMetadata;
        string msgOut;
        int gameId, waitTime, iterAtor;
        bool[] usedActionsExist;
        bool srsMode, inLibrary;

        //Initializations
        //  Global List
        usedGlobals = new string[]
        {
            "qminSoundInteractions",
            "qminCurrentGame",
            "qminMenuType",
            "qminChatState",
            "qminSeriousMode"
        };
        soundInteractions = CPH.GetGlobalVar<List<string>>(usedGlobals[0]);
        CPH.SetGlobalVar(usedGlobals[2], "gameType");
        //  Actions List
        usedActions = new string[]
        {
            "Menu - Post Prompt"
        };
        //  QnamicLib
        usedActionsExist = QnamicLib.CheckCPHActions(usedActions);
        gamesLibrary = QnamicLib.LoadGameLibrary();
        //  SB Args
        twitchRewards = CPH.TwitchGetRewards();
        gameMetadata = new string[]
        {
            args["gameName"].ToString(),
            args["gameBoxArt"].ToString(),
            args["oldGameName"].ToString(),
            args["oldGameBoxArt"].ToString(),
            args["gameId"].ToString()
        };
        gameId = Convert.ToInt32(args["gameId"].ToString());
        //  Specific
        rewGroups = new string[]
        {
            "Standard",
            "Standard - Sounds",
            "GS - DD2",
            "GS - PoE",
            "GS - Horror"
        };
        obsScenes = new string[]
        {
            "SS_MidScreen",
            "SS_KiyoPro_FancyCam"
        };
        obsSources = new string[]
        {
            "Old GameBox Art",
            "VM_Visualizer_Serious",
            "VM_Visualizer_Normal",
            "soPlayer",
            "New GameBox Art"
        };
        currentGame = new string[7];
        currentGame[0] = gameMetadata[0];
        currentGame[1] = gameMetadata[4];
        msgOut = "";
        waitTime = 5000;
        iterAtor = 0;
        srsMode = false;
        inLibrary = false;


        //Iterate through the Library
        foreach (string str in gamesLibrary[1])
        {
            //	... if the game is in the library...
            if (str.Equals(currentGame[1]))
            {
                inLibrary = true;
                for (int j = 2; j < currentGame.Length; j++)
                {
                    currentGame[j] = gamesLibrary[j][iterAtor];
                    CPH.LogVerbose($"『G A M E S』 \'{currentGame[0]}\' | {currentGame[j]}");
                }//for()
            }//if()
            iterAtor++;
        }//foreach()

        //CPH.SendMessage($"{sa_game[1]} | {sa_gameInfo[4]}");

        CPH.SetGlobalVar(usedGlobals[1], currentGame);

        if (inLibrary)
        {
            CPH.LogInfo($"『G A M E S』 \'{currentGame[0]}\' is already in the library.");
        }//if
        else
        {
            CPH.LogInfo($"『G A M E S』 \'{currentGame[0]}\' is not yet in the library.");
            CPH.RunAction(usedActions[0], true);
            while (!CPH.GetGlobalVar<string>(usedGlobals[3]).Equals("default"))
            {
                //Wait... need to figure out how to time this out.
            }//while()
            //.. update Variable after Processing.
            currentGame = CPH.GetGlobalVar<string[]>(usedGlobals[1]);
        }//else

        //  Store Game Id
        gameId = Convert.ToInt32(currentGame[1]);

        //Show the old game Box Art.
        CPH.ObsSetBrowserSource(obsScenes[0], obsSources[0], gameMetadata[3]);
        CPH.ObsShowSource(obsScenes[0], obsSources[0]);

        srsMode = string.Compare(currentGame[4], "TRUE") == 0;

        //CPH.SendMessage($"{b_srs}");


        //Disable Game Specific Rewards.
        //  If Serious...
        if (srsMode)
        {
            CPH.LogVerbose($"『G A M E S』 Type: {gameMetadata[0]} | Serious");
            //... Show Serious Visualizer.
            CPH.ObsShowSource(obsScenes[1], obsSources[1]);
            CPH.ObsHideSource(obsScenes[1], obsSources[2]);
            CPH.ObsHideSource(obsScenes[0], obsSources[3]);

            //... Disable Sound Actions.
            foreach (string s in soundInteractions)
            {
                CPH.DisableAction(s);
            }//foreach

            //... Disable all Rewards.
            for (int i = 0; i < rewGroups.Length; i++)
            {
                CPH.TwitchRewardGroupDisable(rewGroups[i]);
            }//for
        }//else if()
        else
        {
            //... Show Normal Visualizer.
            CPH.ObsHideSource(obsScenes[1], obsSources[1]);
            CPH.ObsShowSource(obsScenes[1], obsSources[2]);
            CPH.ObsShowSource(obsScenes[0], obsSources[3]);

            //... if Standard Rewards are disabled...
            if (!twitchRewards[0].Enabled)
            {
                //... Enable Normal Rewards.
                CPH.TwitchRewardGroupEnable(rewGroups[0]);
                CPH.TwitchRewardGroupEnable(rewGroups[1]);
                //... Enable Sound Actions.
                foreach (string s in soundInteractions)
                {
                    CPH.EnableAction(s);
                }//foreach
            }//if
        }//else

        //  If Horror...
        if (string.Compare(currentGame[5], "TRUE") == 0)
        {
            CPH.LogVerbose($"『G A M E S』 Type: {gameMetadata[0]} | Horror");
            CPH.TwitchRewardGroupEnable(rewGroups[4]);
        }//else if()
        //  If Unique...
        else if (string.Compare(currentGame[6], "TRUE") == 0)
        {
            CPH.LogVerbose($"『G A M E S』 Type: {gameMetadata[0]} | Unique");
            switch (gameId)
            {
                //	Path of Exile
                case 29307:
                    CPH.TwitchRewardGroupDisable(rewGroups[2]);
                    CPH.TwitchRewardGroupEnable(rewGroups[3]);
                    break;
                //	Darkest Dungeon II
                case 511471:
                    CPH.TwitchRewardGroupEnable(rewGroups[2]);
                    CPH.TwitchRewardGroupDisable(rewGroups[3]);
                    break;
                default:
                    CPH.LogWarn($"『G A M E S』: \'{gameMetadata[0]}\' flagged as unique with no special traits!");
                    break;
            }//switch
        }//else if()
        //  Otherwise, if not Serious, the game is Normal...
        else if (!srsMode)
        {
            CPH.LogVerbose($"『G A M E S』 Type: {gameMetadata[0]} | Normal");
            //... Disable Unique Rewards
            CPH.TwitchRewardGroupDisable(rewGroups[2]);
            CPH.TwitchRewardGroupDisable(rewGroups[3]);
            CPH.TwitchRewardGroupDisable(rewGroups[4]);
        }//else

        CPH.LogVerbose($"『G A M E S』 ID: {gameMetadata[0]} | {gameId}");

        //Set the Serious Mode global.
        CPH.SetGlobalVar(usedGlobals[4], srsMode, true);

        //Show the game box art change.
        CPH.ObsSetBrowserSource(obsScenes[0], obsSources[4], gameMetadata[1]);
        CPH.Wait(waitTime);
        CPH.ObsHideSource(obsScenes[0], obsSources[0]);
        CPH.ObsShowSource(obsScenes[0], obsSources[4]);
        CPH.Wait(waitTime);
        CPH.ObsHideSource(obsScenes[0], obsSources[4]);

        //Log stuff.
        CPH.LogInfo($"『G A M E S』: Game update from \'{gameMetadata[2]}\' to \'{gameMetadata[0]}\'!");
        msgOut = $"{gameMetadata[2]} -> {gameMetadata[0]}";

        //If OBS is streaming...
        if (CPH.ObsIsStreaming())
        {
            //	... create a marker.
            CPH.CreateStreamMarker($"CHANGE - {gameMetadata[0]}");
        }//if

        return msgOut;
    }//gameHandler()
}//public class CPHInline