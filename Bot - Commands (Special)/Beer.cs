using System;

/*Beer
 * 
 *  Tell the audience what I'm drinking.
 *  LU: 4-nov-2023
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Initializations
        string str_beer, str_path;
        float f_vol;

        //Declarations
        str_beer = CPH.GetGlobalVar<string>("qminBeerCurrent");
        str_path = CPH.GetGlobalVar<string>("qminMediaRoot") + "CheersMate.mp3";
        f_vol = CPH.GetGlobalVar<float>("qminMediaVolume");

        CPH.PlaySound(str_path, f_vol);
        CPH.SendMessage($"/me The Q-mander is currently drinking a {str_beer}");
        CPH.SendMessage("/me Proost! (\"Cheers\" 🇳🇱 )");

        return true;
    }//Exectue()
}//CPHInline