using System;
using System.IO;
using System.Collections.Generic;

/*Games - Fallout Randomize
 * 
 * Show the Pipboy and randomize which Fallout game is picked next.
 * LU: 21-sep-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        int[] falloutGameId;
        string[] currentGame, staticSounds, falloutTitles;
        string imgURL, falloutSubScene, pipboySrc, albumArtSrc, filePath, markerInfo, mediaOut, selectedTitle;
        int imgWidth, imgHeight, selectedGameId, numGames, numSounds, rndIndex, prevIndex;
        float vol;
        Random rndGame, rndSound;

        //Initializations
        currentGame = CPH.GetGlobalVar<string[]>("qminCurrentGame");
        filePath = CPH.GetGlobalVar<string>("qminMediaRoot");
        vol = CPH.GetGlobalVar<float>("qminMediaVolume");
        //Specific
        falloutGameId = new int[]
        {
            10767,      //Fallout
            5097,       //Fallout 2
            //5826,       //Fallout Tactics: Brotherhood of Steel
            18763,      //Fallout 3
            23453,      //Fallout: New Vegas
            489776,     //Fallout 4
            510580      //The Outer Worlds
        }; 
        falloutTitles = new string[]
        {
            "Fallout",
            "Fallout 2",
            //"Fallout Tactics: Brotherhood of Steel",
            "Fallout 3",
            "Fallout: New Vegas",
            "Fallout 4",
            "The Outer Worlds"
        };
        staticSounds = new string[]
        {
            ".\\Fallout\\ui_static_c_01.wav",
            ".\\Fallout\\ui_static_c_02.wav",
            ".\\Fallout\\ui_static_c_03.wav",
            ".\\Fallout\\ui_static_c_04.wav",
            ".\\Fallout\\ui_static_c_05.wav",
            ".\\Fallout\\ui_static_d_01.wav",
            ".\\Fallout\\ui_static_d_02.wav",
            ".\\Fallout\\ui_static_d_03.wav",
            ".\\Fallout\\ui_static_d_04.wav",
            ".\\Fallout\\ui_static_d_05.wav"
        };
        imgURL = "https://static-cdn.jtvnw.net/ttv-boxart/";
        imgHeight = 450;
        imgWidth = 300;
        rndGame = new Random();
        selectedGameId = rndIndex = 0;
        falloutSubScene = "SS_Fallout";
        pipboySrc = "Fallout_PipBoy";
        albumArtSrc = "Fallout_AlbumArt";
        numGames = falloutGameId.Length;
        numSounds = staticSounds.Length;
        prevIndex = -1;
        selectedTitle = "";

        CPH.ObsShowSource(falloutSubScene, pipboySrc);
        CPH.PlaySound($"{filePath}\\Fallout\\ui_vats_enter.wav", vol, true);

        for (int i = 0; i < 5; i++)
        {
            do
            {
                rndIndex = rndGame.Next(numGames);
                selectedGameId = falloutGameId[rndIndex];
                selectedTitle = falloutTitles[rndIndex];
            }
            while (selectedGameId != Int32.Parse(currentGame[1]) && prevIndex != rndIndex);
            prevIndex = rndIndex;
            rndIndex = rndGame.Next(numSounds);

            imgURL = $"https://static-cdn.jtvnw.net/ttv-boxart/{selectedGameId}_IGDB-{imgWidth}x{imgHeight}.jpg";
            CPH.Wait(1000);
            CPH.ObsSetBrowserSource(falloutSubScene, albumArtSrc, imgURL);
            CPH.Wait(1000);
            CPH.PlaySound($"{filePath}{staticSounds[rndIndex]}", vol, false);
            CPH.ObsShowSource(falloutSubScene, albumArtSrc);
            CPH.Wait(1000);
            if (i != 4)
            {
                CPH.ObsHideSource(falloutSubScene, albumArtSrc);
            }//if
        }//for

        CPH.PlaySound($"{filePath}\\Fallout\\ui_leveluptext.wav", vol, false);
        CPH.Wait(1000);
        CPH.ObsHideSource(falloutSubScene, albumArtSrc);
        CPH.Wait(1000);
        CPH.ObsHideSource(falloutSubScene, pipboySrc);
        CPH.SetGlobalVar("qminNextFalloutId", selectedGameId, true);
        CPH.SendMessage($"VaultBoyHungry The next Fallout game will be {selectedTitle}!");

        return true;
    }//Execute()
}//CPHInline