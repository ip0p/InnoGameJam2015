// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DurationFormatter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Foundation.Providers.Formatters
{
    using System.Text;

    using Slash.Unity.DataBind.Core.Presentation;

    using UnityEngine;

    /// <summary>
    ///   Converts a data value which represents a duration in seconds to a formatted string.
    ///   <para>Input: <see cref="System.Single"/> (Duration in seconds).</para>
    ///   <para>Output: <see cref="System.String"/> (Formated duration).</para>
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Formatters/[DB] Duration Formatter")]
    public class DurationFormatter : DataProvider
    {
        /// <summary>
        ///   Type of format.
        /// </summary>
        public enum TimeLabelFormat
        {
            /// <summary>
            ///   Shows the duration with time units, e.g. 2 h 23 m 32 s
            /// </summary>
            ShowTimeUnits,

            /// <summary>
            ///   Shows the duration with colons, e.g. 02:23:32
            /// </summary>
            ShowColons
        }

        #region Fields

        /// <summary>
        ///   Data value which contains the duration in seconds.
        /// </summary>
        [Tooltip("Data value which contains the duration in seconds")]
        public DataBinding Argument;

        /// <summary>
        ///   Type of format.
        /// </summary>
        [Tooltip("Type of format.")]
        public TimeLabelFormat Separators;

        /// <summary>
        ///   Indicates if hours should be shown.
        /// </summary>
        public bool ShowHours;

        /// <summary>
        ///   Indicates if minutes should be shown.
        /// </summary>
        public bool ShowMinutes;

        /// <summary>
        ///   Indicates if seconds should be shown.
        /// </summary>
        public bool ShowSeconds;

        #endregion

        #region Properties

        public override object Value
        {
            get
            {
                var remainingSeconds = this.Argument.GetValue<float>();

                // Split time into subunits.
                var text = new StringBuilder();

                if (this.ShowHours)
                {
                    var totalHours = remainingSeconds / 3600;
                    var totalFullHours = (int)totalHours;

                    if (totalFullHours > 0)
                    {
                        remainingSeconds %= 3600;
                        text.AppendFormat(
                            "{0}{1}",
                            totalFullHours,
                            this.Separators == TimeLabelFormat.ShowTimeUnits ? "h " : ":");
                    }
                }

                if (this.ShowMinutes)
                {
                    var totalMinutes = remainingSeconds / 60;
                    var totalFullMinutes = (int)totalMinutes;

                    if (totalFullMinutes > 0)
                    {
                        remainingSeconds %= 60;
                        text.AppendFormat(
                            "{0:00}{1}",
                            totalFullMinutes,
                            this.Separators == TimeLabelFormat.ShowTimeUnits ? "m " : ":");
                    }
                }

                if (this.ShowSeconds)
                {
                    text.AppendFormat(
                        "{0:00}{1}",
                        (int)remainingSeconds,
                        this.Separators == TimeLabelFormat.ShowTimeUnits ? "s" : "");
                }

                return text.ToString();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///   Unity callback.
        /// </summary>
        protected void Awake()
        {
            this.AddBinding(this.Argument);
        }

        /// <summary>
        ///   Unity callback.
        /// </summary>
        protected void OnDestroy()
        {
            this.RemoveBinding(this.Argument);
        }

        protected override void UpdateValue()
        {
            this.OnValueChanged(this.Value);
        }

        #endregion
    }
}