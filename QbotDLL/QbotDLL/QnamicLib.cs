using System;
using System.IO;
using System.Collections.Generic;
using Streamer.bot.Plugin.Interface;
using System.Linq;

namespace QminBotDLL
{
    public class QnamicLib
    {
        //Static to Handle CPH Methods
        public static IInlineInvokeProxy CPH;

        public static List<String>[] LoadGameLibrary(int colNum = 7, char deLim = ';')
        {
            //Declarations
            List<string>[] gamesList;
            string externalFilePath, gamesFile;

            //Initializations
            gamesList = new List<string>[colNum];
            for (int i = 0; i < gamesList.Length; i++)
            {
                /*Games Library
                 *	0 - Game
                 *	1 - ID
                 *	2 - Platform
                 *	3 - Installed
                 *	4 - Serious
                 *	5 - Horror
                 *	6 - Special
                 */
                gamesList[i] = new List<string>();
            }//for
            externalFilePath = @".\\external_files\\";
            gamesFile = "GamesList.csv";

            try
            {
                // ... to Read CSV.
                string[] lines = File.ReadAllLines($"{externalFilePath}{gamesFile}");
                //For each line after the first...
                foreach (string line in lines.Skip(1))
                {
                    //... split by delimiter.
                    string[] values = line.Split(deLim);
                    //... add to list.
                    for (int i = 0; i < gamesList.Length; i++)
                    {
                        gamesList[i].Add(values[i]);
                    }//for
                }//foreach
                CPH.LogInfo("『L I B』 Loaded Successfully.");
            }//try
            catch (Exception ex) when (ex is FileNotFoundException || ex is DirectoryNotFoundException)
            {
                //... when the directory and/or file is incorrect.
                CPH.LogWarn("『L I B』 Games File failed to load!");
                CPH.SendMessage("/me dataHuh The Games file could not be found, sir.");
            }//catch

            return gamesList;
        }//LoadGameLibrary()
        public static void MediaLoad(string externalFilePath, float mediaVol)
        {
            /*Generate Globals - Media
             * 
             *  Generate the global variables for all media.
             * 
             */

            //Set Global
            CPH.SetGlobalVar("qminMediaRoot", externalFilePath, true);
            CPH.SetGlobalVar("qminMediaVolume", mediaVol, true);
        }//MediaLoad()

        public static bool[] CheckCPHActions(string[] actionsUsed)
        {
            /*Check CPH Action
             * 
             *	Checks if the actions used (au) exist (ex) in StreamerBot, logs if it doesn't.
             * 
             */
            //Declarations
            int countActions;
            bool[] actionsExist;
            //Initializations
            countActions = actionsUsed.Length;
            actionsExist = new bool[countActions];
            //For every index in the actions used array...
            for (int i = 0; i < countActions; i++)
            {
                //... check if the action exists.
                actionsExist[i] = CPH.ActionExists(actionsUsed[i]);
                //... if it doesn't...
                if (!actionsExist[i])
                {
                    //... Log an error.
                    CPH.LogError($"『A C T』: \'{actionsUsed[i]}\' does NOT exist!");
                } //if
                else
                {
                    //... Log verbose.
                    CPH.LogVerbose($"『A C T』: \'{actionsUsed[i]}\' exists.");
                } //else
            } //for

            return actionsExist;
        } //checkAction()
    }//QnamicLib
}//QminBot_DLL