using System;
using System.Collections.Generic;

public class ScoreSystem
{
    private readonly List<PlayerModel> players;
    private readonly int maxKills;

    public event Action OnScoreChanged;
    public event Action<PlayerModel> OnMaxScoreReached;

    public ScoreSystem(List<PlayerModel> players, int maxKills)
    {
        this.players = players;
        this.maxKills = maxKills;
    }

    public void RegisterKill(PlayerModel killer)
    {
        killer.AddScore();
        OnScoreChanged?.Invoke();

        if (killer.Score >= maxKills) OnMaxScoreReached?.Invoke(killer);
    }

    public List<PlayerModel> GetSortedPlayers()
    {
        players.Sort((a, b) => b.Score.CompareTo(a.Score));
        return players;
    }

    public PlayerModel GetTopPlayer()
    {
        return GetSortedPlayers()[0];
    }
}