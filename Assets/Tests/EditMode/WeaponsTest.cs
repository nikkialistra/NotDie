using System;
using Entities.Data;
using Entities.Player;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode
{
    public class WeaponsTest
    {
        private Weapons _weapons;
        private Weapon _handWeapon;
    
        [SetUp]
        public void SetUp()
        {
            _handWeapon = ScriptableObject.CreateInstance<Weapon>();
            _weapons = new Weapons(_handWeapon);
        }

        [Test]
        public void TakeWeapon_NullWeapon_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _weapons.TakeWeapon(null));
        }


        [Test]
        public void TakeWeapon_TakeFirstWeapon_ReturnsNullAndHaveRightWeapon()
        {
            var weapon = ScriptableObject.CreateInstance<Weapon>();

            var returnedWeapon = _weapons.TakeWeapon(weapon);

            Assert.Null(returnedWeapon);
            Assert.AreEqual(weapon, _weapons.RightWeapon);
        }

        [Test]
        public void TakeWeapon_TakeFirstWeapon_RightWeaponChangedRaised()
        {
            var weapon = ScriptableObject.CreateInstance<Weapon>();
            var eventRaised = false;
            _weapons.RightWeaponChanged += () => eventRaised = true;

            _weapons.TakeWeapon(weapon);
            
            Assert.AreEqual(true, eventRaised);
        }

        [Test]
        public void TakeWeapon_TakeSecondWeapon_ReturnsNullAndHaveBothWeapons()
        {
            var firstWeapon = ScriptableObject.CreateInstance<Weapon>();
            var secondWeapon = ScriptableObject.CreateInstance<Weapon>();

            _weapons.TakeWeapon(firstWeapon);
            var returnedWeapon = _weapons.TakeWeapon(secondWeapon);
            
            Assert.Null(returnedWeapon);
            
            Assert.AreEqual(firstWeapon, _weapons.RightWeapon);
            Assert.AreEqual(secondWeapon, _weapons.LeftWeapon);
        }
        
        [Test]
        public void TakeWeapon_TakeSecondWeapon_LeftWeaponChangedRaised()
        {
            var firstWeapon = ScriptableObject.CreateInstance<Weapon>();
            var secondWeapon = ScriptableObject.CreateInstance<Weapon>();
            var eventRaised = false;
            _weapons.LeftWeaponChanged += () => eventRaised = true;

            _weapons.TakeWeapon(firstWeapon);
            _weapons.TakeWeapon(secondWeapon);
            
            Assert.AreEqual(true, eventRaised);
        }

        [Test]
        public void TakeWeapon_TakeThirdWeapon_ReturnsFirstWeaponAndHaveThirdAndSecondWeapons()
        {
            var firstWeapon = ScriptableObject.CreateInstance<Weapon>();
            var secondWeapon = ScriptableObject.CreateInstance<Weapon>();
            var thirdWeapon = ScriptableObject.CreateInstance<Weapon>();

            _weapons.TakeWeapon(firstWeapon);
            _weapons.TakeWeapon(secondWeapon);
            var returnedWeapon = _weapons.TakeWeapon(thirdWeapon);
            
            Assert.AreEqual(returnedWeapon, firstWeapon);
            
            Assert.AreEqual(thirdWeapon, _weapons.RightWeapon);
            Assert.AreEqual(secondWeapon, _weapons.LeftWeapon);
        }
        
        [Test]
        public void TakeWeapon_TakeThirdWeapon_RightWeaponChangedRaised()
        {
            var firstWeapon = ScriptableObject.CreateInstance<Weapon>();
            var secondWeapon = ScriptableObject.CreateInstance<Weapon>();
            var thirdWeapon = ScriptableObject.CreateInstance<Weapon>();
            var eventRaised = false;
            _weapons.RightWeaponChanged += () => eventRaised = true;

            _weapons.TakeWeapon(firstWeapon);
            _weapons.TakeWeapon(secondWeapon);
            _weapons.TakeWeapon(thirdWeapon);
            
            Assert.AreEqual(true, eventRaised);
        }
        
        [Test]
        public void TakeWeapon_TakeThirdWeaponAfterSwapping_ReturnsSecondWeaponAndHaveFirstAndThirdWeapons()
        {
            var firstWeapon = ScriptableObject.CreateInstance<Weapon>();
            var secondWeapon = ScriptableObject.CreateInstance<Weapon>();
            var thirdWeapon = ScriptableObject.CreateInstance<Weapon>();

            _weapons.TakeWeapon(firstWeapon);
            _weapons.TakeWeapon(secondWeapon);
            _weapons.SwapWeapons();
            var returnedWeapon = _weapons.TakeWeapon(thirdWeapon);
            
            Assert.AreEqual(returnedWeapon, secondWeapon);
            
            Assert.AreEqual(firstWeapon, _weapons.RightWeapon);
            Assert.AreEqual(thirdWeapon, _weapons.LeftWeapon);
        }
        
        [Test]
        public void TakeWeapon_TakeThirdWeaponAfterSwapping_RightWeaponChangedRaised()
        {
            var firstWeapon = ScriptableObject.CreateInstance<Weapon>();
            var secondWeapon = ScriptableObject.CreateInstance<Weapon>();
            var thirdWeapon = ScriptableObject.CreateInstance<Weapon>();
            var eventRaised = false;
            _weapons.LeftWeaponChanged += () => eventRaised = true;

            _weapons.TakeWeapon(firstWeapon);
            _weapons.TakeWeapon(secondWeapon);
            _weapons.SwapWeapons();
            _weapons.TakeWeapon(thirdWeapon);
            
            Assert.AreEqual(true, eventRaised);
        }

        [Test]
        public void DropWeapon_ByDefault_ReturnsNull()
        {
            var droppedWeapon = _weapons.DropWeapon();
            
            Assert.AreEqual(null, droppedWeapon);
        }
        
        [Test]
        public void DropWeapon_AfterTaking_ReturnsTakenWeaponAndResetsWeapon()
        {
            var weapon = ScriptableObject.CreateInstance<Weapon>();
            
            _weapons.TakeWeapon(weapon);
            var droppedWeapon = _weapons.DropWeapon();

            Assert.AreEqual(weapon, droppedWeapon);
            Assert.AreEqual(_handWeapon, _weapons.RightWeapon);
        }
        
        [Test]
        public void DropWeapon_AfterSwappingAfterTaking_ReturnsTakenWeaponAndResetsWeapon()
        {
            var weapon = ScriptableObject.CreateInstance<Weapon>();
            
            _weapons.SwapWeapons();
            _weapons.TakeWeapon(weapon);
            var droppedWeapon = _weapons.DropWeapon();

            Assert.AreEqual(weapon, droppedWeapon);
            Assert.AreEqual(_handWeapon, _weapons.LeftWeapon);
        }
        
        [Test]
        public void DropWeapon_AfterTaking_RightWeaponChangedRaised()
        {
            var weapon = ScriptableObject.CreateInstance<Weapon>();
            var eventRaised = false;
            _weapons.RightWeaponChanged += () => eventRaised = true;
            
            _weapons.TakeWeapon(weapon);
            _weapons.DropWeapon();

            Assert.AreEqual(true, eventRaised);
        }
        
        [Test]
        public void DropWeapon_AfterTakingAndDropping_ReturnsNull()
        {
            var weapon = ScriptableObject.CreateInstance<Weapon>();
            
            _weapons.TakeWeapon(weapon);
            _weapons.DropWeapon();
            var secondDroppedWeapon = _weapons.DropWeapon();
            
            Assert.AreEqual(null, secondDroppedWeapon);
        }

        [Test]
        public void DropWeapon_AfterSwappingAfterTaking_LeftWeaponChangedRaised()
        {
            var weapon = ScriptableObject.CreateInstance<Weapon>();
            var eventRaised = false;
            _weapons.LeftWeaponChanged += () => eventRaised = true;
            
            _weapons.SwapWeapons();
            _weapons.TakeWeapon(weapon);
            _weapons.DropWeapon();

            Assert.AreEqual(true, eventRaised);
        }
        
        [Test]
        public void DropWeapon_AfterSwappingAfterTakingAndDropping_ReturnsNull()
        {
            var weapon = ScriptableObject.CreateInstance<Weapon>();
            
            _weapons.SwapWeapons();
            _weapons.TakeWeapon(weapon);
            _weapons.DropWeapon();
            var secondDroppedWeapon = _weapons.DropWeapon();
            
            Assert.AreEqual(null, secondDroppedWeapon);
        }
        
        [Test]
        public void SwapWeapons_ByDefault_ReturnsLeftIsNotActive()
        {
            Assert.AreEqual(false, _weapons.LeftIsActive);
        }
        
        [Test]
        public void SwapWeapons_AfterCall_ReturnsLeftIsActive()
        {
            _weapons.SwapWeapons();
            
            Assert.AreEqual(true, _weapons.LeftIsActive);
        }
        
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
