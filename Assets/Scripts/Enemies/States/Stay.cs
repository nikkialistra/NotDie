using Services.StateSystem;
using UnityEngine;

namespace Enemies.States
{
    public class Stay : State
    {
        public float TimeStayed { get; set; }

        public override void Tick()
        {
            TimeStayed += Time.deltaTime;
        }

        public override void OnEnter()
        {
            TimeStayed = 0;
        }
    }
}