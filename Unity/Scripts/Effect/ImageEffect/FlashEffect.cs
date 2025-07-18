using UnityEngine;
using UnityEngine.UI;

namespace Tea.UIEffect
{
    [RequireComponent(typeof(Image))]
    [SLua.CustomLuaClass]
    public class FlashEffect : MonoBehaviour
    {
        private Material _flashMat = null;
        private Image _img = null;

        [SerializeField]
        private Color _flashColor = Color.white;

        private Material _originMat = null;

        public Image img
        {
            get
            {
                if (_img == null)
                {
                    _img = GetComponent<Image>();
                }
                return _img;
            }
        }

        public void Clear()
        {
            _flashMat = null;
            _originMat = null;
            _flashColor = Color.white;
        }

        public Material flashMat
        {
            get
            {
                if (_flashMat == null)
                {
                    _flashMat = new Material(Shader.Find("Custom/Sprites/Colorized"));
                    RefreshColor();
                }
                return _flashMat;
            }
        }
        public Material originMat { get => _originMat; }

        public void RefreshColor()
        {
            if (_flashMat)
                _flashMat.SetColor("_Color", _flashColor);
        }

        public Color flashColor
        {
            get => _flashColor;
            set { _flashColor = value; RefreshColor(); }
        }

        public void SwitchToFlash()
        {
            if (flashMat == null)
            {
                Debug.LogError($"flash mat is null");
                return;
            }

            if (_originMat)
            {
                Debug.LogWarning($"already in flash state");
                return;
            }


            _originMat = img.material;
            img.material = flashMat;
            img.SetMaterialDirty();
        }

        public void SwitchToNormal()
        {
            if (_originMat == null)
            {
                Debug.LogError($"origin mat is null");
                return;
            }

            img.material = _originMat;
            img.SetMaterialDirty();
            _originMat = null;
        }
    }
}
