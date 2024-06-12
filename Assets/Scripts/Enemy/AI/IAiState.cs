namespace Enemy
{
    public interface IAiState
    {
        bool CanActivate();
        void PerformUpdate();
    }
}