using Player;
using UI.Views;

namespace UI.Presenters
{
    public class HpPresenter
    {
        private readonly HpHandler _hpHandler;
        private readonly HpView _view;

        public HpPresenter(HpHandler hpHandler, HpView view)
        {
            _hpHandler = hpHandler;
            _view = view;

            _view.SetLives(_hpHandler.Lives);
            _view.SetFullHealthValue(_hpHandler.HealthFullValue);
            _view.SetHealth(_hpHandler.HealthFullValue);

            _hpHandler.LivesChanged += _view.SetLives;
            _hpHandler.HealthChanged += _view.SetHealth;
        }
    }
}