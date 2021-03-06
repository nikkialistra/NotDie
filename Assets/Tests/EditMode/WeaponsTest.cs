using System;
using Entities.Player.Combat;
using NUnit.Framework;
using Things.Data;
using Things.Weapon;
using UnityEngine;

namespace Tests.EditMode
{
    public class WeaponsTest
    {
        private Weapons _weapons;
        private WeaponFacade _handWeapon;
        private WeaponFacade _crossedSlot;
    
        [SetUp]
        public void SetUp()
        {
            var gameObject1 = new GameObject();
            _handWeapon = gameObject1.AddComponent<WeaponFacade>();
            
            var gameObject2 = new GameObject();
            _crossedSlot = gameObject2.AddComponent<WeaponFacade>();

            _weapons = new Weapons(_handWeapon, _crossedSlot);
        }

        [Test]
        public void TakeWeapon_NullWeapon_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _weapons.TakeWeapon(null, out var _, out var _));
        }


        // [Test]
        // public void TakeWeapon_TakeFirstWeapon_ReturnsNullAndHaveRightWeapon()
        // {
        //     var weapon = ScriptableObject.CreateInstance<Weapon>();
        //
        //     var returnedWeapon = _weapons.TakeWeapon(weapon, 100);
        //
        //     Assert.Null(returnedWeapon);
        //     Assert.AreEqual(weapon, _weapons.RightWeapon);
        // }
        //
        // [Test]
        // public void TakeWeapon_TakeFirstWeapon_RightWeaponChangedRaised()
        // {
        //     var weapon = ScriptableObject.CreateInstance<Weapon>();
        //     var eventRaised = false;
        //     _weapons.RightWeaponChanged += () => eventRaised = true;
        //
        //     _weapons.TakeWeapon(weapon, 100);
        //     
        //     Assert.AreEqual(true, eventRaised);
        // }
        //
        // [Test]
        // public void TakeWeapon_TakeSecondWeapon_ReturnsNullAndHaveBothWeapons()
        // {
        //     var firstWeapon = ScriptableObject.CreateInstance<Weapon>();
        //     var secondWeapon = ScriptableObject.CreateInstance<Weapon>();
        //
        //     _weapons.TakeWeapon(firstWeapon, 100);
        //     var returnedWeapon = _weapons.TakeWeapon(secondWeapon, 100);
        //     
        //     Assert.Null(returnedWeapon);
        //     
        //     Assert.AreEqual(firstWeapon, _weapons.RightWeapon);
        //     Assert.AreEqual(secondWeapon, _weapons.LeftWeapon);
        // }
        //
        // [Test]
        // public void TakeWeapon_TakeSecondWeapon_LeftWeaponChangedRaised()
        // {
        //     var firstWeapon = ScriptableObject.CreateInstance<Weapon>();
        //     var secondWeapon = ScriptableObject.CreateInstance<Weapon>();
        //     var eventRaised = false;
        //     _weapons.LeftWeaponChanged += () => eventRaised = true;
        //
        //     _weapons.TakeWeapon(firstWeapon, 100);
        //     _weapons.TakeWeapon(secondWeapon, 100);
        //     
        //     Assert.AreEqual(true, eventRaised);
        // }
        //
        // [Test]
        // public void TakeWeapon_TakeThirdWeapon_ReturnsFirstWeaponAndHaveThirdAndSecondWeapons()
        // {
        //     var firstWeapon = ScriptableObject.CreateInstance<Weapon>();
        //     var secondWeapon = ScriptableObject.CreateInstance<Weapon>();
        //     var thirdWeapon = ScriptableObject.CreateInstance<Weapon>();
        //
        //     _weapons.TakeWeapon(firstWeapon, 100);
        //     _weapons.TakeWeapon(secondWeapon, 100);
        //     var returnedWeapon = _weapons.TakeWeapon(thirdWeapon, 100);
        //     
        //     Assert.AreEqual(returnedWeapon, firstWeapon);
        //     
        //     Assert.AreEqual(thirdWeapon, _weapons.RightWeapon);
        //     Assert.AreEqual(secondWeapon, _weapons.LeftWeapon);
        // }
        //
        // [Test]
        // public void TakeWeapon_TakeThirdWeapon_RightWeaponChangedRaised()
        // {
        //     var firstWeapon = ScriptableObject.CreateInstance<Weapon>();
        //     var secondWeapon = ScriptableObject.CreateInstance<Weapon>();
        //     var thirdWeapon = ScriptableObject.CreateInstance<Weapon>();
        //     var eventRaised = false;
        //     _weapons.RightWeaponChanged += () => eventRaised = true;
        //
        //     _weapons.TakeWeapon(firstWeapon, 100);
        //     _weapons.TakeWeapon(secondWeapon, 100);
        //     _weapons.TakeWeapon(thirdWeapon, 100);
        //     
        //     Assert.AreEqual(true, eventRaised);
        // }
        //
        // [Test]
        // public void TakeWeapon_TakeThirdWeaponAfterSwapping_ReturnsSecondWeaponAndHaveFirstAndThirdWeapons()
        // {
        //     var firstWeapon = ScriptableObject.CreateInstance<Weapon>();
        //     var secondWeapon = ScriptableObject.CreateInstance<Weapon>();
        //     var thirdWeapon = ScriptableObject.CreateInstance<Weapon>();
        //
        //     _weapons.TakeWeapon(firstWeapon, 100);
        //     _weapons.TakeWeapon(secondWeapon, 100);
        //     _weapons.SwapWeapons();
        //     var returnedWeapon = _weapons.TakeWeapon(thirdWeapon, 100);
        //     
        //     Assert.AreEqual(returnedWeapon, secondWeapon);
        //     
        //     Assert.AreEqual(firstWeapon, _weapons.RightWeapon);
        //     Assert.AreEqual(thirdWeapon, _weapons.LeftWeapon);
        // }
        //
        // [Test]
        // public void TakeWeapon_TakeThirdWeaponAfterSwapping_RightWeaponChangedRaised()
        // {
        //     var firstWeapon = ScriptableObject.CreateInstance<Weapon>();
        //     var secondWeapon = ScriptableObject.CreateInstance<Weapon>();
        //     var thirdWeapon = ScriptableObject.CreateInstance<Weapon>();
        //     var eventRaised = false;
        //     _weapons.LeftWeaponChanged += () => eventRaised = true;
        //
        //     _weapons.TakeWeapon(firstWeapon, 100);
        //     _weapons.TakeWeapon(secondWeapon, 100);
        //     _weapons.SwapWeapons();
        //     _weapons.TakeWeapon(thirdWeapon, 100);
        //     
        //     Assert.AreEqual(true, eventRaised);
        // }

        [Test]
        public void DropWeapon_ByDefault_ReturnsNull()
        {
            var droppedWeapon = _weapons.DropWeapon();
            
            Assert.AreEqual(null, droppedWeapon);
        }
        
        // [Test]
        // public void DropWeapon_AfterTaking_ReturnsTakenWeaponAndResetsWeapon()
        // {
        //     var weapon = ScriptableObject.CreateInstance<Weapon>();
        //     
        //     _weapons.TakeWeapon(weapon, 100);
        //     var droppedWeapon = _weapons.DropWeapon();
        //
        //     Assert.AreEqual(weapon, droppedWeapon);
        //     Assert.AreEqual(_handWeapon, _weapons.RightWeapon);
        // }
        //
        // [Test]
        // public void DropWeapon_AfterSwappingAfterTaking_ReturnsTakenWeaponAndResetsWeapon()
        // {
        //     var weapon = ScriptableObject.CreateInstance<Weapon>();
        //     
        //     _weapons.SwapWeapons();
        //     _weapons.TakeWeapon(weapon, 100);
        //     var droppedWeapon = _weapons.DropWeapon();
        //
        //     Assert.AreEqual(weapon, droppedWeapon);
        //     Assert.AreEqual(_handWeapon, _weapons.LeftWeapon);
        // }
        //
        // [Test]
        // public void DropWeapon_AfterTaking_RightWeaponChangedRaised()
        // {
        //     var weapon = ScriptableObject.CreateInstance<Weapon>();
        //     var eventRaised = false;
        //     _weapons.RightWeaponChanged += () => eventRaised = true;
        //     
        //     _weapons.TakeWeapon(weapon, 100);
        //     _weapons.DropWeapon();
        //
        //     Assert.AreEqual(true, eventRaised);
        // }
        //
        // [Test]
        // public void DropWeapon_AfterTakingAndDropping_ReturnsNull()
        // {
        //     var weapon = ScriptableObject.CreateInstance<Weapon>();
        //     
        //     _weapons.TakeWeapon(weapon, 100);
        //     _weapons.DropWeapon();
        //     var secondDroppedWeapon = _weapons.DropWeapon();
        //     
        //     Assert.AreEqual(null, secondDroppedWeapon);
        // }
        //
        // [Test]
        // public void DropWeapon_AfterSwappingAfterTaking_LeftWeaponChangedRaised()
        // {
        //     var weapon = ScriptableObject.CreateInstance<Weapon>();
        //     var eventRaised = false;
        //     _weapons.LeftWeaponChanged += () => eventRaised = true;
        //     
        //     _weapons.SwapWeapons();
        //     _weapons.TakeWeapon(weapon, 100);
        //     _weapons.DropWeapon();
        //
        //     Assert.AreEqual(true, eventRaised);
        // }
        //
        // [Test]
        // public void DropWeapon_AfterSwappingAfterTakingAndDropping_ReturnsNull()
        // {
        //     var weapon = ScriptableObject.CreateInstance<Weapon>();
        //     
        //     _weapons.SwapWeapons();
        //     _weapons.TakeWeapon(weapon, 100);
        //     _weapons.DropWeapon();
        //     var secondDroppedWeapon = _weapons.DropWeapon();
        //     
        //     Assert.AreEqual(null, secondDroppedWeapon);
        // }

        [Test]
        public void SwapWeapons_AfterCall_RightWeaponIsActiveRaised()
        {
            var eventRaised = false;
            _weapons.LeftWeaponIsActive += () => eventRaised = true;
            
            _weapons.SwapWeapons();

            Assert.AreEqual(true, eventRaised);
        }
        
        [Test]
        public void SwapWeapons_AfterSecondCall_LeftWeaponIsActiveRaised()
        {
            var eventRaised = false;
            _weapons.RightWeaponIsActive += () => eventRaised = true;
            
            _weapons.SwapWeapons();
            _weapons.SwapWeapons();

            Assert.AreEqual(true, eventRaised);
        }
    }
}
