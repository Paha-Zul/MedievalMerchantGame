using UnityEngine;
using System.Collections;

/// <summary>
/// This class (MonoBehaviour) is to attach to gameobjects that have a team. This will be used to get the actual Team object from the TeamManager class.
/// </summary>
public class PlayerTeam : MonoBehaviour {

    public string teamName;
    private Player _team;
    public Player team
    {
        get
        {
            if(_team == null) {
                _team = TeamManager.GetTeam(teamName);
            }
            return _team;
        }
        set
        {
            _team = value;
            teamName = value.teamName;
        }
    }

}
