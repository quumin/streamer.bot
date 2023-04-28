using System;

public class CPHInline
{
	public bool Execute()
	{
		//Declarations
		string usr;

        //Initializations
        usr = args["userName"].ToString();

        CPH.RunAction(usr);
		CPH.DisableAction(usr);
		return true;
	}//Execute()
}//CPHInline