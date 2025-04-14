public class OverviewMode : ICarViewMode
{
    public bool CanInteractWithParts { get => false;}

    public void EnterMode(Car car)
    {
        car.StateManager.EnterOverviewMode();
    }

    public void UpdateMode(Car car)
    {
        return;
    }

    public void ExitMode(Car car)
    {
        return;
    }
}
