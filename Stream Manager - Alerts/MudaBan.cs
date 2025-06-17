using System;
using System.IO;
using System.Collections.Generic;

/*Moderate - Muda Ban
 *
 * WRYYYYYYYYYYYYYY!
 *
 * LU: 22-sep-2024
 *
 */

public class CPHInline
{
	public bool Execute()
	{
		//Declarations
		string subSceneAlert, subSceneBan, obsUsername, obsAvatar, obsMuda, twitchUserId;
		int[] phaseLength;
		int animTime, timeCounter, phaseCounter, phaseBuffer, phaseEnd;
		float vol;
		TwitchUserInfoEx userInfo;

		//Initializations
		vol = CPH.GetGlobalVar<float>("qminMediaVolume");
		//Specific
		phaseLength = new int[]
		{
			0,
			3333,
			6458,
			9500,
			14333,
			19416,
			27333,
			28750,
			31041
		};
		subSceneAlert = "SS_Alerts";
		subSceneBan = "SS_BanCard";
		obsUsername = "ban_Username";
		obsAvatar = "ban_Avatar";
		obsMuda = "Muda_Phase";
		animTime = 100;
		timeCounter = 0;
		phaseEnd = 0;

		CPH.TryGetArg("userId", out twitchUserId);

		userInfo = CPH.TwitchGetExtendedUserInfoById(twitchUserId);

		CPH.ObsSetGdiText(subSceneBan, obsUsername, userInfo.UserName);
		CPH.ObsSetBrowserSource(subSceneBan, obsAvatar, userInfo.ProfileImageUrl);
		CPH.ObsShowSource(subSceneAlert, subSceneBan);

		//PHASE 1
		CPH.ObsShowSource(subSceneAlert, $"{obsMuda}One");
		phaseEnd = phaseLength[1] - phaseLength[0];
		CPH.LogVerbose($"『MUDA』 Phase1 Length:\t {phaseEnd} ms");
		phaseCounter = 0;

		// Iterate for 3300ms.
		for (int i = 0; i < 11; i++)
		{
			CPH.ObsShowFilter(subSceneBan, "Baseline_Start");
			CPH.Wait(animTime);
			CPH.ObsShowFilter(subSceneBan, "ShakeLeft");
			CPH.Wait(animTime);
			CPH.ObsShowFilter(subSceneBan, "ShakeRight");
			CPH.Wait(animTime);
			timeCounter += animTime * 3;
			phaseCounter += animTime * 3;
			CPH.LogVerbose($"『MUDA』 Timestamp:\t {timeCounter} ms");
			CPH.LogVerbose($"『MUDA』 \tPhase1:\t ({phaseCounter} ms)");
		}//for
		phaseBuffer = phaseLength[1] - timeCounter;
		CPH.Wait(phaseBuffer);
		timeCounter += phaseBuffer;
		phaseCounter += phaseBuffer;
		CPH.LogVerbose($"『MUDA』 Timestamp:\t {timeCounter} ms");
		CPH.LogVerbose($"『MUDA』 \tPhase1:\t ({phaseCounter} ms)");
		CPH.LogVerbose($"『MUDA』 \tPhase1:\t DONE - GOOD");
		CPH.ObsHideSource(subSceneAlert, $"{obsMuda}One");

		//PHASE 2
		CPH.ObsShowSource(subSceneAlert, $"{obsMuda}Two");
		CPH.ObsShowFilter(subSceneBan, "SecondPhase");
		phaseEnd = phaseLength[2] - phaseLength[1];
		CPH.LogVerbose($"『MUDA』 Phase2 Length:\t {phaseEnd} ms");
		phaseCounter = 0;

		// Iterate for 3000ms.
		for (int i = 0; i < 15; i++)
		{
			CPH.ObsShowFilter(subSceneBan, "DriftUp_Right");
			CPH.Wait(animTime);
			CPH.ObsShowFilter(subSceneBan, "ShakeLeft");
			CPH.Wait(animTime);
			timeCounter += animTime * 2;
			phaseCounter += animTime * 2;
			CPH.LogVerbose($"『MUDA』 Timestamp:\t {timeCounter} ms");
			CPH.LogVerbose($"『MUDA』 \tPhase2:\t ({phaseCounter} ms)");
		}//for
		phaseBuffer = phaseLength[2] - timeCounter;
		CPH.Wait(phaseBuffer);
		timeCounter += phaseBuffer;
		phaseCounter += phaseBuffer;
		CPH.LogVerbose($"『MUDA』 Timestamp:\t {timeCounter} ms");
		CPH.LogVerbose($"『MUDA』 \tPhase2:\t ({phaseCounter} ms)");
		CPH.LogVerbose($"『MUDA』 \tPhase2:\t DONE - GOOD");
		CPH.ObsHideSource(subSceneAlert, $"{obsMuda}Two");

		//PHASE 3
		CPH.ObsShowSource(subSceneAlert, $"{obsMuda}Three");
		CPH.ObsShowFilter(subSceneBan, "ThirdPhase_Start");
		phaseEnd = phaseLength[3] - phaseLength[2];
		CPH.LogVerbose($"『MUDA』 Phase3 Length:\t {phaseEnd} ms");
		phaseCounter = 0;

		// Iterate for 3000ms.
		for (int i = 0; i < 10; i++)
		{
			CPH.ObsShowFilter(subSceneBan, "ThirdPhase");
			CPH.Wait(animTime);
			CPH.ObsShowFilter(subSceneBan, "ShakeLeft");
			CPH.Wait(animTime);
			timeCounter += animTime * 2;
			phaseCounter += animTime * 2;
			CPH.LogVerbose($"『MUDA』 Timestamp:\t {timeCounter} ms");
			CPH.LogVerbose($"『MUDA』 \tPhase3:\t ({phaseCounter} ms)");
			CPH.ObsShowFilter(subSceneBan, "ShakeRight");
			CPH.Wait(animTime);
			timeCounter += animTime;
			phaseCounter += animTime;
			CPH.LogVerbose($"『MUDA』 Timestamp:\t {timeCounter} ms");
			CPH.LogVerbose($"『MUDA』 \tPhase3:\t ({phaseCounter} ms)");
		}//for
		phaseBuffer = phaseLength[3] - timeCounter;
		CPH.Wait(phaseBuffer);
		timeCounter += phaseBuffer;
		phaseCounter += phaseBuffer;
		CPH.LogVerbose($"『MUDA』 Timestamp:\t {timeCounter} ms");
		CPH.LogVerbose($"『MUDA』 \tPhase3:\t ({phaseCounter} ms)");
		CPH.LogVerbose($"『MUDA』 \tPhase3:\t DONE - GOOD");
		CPH.ObsHideSource(subSceneAlert, $"{obsMuda}Three");

		//PHASE 4
		CPH.ObsShowSource(subSceneAlert, $"{obsMuda}Four");
		CPH.ObsHideSource(subSceneAlert, subSceneBan);
		phaseEnd = phaseLength[4] - phaseLength[3];
		CPH.LogVerbose($"『MUDA』 Phase4 Length:\t {phaseEnd} ms");
		phaseCounter = 0;
		CPH.Wait(phaseEnd);
		timeCounter += phaseEnd;
		phaseCounter = phaseEnd;
		CPH.LogVerbose($"『MUDA』 Timestamp:\t {timeCounter} ms");
		CPH.LogVerbose($"『MUDA』 \tPhase4:\t ({phaseCounter} ms)");
		CPH.LogVerbose($"『MUDA』 \tPhase4:\t DONE - GOOD");
		CPH.ObsHideSource(subSceneAlert, $"{obsMuda}Four");

		//PHASE 5
		CPH.ObsShowSource(subSceneAlert, $"{obsMuda}Five");
		CPH.ObsShowSource(subSceneAlert, subSceneBan);
		CPH.ObsShowFilter(subSceneBan, "FifthPhase");
		phaseEnd = phaseLength[5] - phaseLength[4];
		CPH.LogVerbose($"『MUDA』 Phase5 Length:\t {phaseEnd} ms");
		phaseCounter = 0;
		// Iterate for 5000ms.
		for (int i = 0; i < 25; i++)
		{
			CPH.ObsShowFilter(subSceneBan, "DriftUp_Right");
			CPH.Wait(animTime);
			CPH.ObsShowFilter(subSceneBan, "ShakeLeft");
			CPH.Wait(animTime);
			timeCounter += animTime * 2;
			phaseCounter += animTime * 2;
			CPH.LogVerbose($"『MUDA』 Timestamp:\t {timeCounter} ms");
			CPH.LogVerbose($"『MUDA』 \tPhase5:\t ({phaseCounter} ms)");
		}//for
		phaseBuffer = phaseLength[5] - timeCounter;
		CPH.Wait(phaseBuffer);
		timeCounter += phaseBuffer;
		phaseCounter += phaseBuffer;
		CPH.LogVerbose($"『MUDA』 Timestamp:\t {timeCounter} ms");
		CPH.LogVerbose($"『MUDA』 \tPhase5:\t ({phaseCounter} ms)");
		CPH.LogVerbose($"『MUDA』 \tPhase5:\t DONE - GOOD");
		CPH.ObsHideSource(subSceneAlert, $"{obsMuda}Five");

		//PHASE 6
		CPH.ObsShowSource(subSceneAlert, $"{obsMuda}Six");
		CPH.ObsShowFilter(subSceneBan, "SixthPhase");
		phaseEnd = phaseLength[6] - phaseLength[5];
		CPH.LogVerbose($"『MUDA』 Phase6 Length:\t {phaseEnd} ms");
		phaseCounter = 0;
		// Iterate for 7900ms.
		for (int i = 0; i < 40; i++)
		{
			CPH.ObsShowFilter(subSceneBan, "DriftUp_Right");
			CPH.Wait(animTime);
			timeCounter += animTime;
			phaseCounter += animTime;
			CPH.LogVerbose($"『MUDA』 Timestamp:\t {timeCounter} ms");
			CPH.LogVerbose($"『MUDA』 \tPhase6:\t ({phaseCounter} ms)");
			if (i != 39)
			{
				CPH.ObsShowFilter(subSceneBan, "ShakeLeft");
				CPH.Wait(animTime);
				timeCounter += animTime;
				phaseCounter += animTime;
				CPH.LogVerbose($"『MUDA』 Timestamp:\t {timeCounter} ms");
				CPH.LogVerbose($"『MUDA』 \tPhase6:\t ({phaseCounter} ms)");
			}//if
		}//for
		phaseBuffer = phaseLength[6] - timeCounter;
		CPH.Wait(phaseBuffer);
		timeCounter += phaseBuffer;
		phaseCounter += phaseBuffer;
		CPH.LogVerbose($"『MUDA』 Timestamp:\t {timeCounter} ms");
		CPH.LogVerbose($"『MUDA』 \tPhase6:\t ({phaseCounter} ms)");
		CPH.LogVerbose($"『MUDA』 \tPhase6:\t DONE - GOOD");
		CPH.ObsHideSource(subSceneAlert, $"{obsMuda}Six");

		//PHASE 7
		CPH.ObsShowSource(subSceneAlert, $"{obsMuda}Seven");
		CPH.ObsHideSource(subSceneAlert, subSceneBan);
		phaseEnd = phaseLength[7] - phaseLength[6];
		CPH.LogVerbose($"『MUDA』 Phase7 Length:\t {phaseEnd} ms");
		phaseCounter = 0;
		CPH.Wait(phaseEnd);
		timeCounter += phaseEnd;
		phaseCounter += phaseEnd;
		CPH.LogVerbose($"『MUDA』 Timestamp:\t {timeCounter} ms");
		CPH.LogVerbose($"『MUDA』 \tPhase7:\t ({phaseCounter} ms)");
		CPH.LogVerbose($"『MUDA』 \tPhase7:\t DONE - GOOD");
		CPH.ObsHideSource(subSceneAlert, $"{obsMuda}Seven");

		//PHASE 8
		CPH.ObsShowSource(subSceneAlert, $"{obsMuda}Eight");
		CPH.ObsShowSource(subSceneAlert, subSceneBan);
		CPH.ObsShowFilter(subSceneBan, "FinalPhase");
		phaseEnd = phaseLength[8] - phaseLength[7];
		CPH.LogVerbose($"『MUDA』 Phase8 Length:\t {phaseEnd} ms");
		phaseCounter = 0;
		CPH.Wait(100);
		timeCounter += 100;
		phaseCounter += 100;
		CPH.LogVerbose($"『MUDA』 Timestamp:\t {timeCounter} ms");
		CPH.LogVerbose($"『MUDA』 \tPhase8:\t ({phaseCounter} ms)");
		CPH.ObsShowFilter(subSceneBan, "FinalPhaseFlight");
		phaseBuffer = phaseLength[8] - timeCounter;
		CPH.Wait(phaseBuffer);
		timeCounter += phaseBuffer;
		phaseCounter += phaseBuffer;
		CPH.LogVerbose($"『MUDA』 Timestamp:\t {timeCounter} ms");
		CPH.LogVerbose($"『MUDA』 \tPhase8:\t ({phaseCounter} ms)");
		CPH.LogVerbose($"『MUDA』 \tPhase8:\t DONE - GOOD");
		CPH.ObsHideSource(subSceneAlert, $"{obsMuda}Eight");

		//ARRIVERDERCI
		CPH.ObsShowSource(subSceneAlert, $"{obsMuda}Arri");
		phaseEnd = 1649;
		phaseCounter = 0;
		CPH.LogVerbose($"『MUDA』 PhaseArri Length:\t {phaseEnd} ms");
		CPH.Wait(phaseEnd);
		timeCounter += phaseEnd;
		phaseCounter += phaseEnd;
		CPH.LogVerbose($"『MUDA』 Timestamp:\t {timeCounter} ms");
		CPH.LogVerbose($"『MUDA』 \tPhaseArri:\t ({phaseCounter} ms)");
		CPH.LogVerbose($"『MUDA』 \tPhaseArri:\t DONE - GOOD");
		CPH.ObsHideSource(subSceneAlert, $"{obsMuda}Arri");
		CPH.TtsSpeak("Takumi", $"Piss off, {userInfo.UserName}. Good riddance.", false);
		CPH.ObsHideSource(subSceneAlert, subSceneBan);
		return true;
	}//Execute()
}//CPHInline
