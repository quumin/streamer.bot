using System;

public class CPHInline
{
    public bool Execute()
    {

        if (CPH.ObsIsStreaming())
        {
            bool bool_serious = CPH.GetGlobalVar<bool>("seriousMode");
            string soundPath = "W:\\Streaming\\Media\\Sounds\\";
            CPH.SendMessage("marinHey Why don't you do the dishes for 10-15? peepoJoJo",
                true);
            if (!bool_serious)
            {
                CPH.PlaySound(soundPath + "Adulting.mp3", 0.15f);
            }
        }

        return true;
    }
}