using System.Collections.Generic;

public class EntitiesRegistry : GameSystem
{
    private PlayerController _player;

    private List<GroundAIController> _playerTeam;
    private List<GroundAIController> _aiTeam;

    public PlayerController Player => _player;
    public List<GroundAIController> PlayerTeam => _playerTeam;
    public List<GroundAIController> AITeam => _aiTeam;


    public void Initialize()
    {
        _playerTeam = new List<GroundAIController>();
        _aiTeam = new List<GroundAIController>();
    }


    public void AddPlayer(PlayerController playerController)
    {
        if (_player != null)
            throw new System.Exception("Player already setted");

        _player = playerController;
    }

    public void AddInPlayerTeam(GroundAIController groundAIController)
    {
        _playerTeam.Add(groundAIController);
    }

    public void AddInAITeam(GroundAIController groundAIController)
    {
        _aiTeam.Add(groundAIController);
    }

    public void AddInSituableTeam(GroundAIController groundAIController)
    {
        switch (groundAIController.Team)
        {
            case Team.Player: _playerTeam.Add(groundAIController); break;
            case Team.AI: _aiTeam.Add(groundAIController); break;
        }
    }

    public List<GroundAIController> GetOtherTeam(Team team)
    {
        switch (team)
        {
            case Team.Player: return _aiTeam;
            case Team.AI: return _playerTeam;
        }
        throw new System.Exception("Cant find team");
    }
}

public enum Team
{
    Player,
    AI
}
