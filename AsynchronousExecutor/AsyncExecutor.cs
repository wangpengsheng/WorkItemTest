namespace AsynchronousExecutor;

/// <summary>
/// 执行器模式：Serial（串行）和Parallel（并行）
/// </summary>
public enum ExecutionMode
{
    Serial,
    Parallel
}


/// <summary>
/// 表示执行器可能状态的枚举。
/// </summary>
public enum ExecutionState
{
    Stopped,
    Running,
    Paused,
    Exited
}

public interface IAsyncTask<T>
{
    string Name { get; set; }
    T Result{ get;set;}
    Task<T> Execute();
    void Stop();
    void Exit();
    void Pause();
    ExecutionState GetState();
}

public class Executor<T>
{
    private readonly List<IAsyncTask<T>> _tasks = new();
    private readonly ExecutionMode _executionMode;

    public Executor(ExecutionMode executionMode)
    {
        _executionMode = executionMode;
    }

    public void AddTask<TTask>(TTask task) where TTask : IAsyncTask<T>
    {
        _tasks.Add(task);
    }

    public async Task<List<T>> StartExecute()
    {
        var resultDs = new List<T>();
        switch (_executionMode)
        {
            case ExecutionMode.Serial:
            {
                foreach (var task in _tasks)
                {
                    var result = await task.Execute();
                    resultDs.Add(result);
                }

                break;
            }
            case ExecutionMode.Parallel:
            {
                var tasks = _tasks.Select(task => task.Execute());
                resultDs.AddRange(await Task.WhenAll(tasks));
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }

        return resultDs;
    }

    public void Stop(string taskName)
    {
        _tasks.FirstOrDefault(task => task.GetType().Name == taskName)?.Stop();
    }
    public void Exit(string taskName)
    {
        _tasks.FirstOrDefault(task => task.GetType().Name == taskName)?.Exit();
    }

    public void Pause(string taskName)
    {
        _tasks.FirstOrDefault(task => task.GetType().Name == taskName)?.Pause();
    }
 
    public ExecutionState GetState(string taskName)
    {
        return _tasks.FirstOrDefault(task => task.GetType().Name == taskName)?.GetState() ?? ExecutionState.Exited;
    }
}