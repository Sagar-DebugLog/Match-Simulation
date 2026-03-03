public class PlayerModel
{
    public int Id { get; }
    public int Score { get; private set; }
    public bool IsAlive { get; private set; }

    public PlayerModel(int id)
    {
        Id = id;
        Score = 0;
        IsAlive = true;
    }

    public void AddScore() => Score++;

    public void Kill() => IsAlive = false;

    public void Respawn() => IsAlive = true;
}