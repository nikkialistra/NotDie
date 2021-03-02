using System;
using Entities.Player;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    [CreateAssetMenu(fileName = "SceneSettingsInstaller", menuName = "Installers/SceneSettingsInstaller")]
    public class SceneSettingsInstaller : ScriptableObjectInstaller<SceneSettingsInstaller>
    {
        [SerializeField] private PlayerSettings _player;

        [Serializable]
        private class PlayerSettings
        {
            public AttackHandler.Settings AttackHandler;
            public Hp.Settings Hp;
            public PlayerMover.Settings PlayerMover;
            public WeaponHandler.Settings WeaponHandler;
        }
        
        public override void InstallBindings()
        {
            Container.BindInstance(_player).AsSingle();
        }
    }
}