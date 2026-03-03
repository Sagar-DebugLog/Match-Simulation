using UnityEngine;
using TMPro;

public class WinnerUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI winnerText;

    private void Start()
    {
        panel.SetActive(false);

        var initializer = Object.FindFirstObjectByType<GameInitializer>();

        if (initializer != null)
        {
            var controller = initializer.GetController();
            controller.OnMatchEnded += ShowWinner;
        }
    }

    private void ShowWinner(PlayerModel winner)
    {
        panel.SetActive(true);
        winnerText.text = $"Winner: Player {winner.Id}";
    }
}