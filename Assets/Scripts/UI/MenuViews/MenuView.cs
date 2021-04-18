using UnityEngine.UIElements;

namespace UI.MenuViews
{
    public abstract class MenuView : IMenuView
    {
        protected VisualElement Focused;
        
        protected readonly VisualElement _root;
        protected readonly MenuManager _menuManager;

        protected TemplateContainer _tree;

        private readonly IMenuView _parent;

        private bool _initialized;

        protected MenuView(VisualElement root, IMenuView parent, MenuManager menuManager)
        {
            _root = root;
            _parent = parent;
            _menuManager = menuManager;
        }
        
        public void ShowSelf()
        {
            _menuManager.Return += ShowParent;

            if (!_initialized)
            {
                SetUp();
                _root.Add(_tree);
                _initialized = true;
            }

            _root.Add(_tree);
            Enable();
            
            Focused?.Focus();
            
            _menuManager.LocalizeRecursively(_tree);
        }

        protected virtual void ShowParent()
        {
            _menuManager.Return -= ShowParent;
            _root.Remove(_tree);
            _parent.ShowSelf();
        }
        
        protected void HideSelf()
        {
            _menuManager.Return -= ShowParent;
            _root.Remove(_tree);
        }

        protected abstract void SetUp();
        protected abstract void Enable();
    }
}