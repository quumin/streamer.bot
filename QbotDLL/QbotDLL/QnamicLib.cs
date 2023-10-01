using System;
using System.IO;
using System.Collections.Generic;
using Streamer.bot.Plugin.Interface;

namespace QminBotDLL
{
    public class QnamicLib
    {
        //Static to Handle CPH Methods
        public static IInlineInvokeProxy CPH;
        
        public static List<string> GameLoad()
        {
            /*Games List Load
             * 
             * Load a list of games from the GamesList.
             * 
             */

            //Declarations
            List<string> list_games;
            //Initializations
            list_games = new List<string>();

            try
            {
                //Try to find the file and read from it...
                using (var reader = new StreamReader(@".\\external_files\\GamesList.csv"))
                {
                    //Populate the List
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (!string.IsNullOrEmpty(line))
                        {
                            list_games.Add(line);
                        }//if
                    }//while

                    CPH.LogInfo("『G A M E S』 Loaded Successfully.");
                }//using
            }//try
            catch (Exception ex) when (ex is FileNotFoundException || ex is DirectoryNotFoundException)
            {
                //Catch when the directory and/or file is incorrect.
                CPH.LogWarn("『G A M E S』 Games File failed to load!");
                CPH.SendMessage("/me dataHuh The Games file could not be found, sir.");
            }//catch
            return list_games;
        }//gameLoad()
        public static void gg_Media(string str_path, float f_vol)
        {
            /*Generate Globals - Media
             * 
             *  Generate the global variables for all media.
             * 
             */

            //Set Global
            CPH.SetGlobalVar("mediaRoot", str_path, true);
            CPH.SetGlobalVar("mediaVolume", f_vol, true);
        }//gg_Media()
    }//QnamicLib
}//QminBot_DLL