
public interface ITaskUITimer
{
    public float GetHoldingTime();

    public float GetCurrTime();

    public bool IsDone();

    public bool Talking { get; }
}