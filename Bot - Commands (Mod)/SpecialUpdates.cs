using System;
using System.Collections.Generic;

public class CPHInline
{
    public bool Execute()
    {
        //Initializations
        List<string> list_squad;
        string str_cmd, str_add, str_set, str_act;

        //Declarations
        list_squad = new List<string>();
        str_cmd = args["command"].ToString();
        str_add = str_set = "";
        str_act = "Special - ";

        //Select the Special Case
        switch (str_cmd)
        {
            // Beer
            case string s when s.Contains("beer"):
                str_set = "beerCurrent";
                str_act += "Beer";
                break;
            //	Wine
            case string s when s.Contains("wine"):
                str_set = "wineCurrent";
                str_act += "Wine";
                break;
            //	Squad
            case string s when s.Contains("squad"):
                str_set = "squadCurrent";
                str_act += "Squad";
                break;
            //	Error
            default:
                return true;
                break;
        }//switch (str_cmd)

        //Select Add or Reset
        switch (str_cmd)
        {
            //	Reset
            case string s when s.Contains("reset"):
                CPH.UnsetGlobalVar(str_set);
                CPH.DisableAction(str_act);
                break;
            //	Add
            case string s when s.Contains("add"):
                str_add = args["rawInput"].ToString();
                //If command contains squad...
                if (s.Contains("!squad"))
                {
                    //... then add to list and set list.
                    list_squad = CPH.GetGlobalVar<List<string>>(str_set);
                    list_squad.Add(str_add);
                    CPH.SetGlobalVar(str_set, list_squad);
                }//if (s.Contains("!squad"))
                else
                {
                    //... else set global string.
                    CPH.SetGlobalVar(str_set, str_add);
                }//else
                CPH.EnableAction(str_act);
                break;
            //	Error
            default:
                return true;
                break;
        }//switch (str_cmd)

        //Update the Title
        CPH.RunAction("Set Title");

        return true;
    }//public bool Execute()
}//public class CPHInline