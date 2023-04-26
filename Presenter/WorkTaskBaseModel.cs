using Common;

namespace Presenter;

public class WorkTaskBaseModel
{
    /// <summary>
    /// 任务名称
    /// </summary>
    protected readonly string TaskName; 
    /// <summary>
    /// 线程句柄
    /// </summary>
    protected Thread ThreadHandler;
    /// <summary>
    /// 退出标志
    /// </summary>
    protected bool ExitRunning; 
    /// <summary>
    /// 暂停标志
    /// </summary>
    protected bool PauseFlag; 
    /// <summary>
    /// 任务循环周期
    /// </summary>
    protected int _loopInterval = 1000; 

    private DelegateThreadRoutine _threadRoutine;

    public int LoopInterval
    {
        get => _loopInterval;
        set => _loopInterval = value;
    }

    public WorkTaskBaseModel(string taskName)
    {
        TaskName = taskName;
    }

    public bool InitTask()
    {
        ThreadHandler = new Thread(new ThreadStart(TaskLoopProc))
        {
            IsBackground = true,
            Name = TaskName
        };
        PauseFlag = false;
        ExitRunning = false;
        return true;
    }

    public bool StartTask()
    {
        try
        {
            PauseFlag = false;
            if (ThreadHandler.ThreadState == (ThreadState.Unstarted | ThreadState.Background))
            {
                //this.threadHandler.Apartment = ApartmentState.STA;
                //this.threadHandler.SetApartmentState(ApartmentState.STA); //线程单元模型
                ThreadHandler.Start();
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    [Obsolete("Obsolete")]
    public bool ExitTask()
    {
        ExitRunning = true;
        try
        {
            if (ThreadHandler.ThreadState == (ThreadState.Running | ThreadState.Background))
            {
                if (!ThreadHandler.Join(500))
                {
                    ThreadHandler?.Abort();
                }
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public void PauseTask()
    {
        PauseFlag = true;
    }

    protected virtual void TaskLoopProc()
    {
        while (!ExitRunning)
        {
            Thread.Sleep(_loopInterval);
            if (PauseFlag)
            {
                continue;
            }

            _threadRoutine?.Invoke();
        }
    }

    public void SetThreadRoutine(DelegateThreadRoutine routine)
    {
        _threadRoutine = routine;
    }
}