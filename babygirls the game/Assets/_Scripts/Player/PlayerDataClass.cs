/// <summary>
/// This script isn't attached to any GameObject but it is used
/// by the PlayerDataBase script in building the PlayerList.
/// </summary>


public class PlayerDataClass {
	
	//Variables Start___________________________________
	
	public int networkPlayer;
	
	public string playerName;
	
	public int playerScore;
	
	public string playerTeam;
	
	//Variables End_____________________________________
	
	
	public PlayerDataClass Constructor ()
	{
		PlayerDataClass capture = new PlayerDataClass();	
		
		capture.networkPlayer = networkPlayer;
		
		capture.playerName = playerName;
		
		capture.playerScore = playerScore;
		
		capture.playerTeam = playerTeam;
		
		return capture;
	}
}