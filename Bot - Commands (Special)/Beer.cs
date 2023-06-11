using System;

/*Beer
 * 
 *  Tell the audience what I'm drinking.
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
        str_beer = CPH.GetGlobalVar<string>("beerCurrent");
        str_path = CPH.GetGlobalVar<string>("mediaRoot") + "CheersMate.mp3";
        f_vol = CPH.GetGlobalVar<float>("mediaVolume");

        CPH.PlaySound(str_path, f_vol);
        CPH.SendMessage($"/me The Q-mander is currently drinking a {str_beer}");
        CPH.SendMessage("/me Proost! (\"Cheers\" 🇳🇱 )");

        return true;
    }//Exectue()
}//CPHInline