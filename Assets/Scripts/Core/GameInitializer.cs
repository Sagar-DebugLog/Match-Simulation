using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private MatchConfig config;

    private MatchController controller;

    private void Awake()
    {
        controller = new MatchController(config);
    }

    private void Start()
    {
        controller.StartMatch(this);
    }

    public MatchController GetController() => controller;
}