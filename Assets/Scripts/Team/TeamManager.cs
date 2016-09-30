using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TeamManager  {
    static Dictionary<string, Player> teamMap = new Dictionary<string, Player>();

    /// <summary>
    /// Adds a new team.
    /// </summary>
    /// <param name="name"> The name of the Team.</param>
    /// <returns>The newly created Team.</returns>
    public static Player AddNewTeam(string name) {
        var team = new Player(name);
        teamMap.Add(name, team);
        return team;
    }

    /// <summary>
    /// Tries to get a Team by name.
    /// </summary>
    /// <param name="name">The name of the Team.</param>
    /// <returns>The Team if found, null otherwise.</returns>
    public static Player GetTeam(string name) {
        Player team;
        teamMap.TryGetValue(name, out team);
        return team;
    }
}
