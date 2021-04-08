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

        [Inject]
        public void Construct(Hp hp, HpView hpView,
            Weapons weapons, WeaponsView weaponsView,
            Inventory inventory, InventoryView inventoryView)
        {
            _hpPresenter = new HpPresenter(hp, hpView);
            _weaponsPresenter = new WeaponsPresenter(weapons, weaponsView);
            _inventoryPresenter = new InventoryPresenter(inventory, inventoryView);
        }

        private void Start()
        {
            _hpPresenter.SetUp();
            _weaponsPresenter.SetUp();
            _inventoryPresenter.SetUp();
        }
    }
}