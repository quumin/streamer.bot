using System;
using System.IO;
using System.Collections.Generic;

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        Random rnd = new Random();
        List<string>[] question = new List<string>[7];
        List<string> ans = new List<string>();
        string bld_response = "";
		string correct = "";
        
		//Init question array
        for (int b = 0; b < question.Length; b++)
            question[b] = new List<string>();
        
		//Fetch Q&A
        question[0] = CPH.GetGlobalVar<List<string>>("questionsOne");
        question[1] = CPH.GetGlobalVar<List<string>>("questionsTwo");
        question[2] = CPH.GetGlobalVar<List<string>>("questionsThr");
        question[3] = CPH.GetGlobalVar<List<string>>("questionsFou");
        question[4] = CPH.GetGlobalVar<List<string>>("questionsFiv");
        question[5] = CPH.GetGlobalVar<List<string>>("questionsSix");
        question[6] = CPH.GetGlobalVar<List<string>>("questionsSev");
        ans = CPH.GetGlobalVar<List<string>>("ansWer");
        
		//Get Random Index
		int index = rnd.Next(question[0].Count);
		correct = ans[index];
		ans.RemoveAt(index);
		
        //Generate Response
        CPH.SendMessage("Answer the riddle to prove you're better, it can be words or a single letter:");
        for (int i = 0; i < 7; i++)
        {
            if (!string.IsNullOrEmpty(question[i][index]))
            {
                bld_response = "[Line " + (i + 1) + "] " + question[i][index];
            }

            if (i + 1 < 7)
            {
                if (!string.IsNullOrEmpty(question[i + 1][index]) && !question[i][index].EndsWith("?") && !question[i][index].EndsWith(",") && !question[i][index].EndsWith("!") && !question[i][index].EndsWith("."))
                {
                    bld_response += ",";
                }
            }

            if (!string.IsNullOrEmpty(question[i][index]))
            {
                CPH.SendMessage(bld_response);
            }
			//Delete Riddle to Prevent Duplicates
			question[i].RemoveAt(index);
        }
		//Update deleted Lists in Globals
		CPH.SetGlobalVar("questionsOne", question[0]);
        CPH.SetGlobalVar("questionsTwo", question[1]);
		CPH.SetGlobalVar("questionsThr", question[2]);
        CPH.SetGlobalVar("questionsFou", question[3]);
        CPH.SetGlobalVar("questionsFiv", question[4]);
        CPH.SetGlobalVar("questionsSix", question[5]);
        CPH.SetGlobalVar("questionsSev", question[6]);
			
		CPH.LogInfo("『R I D D L E S』 Riddle posted succesfully - answer is: " +
			correct);
		CPH.SetGlobalVar("correctAnswer", correct);
		CPH.SetGlobalVar("chatState", "riddle_on");
        CPH.SendMessage("You have 60 seconds to respond. Glory to the victor!");
		CPH.EnableTimer("RiddleTimer");
        return true;
    }
}