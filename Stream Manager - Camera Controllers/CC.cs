using System;

/*Camera Controller
 * 
 *  Adjust the location of the camera (and others) using OBS Move Plugin.
 *  LU: 05-apr-2026
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        int cameraPos;
        string obsCurrentScene, obsCamScene, obsFilter, obsNpScene, filterPrefix;
        bool qminPuzzleMode, qminNowPlaying, cameraPresent;

        //Initializations
        cameraPos = CPH.GetGlobalVar<int>("qminGlobalMove");
        qminNowPlaying = CPH.GetGlobalVar<bool>("qminNowPlayingBool");
        obsCurrentScene = CPH.ObsGetCurrentScene();
        qminPuzzleMode = Equals(obsCurrentScene,"Puzzles") ? true : false;
        obsCamScene = "SS_KiyoPro_FancyCam";
        obsNpScene = "SS_NowPlaying";
        filterPrefix = obsFilter = "";
        cameraPresent = false;

        //Check for Camera Presence
        switch(obsCurrentScene)
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

        //If the scene has a camera...
		if(cameraPresent)
        {

            //... Check Position
            switch (cameraPos)
            {
                //	Top Left
                case 1:
                    filterPrefix = "TL";
                    break;
                //	Top Middle
                case 2:
                    filterPrefix = "TM";
                    break;
                //	Top Right
                case 3:
                    filterPrefix = "TR";

                    break;
                //	Middle Right
                case 4:
                    filterPrefix = "MR";

                    break;
                //	Bottom Right
                case 5:
                    filterPrefix = "BR";

                    break;
                //	Bottom Middle
                case 6:
                    filterPrefix = "BM";

                    break;
                //	Bottom Left
                case 7:
                    filterPrefix = "BL";

                    break;
                //	Middle Left
                case 8:
                    filterPrefix = "ML";

                    break;
                //  Other
                default:
                    CPH.LogInfo("[C C] Cam Controller went out of bounds!");
                    return true;
                    break;
            }//switch

            //Show

            if(qminPuzzleMode)
            {
                CPH.ObsHideScenesFilters("SS_KP_MaskedCam");
                CPH.ObsShowFilter("SS_KP_MaskedCam", $"{filterPrefix}_Puzzle");
                CPH.ObsShowFilter("SS_KP_MaskedCam", "Cropper");
                CPH.ObsShowFilter("SS_KP_MaskedCam", "Stroke");
                CPH.ObsShowFilter("SS_KP_MaskedCam", "Glow");
                CPH.LogInfo($"[P U Z Z L E] Puzzles mode active ({filterPrefix}_Puzzle).");
            }//if
            
            CPH.ObsShowFilter(obsCamScene, $"{filterPrefix}_Busta");
            
            if(qminNowPlaying)
            {
                CPH.ObsShowSource(obsCamScene, obsNpScene);
                CPH.ObsShowFilter(obsCamScene, $"{filterPrefix}_AT-NP");
                CPH.ObsShowFilter(obsCamScene, $"{filterPrefix}_NP");
            }//if
            else
            {
                CPH.ObsHideSource(obsCamScene, obsNpScene);
                CPH.ObsShowFilter(obsCamScene, $"{filterPrefix}_NP-Gone");
                CPH.ObsShowFilter(obsCamScene, $"{filterPrefix}_NP");
            }//else
        }
        return true;
    }//Execute()
}//CPHInline