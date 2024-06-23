using System;

/*Beer
 * 
 *  Tell the audience what I'm drinking.
 *  LU: 21-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Initializations
        string beerInfo, filePath;
        float vol;

        //Declarations
        beerInfo = CPH.GetGlobalVar<string>("qminBeerCurrent");
        filePath = CPH.GetGlobalVar<string>("qminMediaRoot") + "CheersMate.mp3";
        vol = CPH.GetGlobalVar<float>("qminMediaVolume");

        CPH.PlaySound(filePath, vol);
        CPH.SendMessage($"/me The Q-mander is currently drinking a {beerInfo}");
        CPH.SendMessage("/me Proost! (\"Cheers\" 🇳🇱 )");

        return true;
    }//Exectue()
}//CPHInline