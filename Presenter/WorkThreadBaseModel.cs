namespace Presenter;

public class WorkThreadBaseModel
{
    protected string taskName = "";         //任务名称
    protected Thread threadHandler = null;  //线程句柄
    protected bool exitRunning = false;     //退出标志
    protected bool pauseFlag = false;       //暂停标志
    protected int loopInterval = 100;       //任务循环周期


}