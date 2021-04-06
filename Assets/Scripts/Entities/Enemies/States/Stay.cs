using Core.StateSystem;
using UnityEngine;

namespace Entities.Enemies.States
{
    public class Stay : State
    {
        public float TimeInStay { get; private set; }

        public override void Tick()
        {
            TimeInStay += Time.deltaTime;
        }

        public override void OnEnter()
        {
            TimeInStay = 0;
        }
    }
}