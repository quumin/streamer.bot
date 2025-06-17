using System;

/*Camera Controller - Now Playing
 * 
 *	Check if Spotify is playing and update the camera and/or alert text based on this.
 *  LU: 23-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        int cameraPos;
        string[] obFilter, postFix;
        string currentScene, camScene, npScene;
        bool nowPlaying, cameraPresent;

        //Initializations
        cameraPos = CPH.GetGlobalVar<int>("qminGlobalMove");
        nowPlaying = CPH.GetGlobalVar<bool>("qminNowPlayingBool");
        currentScene = CPH.ObsGetCurrentScene();
        camScene = "SS_KiyoPro_FancyCam";
        npScene = "SS_NowPlaying";
        cameraPresent = false;


        switch (currentScene)
        {
            case "Starter":
            case "Chat":
            case "Be Right Back":
            case "Ender":
                cameraPresent = false;
                break;
            default:
                cameraPresent = true;
                break;
        }//switch

        obFilter = new string[]
        {
            "",
            ""
        };
        postFix = new string[]
        {
            "_NP",
            ""
        };

        if (cameraPresent)
        {
            currentScene = camScene;
            //Check Position
            switch (cameraPos)
            {
                //	Top Left
                case 1:
                    obFilter[0] = "TL";
                    obFilter[1] = "TL";
                    break;
                //	Top Middle
                case 2:
                    obFilter[0] = "TM";
                    break;
                //	Top Right
                case 3:
                    obFilter[0] = "TR";
                    obFilter[1] = "TR";
                    break;
                //	Middle Right
                case 4:
                    obFilter[0] = "MR";
                    break;
                //	Bottom Right
                case 5:
                    obFilter[0] = "BR";
                    obFilter[1] = "BR";
                    break;
                //	Bottom Middle
                case 6:
                    obFilter[0] = "BM";
                    break;
                //	Bottom Left
                case 7:
                    obFilter[0] = "BL";
                    obFilter[1] = "BL";
                    break;
                //	Middle Left
                case 8:
                    obFilter[0] = "ML";
                    break;
            }//switch
        }//if

        //If nowPlaying is false...
        if (!nowPlaying)
        {
            //... hide Now Playing.
            CPH.ObsHideSource(currentScene, npScene);
            //... move Alerts_Text away from cam.
            postFix[1] = "_NP-Gone";
        }//if
        else
        {
            //... show Now Playing.
            CPH.ObsShowSource(currentScene, npScene);
            //... bring Alerts_Text closer to cam.
            postFix[1] = "_AT-NP";
        }//else

        //Show
        for (int i = 0; i < obFilter.Length; i++)
        {
            obFilter[i] += postFix[i];
            CPH.ObsShowFilter(currentScene, obFilter[i]);
        }//for

        return true;
    }//Execute()
}//CPHInline