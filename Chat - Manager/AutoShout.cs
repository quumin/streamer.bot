using System;

public class CPHInline
{
	public bool Execute()
	{
		//Declarations
		string str_usr;

        //Initializations
        str_usr = args["userName"].ToString();

        CPH.RunAction(str_usr);
		CPH.DisableAction(str_usr);
		return true;
	}//Execute()
}//CPHInline