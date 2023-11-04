using System;
using System.Collections.Generic;

/*Update Special Commands
 * 
 *   Update beer, wine, and squad commands.
 *   LU: 4-nov-2023
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Initializations
        List<string> squadList;
        string commandType, squadAdd, globalName, actionName;

        //Declarations
        commandType = args["command"].ToString();
        squadAdd = globalName = "";
        actionName = "Special - ";

        //Select the Special Case
        switch (commandType)
        {
            // Beer
            case string s when s.Contains("beer"):
                globalName = "qminBeerCurrent";
                actionName += "Beer";
                break;
            //	Wine
            case string s when s.Contains("wine"):
                globalName = "qminWineCurrent";
                actionName += "Wine";
                break;
            //	Squad
            case string s when s.Contains("squad"):
                globalName = "qminSquadCurrent";
                actionName += "Squad";
                break;
            //	Error
            default:
                return true;
                break;
        }//switch (str_cmd)

        //Select Add or Reset
        switch (commandType)
        {
            //	Reset
            case string s when s.Contains("reset"):
                CPH.UnsetGlobalVar(globalName);
                CPH.DisableAction(actionName);
                break;
            //	Add
            case string s when s.Contains("add"):
                squadAdd = args["rawInput"].ToString();
                //If command contains squad...
                if (s.Contains("!squad"))
                {
                    //... then add to list and set list.
                    try
                    {
                        squadList = CPH.GetGlobalVar<List<string>>(globalName);
                        squadList.Add(squadAdd);
                    }//try
                    catch (Exception e)
                    {
                        squadList = new List<string>() { squadAdd };
                    }//catch
                    CPH.SetGlobalVar(globalName, squadList);
                }//if
                else
                {
                    //... else set global string.
                    CPH.SetGlobalVar(globalName, squadAdd);
                }//else
                CPH.EnableAction(actionName);
                break;
        }//switch (str_cmd)

        //Update the Title
        CPH.RunAction("Set Title");

        return true;
    }//public bool Execute()
}//public class CPHInline