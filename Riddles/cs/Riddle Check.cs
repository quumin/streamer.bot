using System;

public class CPHInline
{

	public bool Execute()
	{
		checkAnswer(CPH.GetGlobalVar<string>("correctAnswer"));
		return true;
	}
	void checkAnswer(string correctAns)
	{
		string usr_ans = args["rawInput"].ToString();
		string usr_nam = args["user"].ToString();

		if(string.Compare(usr_ans, correctAns, StringComparison.CurrentCultureIgnoreCase) == 0)
		{
			CPH.SendMessage(usr_nam + " wins! Nerdge The correct answer was: \"" + correctAns + "!\" EZ");
			CPH.SetGlobalVar("chatState", "default");
			CPH.DisableTimer("RiddleTimer");
		}
		
	}
}
