using UnityEngine;
using UnityEngine.UI;

namespace LevelManagement
{
    public class ScreenFader : MonoBehaviour
    {
        [SerializeField]
        protected float _solidAlpha = 1f;

        [SerializeField]
        protected float _clearAlpha = 0f;

        [SerializeField]
        private float _fadeOnDuration = 2f;

        [SerializeField]
        private float _fadeOffDuration = 2f;

        [SerializeField]
        private MaskableGraphic[] graphicsToFade;

        [SerializeField]
        private Text text;

        public float FadeOnDuration { get => _fadeOnDuration; }
        public float FadeOffDuration { get => _fadeOffDuration; }

        protected void SetAlpha(float alpha)
        {
            foreach (MaskableGraphic graphic in graphicsToFade)
            {
                if (graphic != null)
                {
                    graphic.canvasRenderer.SetAlpha(alpha);
                }
            }
        }

        private void Fade(float targetAlpha, float duration)
        {
            foreach (MaskableGraphic graphic in graphicsToFade)
            {
                if (graphic != null)
                {
                    graphic.CrossFadeAlpha(targetAlpha, duration, true);
                }
            }
        }

        public void UpdateText(string _text)
        {
            text.text = _text;
        }

        public void FadeOff()
        {
            SetAlpha(_solidAlpha);
            Fade(_clearAlpha, _fadeOffDuration);
        }

        public void FadeOn()
        {
            SetAlpha(_clearAlpha);
            Fade(_solidAlpha, _fadeOnDuration);
        }
    }
}