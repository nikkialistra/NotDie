using Entities.Player;
using UI.Presenters;
using UI.Views;

namespace UI
{
    public class UiManager
    {
        private Hp _hp;
        private HpView _hpView;
        
        private Weapons _weapons;
        private WeaponsView _weaponsView;

        private HpPresenter _hpPresenter;
        private WeaponsPresenter _weaponsPresenter;

        public UiManager(Hp hp, HpView hpView, Weapons weapons, WeaponsView weaponsView)
        {
            _hp = hp;
            _hpView = hpView;
            _weapons = weapons;
            _weaponsView = weaponsView;
            
            _hpPresenter = new HpPresenter(_hp, _hpView);
            _weaponsPresenter = new WeaponsPresenter(_weapons, _weaponsView);
        }
    }
}