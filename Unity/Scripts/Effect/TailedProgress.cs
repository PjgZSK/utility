using UnityEngine;
using UnityEngine.UI;
using System;

namespace Tea.UIEffect
{
    [SLua.CustomLuaClass]
    [RequireComponent(typeof(Image))]
    public class TailedProgress : MonoBehaviour
    {
        private Image targetImage;
        [Range(0, 1)]
        [SerializeField]
        private float _fillAmount;
        [SerializeField] private RectTransform tailTrans = null;

        public event Action<float> OnFillAmountChanged;

        public float fillAmount
        {
            get { return _fillAmount; }
            set
            {
                if (_fillAmount != value)
                {
                    _fillAmount = value;
                    TriggerFillAmountChange();
                }
            }
        }

        public void TriggerFillAmountChange()
        {
            if (targetImage != null)
                targetImage.fillAmount = _fillAmount;
            OnFillAmountChanged?.Invoke(_fillAmount);
            // Debug.Log($"fillAmount: {_fillAmount}");
        }

        public void Reset()
        {
            inited = false;
            Init();
        }

        private bool inited = false;
        public void Init()
        {
            if (inited) return;
            inited = true;

            if (targetImage == null)
                targetImage = GetComponent<Image>();

            if (tailTrans == null)
            {
                Debug.LogError($"{this.name} tail is nil in tailed progress!");
                return;
            }

            if (targetImage.type != Image.Type.Filled)
            {
                Debug.LogError($"{this.name} type of target image must be filled!");
                return;
            }

            switch (targetImage.fillMethod)
            {
                case Image.FillMethod.Horizontal:
                    Rect r = targetImage.GetComponent<RectTransform>().rect;
                    float w = r.width;
                    if ((Image.OriginHorizontal)targetImage.fillOrigin == Image.OriginHorizontal.Left)
                    {
                        tailTrans.anchorMax = tailTrans.anchorMin = new Vector2(0, 0.5f);
                        OnFillAmountChanged = null;
                        OnFillAmountChanged += v => tailTrans.anchoredPosition = new Vector2(w * v, 0);
                    }
                    else
                    {
                        tailTrans.anchorMax = tailTrans.anchorMin = new Vector2(1, 0.5f);
                        OnFillAmountChanged = null;
                        OnFillAmountChanged += v => tailTrans.anchoredPosition = new Vector2(-w * v, 0);
                    }
                    break;
                    // todo: vertical case and radial case
                    // case Image.FillMethod.Vertical:
                    // break;
            }
        }

        void Start()
        {
            Init();
        }
    }
}
