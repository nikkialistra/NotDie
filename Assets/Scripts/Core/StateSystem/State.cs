namespace Services.StateSystem
{
    public abstract class State
    {
        public abstract void Tick();

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }
    }
}