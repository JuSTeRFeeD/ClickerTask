using Leopotam.Ecs;

namespace Project.Scripts.ECS.Components
{
    public struct PlayerBalance
    {
        public int Value;
    }

    public struct BalanceChangedOneFrame : IEcsIgnoreInFilter { }
}