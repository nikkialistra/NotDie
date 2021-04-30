using Entities.Player;
using UI.Views;

namespace UI.Presenters
{
    public class HpPresenter
    {
        private readonly Hp _hp;
        private readonly HpView _hpView;

        public HpPresenter(Hp hp, HpView hpView)
        {
            _hp = hp;
            _hpView = hpView;
        }

        public void SetUp()
        {
            _hpView.SetLives(_hp.Lives);
            
            _hp.LivesChanged += _hpView.SetLives;
            _hp.HealthChanged += _hpView.SetHealth;
            _hp.HealthFullChanged += _hpView.SetHealthFull;
        }
    }
}