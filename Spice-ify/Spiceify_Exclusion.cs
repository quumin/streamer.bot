using System;
using System.Collections.Generic;

public class CPHInline
{
    public bool Execute()
    {
        string commandUsed = args["command"].ToString().ToLower();

        string input = "";
        string spotifyLinkBase = "https://open.spotify.com/track/";
        var songExclusion = CPH.GetGlobalVar<List<string>>("songExclusion", true);

        if (args.ContainsKey("rawInput"))
        {
            input = args["rawInput"].ToString();
            //inputAsInt = Convert.ToInt32(args["rawInput"].ToString());
            CPH.SendMessage(input);
        }


        //--Display each banned songs from song request
        if (commandUsed == "!excludedsongs")
        {
            int songExclusionCount = songExclusion.Count;
            CPH.SendMessage(songExclusionCount.ToString() + " Songs Excluded : ");
            for (int i = 0; i < songExclusionCount; i++)
            {
                CPH.SendMessage(i + " " + songExclusion[i]);
                CPH.Wait(1000);
            }

            CPH.SendMessage("use !songback followed by the starting number of previous results to get the song back to song request");
        }

        //--Get Back the song choosen (#) to be requested from song request
        if (commandUsed == "!songback")
        {
            if (input == "")
            {
                CPH.SendMessage("you must enter a number after the command, use !excludedSongs to checks songs listed");
            }
            else
            {
                CPH.SendMessage(" Song Request available for : " + songExclusion[Convert.ToInt32(input)]);
                songExclusion.RemoveAt(Convert.ToInt32(input));
                CPH.SetGlobalVar("songExclusion", songExclusion, true);
            }
        }

        //--Exclude the current song to be requested in future song request
        if (commandUsed == "!songout")
        {
            string currentUrl = CPH.GetGlobalVar<string>("currentUrl", true);
            string currentSong = CPH.GetGlobalVar<string>("currentSong", true);
            string currentArtist = CPH.GetGlobalVar<string>("currentArtist", true);
            string excludedSongInfos = currentArtist + " " + currentSong + " " + currentUrl;
            if (songExclusion.Contains(excludedSongInfos))
            {
                CPH.SendMessage("the song is already banned");
                return false;
            }
            else
            {
                songExclusion.Add(excludedSongInfos);
                CPH.SendMessage("Added " + currentSong + " " + currentArtist + " to banned Songs List, skipping Song");
                CPH.SetGlobalVar("songExclusion", songExclusion, true);
                CPH.RunAction("tiny SPOT TO SB - !sNext [ Skip To Next Song ]");
            }
        }

        return true;
    }
}