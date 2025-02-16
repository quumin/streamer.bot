using System;
using System.Collections.Generic;
using System.Collections;
using QminBotDLL;

/*Games - Update Rewards
 * 
 *	Update the rewards based on the game type.
 *  LU: 22-sep-2024
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
        string qminSoundInteractions, qminCurrentGame;
        List<string> soundInteractions;
        List<TwitchReward> twitchRewards;
        //  Specific
        string[] currentGame, rewGroups;
        string gameName, standardRewards, standardSoundRewards, ddRewards, poeRewards, horroRewards, falloutRewards;
        int gameId, waitTime;
        bool isSerious, isHorror, isUnique;

        //Initializations
        //  Common Variables
        qminSoundInteractions = "qminSoundInteractions";
        soundInteractions = CPH.GetGlobalVar<List<string>>(qminSoundInteractions);
        qminCurrentGame = "qminCurrentGame";
        currentGame = CPH.GetGlobalVar<string[]>(qminCurrentGame);
        twitchRewards = CPH.TwitchGetRewards();
        //  Specific
        gameName = currentGame[0];
        gameId = Convert.ToInt32(currentGame[1]);
        isSerious = string.Compare(currentGame[4], "TRUE") == 0;
        isHorror = string.Compare(currentGame[5], "TRUE") == 0;
        isUnique = string.Compare(currentGame[6], "TRUE") == 0;
        standardRewards = "Standard";
        standardSoundRewards = "Standard - Sounds";
        ddRewards = "GS - DD2";
        poeRewards = "GS - PoE";
        horroRewards = "GS - Horror";
        falloutRewards = "GS - Fallout";
        rewGroups = new string[]
        {
            standardRewards,
            standardSoundRewards,
            ddRewards,
            poeRewards,
            horroRewards,
            falloutRewards
        };

        //  If Serious...
        if (isSerious)
        {
            CPH.LogInfo($"『G A M E S』 Type: {gameName} | Serious");
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
        }//if()
        else
        {
            //... if Standard Rewards are disabled...
            if (!twitchRewards[0].Enabled)
            {
                //... Enable Normal Rewards.
                CPH.TwitchRewardGroupEnable(standardRewards);
                CPH.TwitchRewardGroupEnable(standardSoundRewards);
                //... Enable Sound Actions.
                foreach (string s in soundInteractions)
                {
                    CPH.EnableAction(s);
                }//foreach
            }//if

            //  If Horror...
            if (isHorror)
            {
                CPH.LogInfo($"『G A M E S』 Type: {gameName} | Horror");
                CPH.TwitchRewardGroupEnable(horroRewards);
            }//else if()
             //  If Unique...
            else if (isUnique)
            {
                CPH.LogInfo($"『G A M E S』 Type: {gameName} | Unique");
                switch (gameId)
                {
                    //Fallout Fridays
                    //	Fallout
                    case 10767:
                    //	Fallout 2    
                    case 5097:
                    //	Fallout Tactics: Brotherhood of Steel
                    case 5826:
                    //	Fallout 3    
                    case 18763:
                    //	Fallout: New Vegas
                    case 23453:
                    //	Fallout 4
                    case 489776:
                    //	The Outer Worlds
                    case 510580:
                        CPH.TwitchRewardGroupDisable(ddRewards);
                        CPH.TwitchRewardGroupDisable(poeRewards);
                        CPH.TwitchRewardGroupEnable(falloutRewards);
                        break;
                    //	Path of Exile
                    case 29307:
                        CPH.TwitchRewardGroupDisable(ddRewards);
                        CPH.TwitchRewardGroupEnable(poeRewards);
                        CPH.TwitchRewardGroupDisable(falloutRewards);
                        break;
                    //	Darkest Dungeon II
                    case 511471:
                        CPH.TwitchRewardGroupEnable(ddRewards);
                        CPH.TwitchRewardGroupDisable(poeRewards);
                        CPH.TwitchRewardGroupDisable(falloutRewards);
                        break;
                    default:
                        CPH.LogWarn($"『G A M E S』: \'{gameName}\' flagged as unique with no special traits!");
                        break;
                }//switch
            }//else if()
             //  Otherwise, if not Serious, the game is Normal...
            else
            {
                CPH.LogInfo($"『G A M E S』 Type: {gameName} | Normal");
                //... Disable Unique Rewards
                CPH.TwitchRewardGroupDisable(ddRewards);
                CPH.TwitchRewardGroupDisable(poeRewards);
                CPH.TwitchRewardGroupDisable(falloutRewards);
                CPH.TwitchRewardGroupDisable(horroRewards);
            }//else()
        }//else()

        return true;
    }//Execute()
}//CPHInline