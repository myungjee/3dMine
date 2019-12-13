using UnityEngine;
using Zenject;

namespace cubepuzzle
{
    public class MineSweeperInstaller : MonoInstaller
    {
        [SerializeField] private CubeMaker cubeMaker;
        [SerializeField] private Player player;

        public override void InstallBindings()
        {
            Container.Bind<ICubeMaker>().To<CubeMaker>().FromInstance(cubeMaker).AsSingle();
            Container.Bind<IPlayer>().To<Player>().FromInstance(player).AsSingle();
        }
    }
}