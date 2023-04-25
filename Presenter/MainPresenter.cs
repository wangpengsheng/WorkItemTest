using Common;

namespace Presenter;

public class MainPresenter
{
    private readonly IMainView _mainView;

    public MainPresenter(IMainView mainView)
    {
        _mainView = mainView;
    }
}