public interface ITouchable
{
    void OnTouch();
    bool DoesHarm();
    int Damage { get; }
}
