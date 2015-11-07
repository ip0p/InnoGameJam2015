﻿namespace Slash.Unity.DataBind.UI.Unity.Setters
{
    using UnityEngine;

    /// <summary>
    ///   Set the fill amount of an Image depending on the data value,
    ///   smoothly changing it over time.
    /// </summary>
    [AddComponentMenu("Data Bind/UnityUI/Setters/[DB] Image Fill Amount Smooth Setter (Unity)")]
    public class ImageFillAmountSmoothSetter : ImageFillAmountSetter
    {
        #region Fields

        public float ChangePerSecond = 1.0f;

        [Tooltip(
            "When checked, instead reducing fill amounts, fills till 100% and then fills again to smaller value. "
            + "Useful for level-ups in experience bars, for example.")]
        public bool NeverReduceFillAmount;

        private float targetValue;

        #endregion

        #region Methods

        protected override void OnValueChanged(float newValue)
        {
            this.targetValue = newValue;
        }

        private void Update()
        {
            var difference = this.targetValue - this.Target.fillAmount;
            var maxDifference = Time.deltaTime * this.ChangePerSecond;
            var appliedChange = Mathf.Clamp(Mathf.Abs(difference), 0, maxDifference);

            if (this.NeverReduceFillAmount && difference < 0)
            {
                // Fill till 100%, then fill again to smaller value in next frame.
                this.Target.fillAmount += maxDifference;

                if (this.Target.fillAmount >= 1.0f)
                {
                    this.Target.fillAmount = 0.0f;
                }
            }
            else
            {
                this.Target.fillAmount += Mathf.Sign(difference) * appliedChange;
            }
        }

        #endregion
    }
}