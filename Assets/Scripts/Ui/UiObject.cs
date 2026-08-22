using Grid;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace DefaultNamespace.Ui
{
    [RequireComponent(typeof(UIDocument))]
    public sealed class UiObject : MonoBehaviour
    {
        private UIDocument _document;
        private VisualElement _panel;
        
        [Inject] HeightMapData _heightMap;

        private void Awake()
        {
            _document = GetComponent<UIDocument>();
            BuildUi();
        }

        private void BuildUi()
        {
            var root = _document.rootVisualElement;

            _panel = new VisualElement();
            _panel.style.position = Position.Absolute;
            _panel.style.top = 12;
            _panel.style.right = 12;
            _panel.style.width = 260;
            _panel.style.paddingLeft = 10;
            _panel.style.paddingRight = 10;
            _panel.style.paddingTop = 10;
            _panel.style.paddingBottom = 10;
            _panel.style.color = Color.black;
            _panel.style.fontSize = 24;
            _panel.style.backgroundColor = new Color(0f, .1f, .2f, 0.85f);
            root.Add(_panel);

            _panel.Add(new Label("Ui")
            {
                style = { unityFontStyleAndWeight = FontStyle.Bold, 
                    alignSelf = new StyleEnum<Align>(Align.Center),
                    fontSize = 32, 
                    marginBottom = 6, 
                    color = Color.white }
            });

            _panel.Add(new Button( () => _heightMap.Reset())
            {
                text = "Reset Area",
            });
        }
    }
}