namespace Core.StateSystem
{
    public abstract class State
    {
        public virtual void Tick()
        {
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }
    }
}