using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSystem
{
    private readonly List<PlayerModel> players;
    private readonly ScoreSystem scoreSystem;
    private readonly RespawnSystem respawnSystem;
    private bool running;

    public KillSystem(List<PlayerModel> players, ScoreSystem scoreSystem, RespawnSystem respawnSystem)
    {
        this.players = players;
        this.scoreSystem = scoreSystem;
        this.respawnSystem = respawnSystem;
    }

    public void Start(MonoBehaviour runner)
    {
        running = true;
        runner.StartCoroutine(KillRoutine(runner));
    }

    public void Stop() => running = false;

    private IEnumerator KillRoutine(MonoBehaviour runner)
    {
        while (running)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2f));

            var killer = GetRandomAlive();
            var victim = GetRandomAlive();

            if (killer != null && victim != null && killer != victim)
            {
                respawnSystem.HandleDeath(victim, runner);
                scoreSystem.RegisterKill(killer);
            }
        }
    }

    private PlayerModel GetRandomAlive()
    {
        int aliveCount = 0;

        foreach (var p in players)
            if (p.IsAlive) aliveCount++;

        if (aliveCount < 2) return null;

        PlayerModel selected;

        do
        {
            selected = players[Random.Range(0, players.Count)];
        }
        while (!selected.IsAlive);

        return selected;
    }
}