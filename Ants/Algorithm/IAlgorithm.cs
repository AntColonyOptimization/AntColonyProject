namespace Ants
{
    public interface IAlgorithm
    {
        IOutputService Execute();
        bool IsFinished();
    }
}
