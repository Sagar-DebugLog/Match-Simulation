using UnityEngine;

[CreateAssetMenu(menuName = "Match/MatchConfig")]
public class MatchConfig : ScriptableObject
{
    public int playerCount = 10;
    public int maxKills = 10;
    public float matchDuration = 180f;

    public bool enableSuddenDeath = true;
}