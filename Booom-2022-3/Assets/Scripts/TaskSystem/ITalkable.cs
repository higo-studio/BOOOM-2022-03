
public interface ITalkable
{
    public bool InArea();

    public void OnTalkStart();

    public void OnTalkEnd(bool normalEnding);
}