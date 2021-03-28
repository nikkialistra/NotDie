using UI;
using UI.Views;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Infrastructure
{
    public class UiInstaller : MonoInstaller
    {
        [Header("Hp")]
        [SerializeField] private Image _fillIndicator;
        
        [SerializeField] private Text _lives;
        
        [SerializeField] private DigitManager _digitManager;
        
        [SerializeField] private Image _healthFirst;
        [SerializeField] private Image _healthSecond;
        [SerializeField] private Image _healthThird;

        [Header("Timer")] 
        [SerializeField] private Text _timer;

        [Header("Weapons")]
        [SerializeField] private Image _leftWeapon;
        [SerializeField] private Image _rightWeapon;

        public override void InstallBindings()
        {
            BindHpView();
            BindTimerView();
            BindWeaponsView();

            Container.Bind<UiManager>().AsSingle().NonLazy();
        }

        private void BindHpView()
        {
            Container.BindInstance(_fillIndicator);

            Container.BindInstance(_digitManager);
            
            Container.BindInstance(_lives).WithId("lives");
            Container.BindInstance(_healthFirst).WithId("healthFirst");
            Container.BindInstance(_healthSecond).WithId("healthSecond");
            Container.BindInstance(_healthThird).WithId("healthThird");

            Container.Bind<HpView>().AsSingle();
        }

        private void BindTimerView()
        {
            Container.BindInstance(_timer).WithId("timer");
            
            Container.BindInterfacesTo<TimerView>().AsSingle();
        }

        private void BindWeaponsView()
        {
            Container.BindInstance(_leftWeapon).WithId("leftWeapon");
            Container.BindInstance(_rightWeapon).WithId("rightWeapon");
            
            Container.Bind<WeaponsView>().AsSingle();
        }
    }
}