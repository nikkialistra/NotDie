using System;
using Entities.Player;
using Entities.Player.Combat;
using UI.Presenters;
using UI.Views;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UiManager : MonoBehaviour
    {
        private Hp _hp;
        private HpView _hpView;
        
        private Weapons _weapons;
        private WeaponsView _weaponsView;

        private HpPresenter _hpPresenter;
        private WeaponsPresenter _weaponsPresenter;
        
        [Inject]
        public void Construct(Hp hp, HpView hpView, Weapons weapons, WeaponsView weaponsView)
        {
            _hp = hp;
            _hpView = hpView;
            
            _weapons = weapons;
            _weaponsView = weaponsView;
            
            _hpPresenter = new HpPresenter(_hp, _hpView);
            _weaponsPresenter = new WeaponsPresenter(_weapons, _weaponsView);
        }

        private void Start()
        {
            _hpPresenter.SetUp();
            _weaponsPresenter.SetUp();
        }
    }
}