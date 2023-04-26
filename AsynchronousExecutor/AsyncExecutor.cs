namespace AsynchronousExecutor
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


}