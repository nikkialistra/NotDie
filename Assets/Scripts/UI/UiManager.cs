using Entities.Player;
using UI.Presenters;
using UI.Views;
using UnityEngine;

namespace UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private HpHandler _hpHandler;
        [SerializeField] private HpView _hpView;
        
        [Space]
        [SerializeField] private Weapons _weapons;
        [SerializeField] private WeaponsView _weaponsView;

        private HpPresenter _hpPresenter;
        private WeaponsPresenter _weaponsPresenter;


        private void Start()
        {
            _hpPresenter = new HpPresenter(_hpHandler, _hpView);
            _weaponsPresenter = new WeaponsPresenter(_weapons, _weaponsView);
        }
    }
}