using System;
using Entities.Player;
using UI.Presenters;
using UI.Views;
using UnityEngine;

namespace UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private Settings _settings;

        [Serializable]
        public class Settings
        {
            public Hp Hp;
            public HpView HpView;
        
            [Space]
            public WeaponHandler WeaponHandler;
            public WeaponsView WeaponsView;
        }

        private HpPresenter _hpPresenter;

        private WeaponsPresenter _weaponsPresenter;


        private void Start()
        {
            _hpPresenter = new HpPresenter(_settings.Hp, _settings.HpView);
            _weaponsPresenter = new WeaponsPresenter(_settings.WeaponHandler.Weapons, _settings.WeaponsView);
        }
    }
}