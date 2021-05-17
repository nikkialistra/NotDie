using Entities.Player;
using Entities.Player.Combat;
using Entities.Player.Items;
using UI.Presenters;
using UI.Views;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UiManager : MonoBehaviour
    {
        private HpPresenter _hpPresenter;
        private WeaponsPresenter _weaponsPresenter;
        private InventoryPresenter _inventoryPresenter;
        private GameOverPresenter _gameOverPresenter;

        [Inject]
        public void Construct(Hp hp, HpView hpView,
            Weapons weapons, WeaponsView weaponsView,
            Inventory inventory, InventoryView inventoryView,
            GameOverView gameOverView)
        {
            _hpPresenter = new HpPresenter(hp, hpView);
            _weaponsPresenter = new WeaponsPresenter(weapons, weaponsView);
            _inventoryPresenter = new InventoryPresenter(inventory, inventoryView);
            _gameOverPresenter = new GameOverPresenter(hp, gameOverView);
        }

        private void Awake()
        {
            _hpPresenter.SetUp();
            _weaponsPresenter.SetUp();
            _inventoryPresenter.SetUp();
            _gameOverPresenter.SetUp();
        }
    }
}