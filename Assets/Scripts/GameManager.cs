using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public MatchSettings matchSettings;

    private void Awake()
    {
        if (singleton)
        {
            Debug.LogWarning("Game magager is existing!");
        }
        else
        {
            singleton = this; 
        }
    }

    #region Player tracking
    private const string PLAYER_ID_PREFIX = "Player ";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public static void RegisterPlayer(string _netId, Player _player)
    {
        string _playerId = PLAYER_ID_PREFIX + _netId;
        players.Add(_playerId, _player);
        _player.name = _playerId;
    }

    public static void DeregisterPlayer(string _playerId)
    {
        players.Remove(_playerId);
    }

    public static Player GetPlayer(string _playerId)
    {
        return players[_playerId];
    }
    #endregion
}
