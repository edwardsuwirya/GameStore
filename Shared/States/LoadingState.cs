namespace GameStore.Shared.States;

public class LoadingState
{
    public bool IsLoading { get; private set; }

    public event Action? OnChange;

    public void SetOnLoading(bool isLoading)
    {
        IsLoading = isLoading;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}