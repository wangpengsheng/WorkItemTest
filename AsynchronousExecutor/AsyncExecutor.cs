<<<<<<< HEAD
﻿namespace AsynchronousExecutor;

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
=======
﻿namespace AsynchronousExecutor
{
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

    public class AsyncExecutor<T>
    {
        private readonly List<Func<Task<T>>> _tasks = new();
        private CancellationTokenSource _cancellationTokenSource = new();
        private readonly ExecutionMode _executionMode;
        private ExecutionState _executionState = ExecutionState.Stopped;
        private readonly List<T> _results = new();

        public AsyncExecutor(ExecutionMode executionMode)
        {
            _executionMode = executionMode;
        }

        public void AddTask(Func<Task<T>> task)
        {
            _tasks.Add(task);
        }

        public async Task<List<T>> StartAsync()
        {
            if (_executionState != ExecutionState.Stopped)
            {
                throw new InvalidOperationException("执行器已经在运行!");
            }

            _executionState = ExecutionState.Running;

            if (_executionMode == ExecutionMode.Serial)
            {
                foreach (var task in _tasks)
                {
                    if (_cancellationTokenSource.IsCancellationRequested)
                    {
                        _executionState = ExecutionState.Stopped;
                        return _results;
                    }

                    var result = await task.Invoke();
                    _results.Add(result);
                }
            }
            else if (_executionMode == ExecutionMode.Parallel)
            {
                var tasks = _tasks.Select(task => task.Invoke());
                _results.AddRange(await Task.WhenAll(tasks));
            }

            _executionState = ExecutionState.Stopped;

            return _results;
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
            _executionState = ExecutionState.Stopped;
        }

        public void Exit()
        {
            _cancellationTokenSource.Cancel();
            _executionState = ExecutionState.Exited;
        }

        public void Pause()
        {
            _cancellationTokenSource.Cancel();
            _executionState = ExecutionState.Paused;
        }

        public async Task<List<T>> ResumeAsync()
        {
            if (_executionState != ExecutionState.Paused)
            {
                throw new InvalidOperationException("执行器没有暂停!");
            }

            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
            _executionState = ExecutionState.Running;

            if (_executionMode == ExecutionMode.Serial)
            {
                foreach (var task in _tasks)
                {
                    if (_cancellationTokenSource.IsCancellationRequested)
                    {
                        _executionState = ExecutionState.Stopped;
                        return _results;
                    }

                    var result = await task.Invoke();
                    _results.Add(result);
                }
            }
            else if (_executionMode == ExecutionMode.Parallel)
            {
                var tasks = _tasks.Select(task => task.Invoke());
                _results.AddRange(await Task.WhenAll(tasks));
            }

            _executionState = ExecutionState.Stopped;

            return _results;
        }

        public ExecutionState GetState()
        {
            return _executionState;
        }
    }


>>>>>>> 887dfb4f68b3383f670c71f69d7d49fc17226f5d
}