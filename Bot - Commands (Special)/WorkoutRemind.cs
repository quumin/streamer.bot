using System;

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        bool bool_srs;
        string str_path;

        //Initializations
        bool_srs = CPH.GetGlobalVar<bool>("seriousMode");
        str_path = "W:\\Streaming\\Media\\Sounds\\";

        //If OBS is streaming...
        if (CPH.ObsIsStreaming())
        {
            //... send the mesage.
            CPH.SendMessage("/me marinHey Why don't you do the dishes for 10-15? peepoJoJo");
            //... if Serious Mode is disabled...
            if (!bool_srs)
            {
                //... play the sound.
                CPH.PlaySound(str_path + "Adulting.mp3", 0.15f);
            }//if
        }//if

        return true;
    }//Execute()
}//CPHInline