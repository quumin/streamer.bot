using System;
using System.Collections.Generic;

public class CPHInline
{
    public bool Execute()
    {
        var songExclusionList = CPH.GetGlobalVar<List<string>>("songExclusion", true);

        if (songExclusionList == null)
        {
            List<string> songExclusion = new List<string>();
            CPH.SetGlobalVar("songExclusion", songExclusion, true);
        }
        else
        {
            //do nothing
        }
        return true;
    }
}