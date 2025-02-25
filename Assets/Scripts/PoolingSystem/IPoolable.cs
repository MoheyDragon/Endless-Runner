public interface IPoolable
{
    void OnGet();
    void OnRelease();
    void OnDestroy();
}


