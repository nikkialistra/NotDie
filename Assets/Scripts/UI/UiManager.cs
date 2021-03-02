using Entities.Player;
using UI.Presenters;
using UI.Views;
using UnityEngine;

namespace UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private Hp _hp;
        [SerializeField] private HpView _hpView;
        
        [Space]
        [SerializeField] private WeaponHandler _weaponHandler;
        [SerializeField] private WeaponsView _weaponsView;

        private HpPresenter _hpPresenter;
        private WeaponsPresenter _weaponsPresenter;


        private void Start()
        {
            _hpPresenter = new HpPresenter(_hp, _hpView);
            _weaponsPresenter = new WeaponsPresenter(_weaponHandler.Weapons, _weaponsView);
        }
    }
}