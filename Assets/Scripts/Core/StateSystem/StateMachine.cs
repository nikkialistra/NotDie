using System;
using System.Collections.Generic;

namespace Core.StateSystem
{
   public class StateMachine
   {
      private State _currentState;
   
      private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type,List<Transition>>();
      private List<Transition> _currentTransitions = new List<Transition>();
      private List<Transition> _anyTransitions = new List<Transition>();
   
      private static List<Transition> EmptyTransitions = new List<Transition>(0);

      public void Tick()
      {
         var transition = GetTransition();
         if (transition != null)
            SetState(transition.To);
         
         _currentState?.Tick();
      }

      public void AddTransition(State to, State from, Func<bool> predicate)
      {
         if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
         {
            transitions = new List<Transition>();
            _transitions[from.GetType()] = transitions;
         }
      
         transitions.Add(new Transition(to, predicate));
      }

      public void AddAnyTransition(State state, Func<bool> predicate)
      {
         _anyTransitions.Add(new Transition(state, predicate));
      }

      public void SetState(State state)
      {
         if (state == _currentState)
            return;
      
         _currentState?.OnExit();
         _currentState = state;
      
         _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
         _currentTransitions ??= EmptyTransitions;
      
         _currentState.OnEnter();
      }

      private Transition GetTransition()
      {
         foreach(var transition in _anyTransitions)
            if (transition.Condition())
               return transition;
      
         foreach (var transition in _currentTransitions)
            if (transition.Condition())
               return transition;

         return null;
      }

      private class Transition
      {
         public Func<bool> Condition { get; }
         public State To { get; }

         public Transition(State to, Func<bool> condition)
         {
            To = to;
            Condition = condition;
         }
      }
   }
}