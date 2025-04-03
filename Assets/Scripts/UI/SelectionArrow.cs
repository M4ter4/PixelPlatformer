using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SelectionArrow : MonoBehaviour
    {
        [SerializeField] private RectTransform[] options;
        private Button[] _components;
        private RectTransform _rectTransform;
        private int _currentOption;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _components = new Button[options.Length];
            for (int i = 0; i < options.Length; i++)
            {
                _components[i] = options[i].GetComponent<Button>();
            }
        }

        public void UpdateOptions(GameObject objectHelder)
        {
            _components = objectHelder.GetComponentsInChildren<Button>();
            options = new RectTransform[_components.Length];
            for (int i = 0; i < _components.Length; i++)
            {
                options[i] = _components[i].gameObject.GetComponent<RectTransform>();
            }
            _currentOption = 0;
            ChangePosition(0);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.W))
                ChangePosition(-1);
            if(Input.GetKeyDown(KeyCode.S))
                ChangePosition(1);
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
                Interact();
        }

        private void Interact()
        {
            _components[_currentOption].onClick.Invoke();
        }

        private void ChangePosition(int change)
        {
            _currentOption += change;
            if (_currentOption >= options.Length)
                _currentOption = 0;
            if(_currentOption < 0)
                _currentOption = options.Length - 1;
            _rectTransform.position = new Vector3(_rectTransform.position.x, options[_currentOption].position.y,0);
        }
    }
}
