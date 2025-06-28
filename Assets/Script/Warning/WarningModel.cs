using UniRx;

public class WarningModel
{
    private readonly ReactiveProperty<int> warningLevel;
    public ReactiveProperty<int> WarningLevel { get { return warningLevel; } }

    public WarningModel()
    {
        warningLevel = new ReactiveProperty<int>(5); // ‰Šú’l‚Í5(ˆÀ‘Só‘Ô)
    }
}