using System.Collections;
using UnityEngine;

public class RespawnSystem
{
    public void HandleDeath(PlayerModel victim, MonoBehaviour runner)
    {
        victim.Kill();
        runner.StartCoroutine(RespawnRoutine(victim));
    }

    private IEnumerator RespawnRoutine(PlayerModel player)
    {
        yield return new WaitForSeconds(3f);
        player.Respawn();
    }
}