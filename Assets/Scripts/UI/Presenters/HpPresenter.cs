using Entities.Player;
using UI.Views;

namespace UI.Presenters
{
    public class HpPresenter
    {
        private readonly Hp _hp;
        private readonly HpView _view;

        public HpPresenter(Hp hp, HpView view)
        {
            _hp = hp;
            _view = view;

            _view.SetLives(_hp.Lives);
            _view.SetFullHealthValue(_hp.HealthFullValue);
            _view.SetHealth(_hp.HealthFullValue);

            _hp.LivesChanged += _view.SetLives;
            _hp.HealthChanged += _view.SetHealth;
        }
    }
}