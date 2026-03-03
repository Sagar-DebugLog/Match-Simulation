using System;
using System.Collections.Generic;
using UnityEngine;

public class MatchController
{
    private readonly MatchConfig config;
    private readonly List<PlayerModel> players;
    private readonly ScoreSystem scoreSystem;
    private readonly TimerSystem timerSystem;
    private readonly KillSystem killSystem;

    public event Action<List<PlayerModel>> OnScoreChanged;
    public event Action<float> OnTimeChanged;
    public event Action<PlayerModel> OnMatchEnded;

    public MatchController(MatchConfig config)
    {
        this.config = config;

        players = new List<PlayerModel>(config.playerCount);

        for (int i = 0; i < config.playerCount; i++) players.Add(new PlayerModel(i));

        var respawnSystem = new RespawnSystem();
        scoreSystem = new ScoreSystem(players, config.maxKills);
        timerSystem = new TimerSystem(config.matchDuration);
        killSystem = new KillSystem(players, scoreSystem, respawnSystem);

        scoreSystem.OnScoreChanged += () => OnScoreChanged?.Invoke(scoreSystem.GetSortedPlayers());

        scoreSystem.OnMaxScoreReached += EndMatch;

        timerSystem.OnTimeChanged += t => OnTimeChanged?.Invoke(t);

        timerSystem.OnTimeUp += HandleTimeUp;
    }

    public void StartMatch(MonoBehaviour runner)
    {
        timerSystem.Start(runner);
        killSystem.Start(runner);
    }

    private void HandleTimeUp()
    {
        var sorted = scoreSystem.GetSortedPlayers();

        if (sorted.Count > 1 && sorted[0].Score == sorted[1].Score && config.enableSuddenDeath)
        {
            Debug.Log("Sudden Death Activated!");
            return;
        }

        EndMatch(sorted[0]);
    }

    private void EndMatch(PlayerModel winner)
    {
        killSystem.Stop();
        timerSystem.Stop();
        OnMatchEnded?.Invoke(winner);
    }
}