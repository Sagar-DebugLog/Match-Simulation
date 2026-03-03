using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private GameObject itemPrefab;

    private List<GameObject> items = new List<GameObject>();

    private void Start()
    {
        var initializer = Object.FindFirstObjectByType<GameInitializer>();

        if (initializer != null)
        {
            var controller = initializer.GetController();
            controller.OnScoreChanged += UpdateLeaderboard;
        }
    }

    private void UpdateLeaderboard(List<PlayerModel> players)
    {
        foreach (var item in items)
            Destroy(item);

        items.Clear();

        foreach (var player in players)
        {
            var obj = Instantiate(itemPrefab, content);
            obj.GetComponent<TextMeshProUGUI>().text = $"Player {player.Id} - {player.Score}";
            items.Add(obj);
        }
    }
}