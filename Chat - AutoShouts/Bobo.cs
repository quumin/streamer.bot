using System;

/*Bobo
 * 
 *	Trigger when Billy is here.
 * 
 */

public class CPHInline
{
	public bool Execute()
	{
        //Declarations
        string str_path, str_media;
		bool bool_serious;
        float f_vol;

        //Initializations
        str_path = CPH.GetGlobalVar<string>("mediaRoot");
        str_media = "Whomp.mp3";
        bool_serious = CPH.GetGlobalVar<bool>("seriousMode");
        f_vol = CPH.GetGlobalVar<float>("mediaVolume");

        //Send message
        CPH.SendMessage("/me @bobotucci has been Q's best bud for 16 years. Than-Q for coming by, Q always appreciates it quuminL");
        
		//If Serious Mode is disabled...
        if (!bool_serious)
		{
			//... play the sound.
			CPH.PlaySound(str_path + str_media, f_vol, true);
		}//if

		CPH.DisableAction("bobotucci");
		return true;
	}//Execute()
}//CPHInline