// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AnimatorBooleanSetter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Foundation.Setters
{
    using UnityEngine;

    /// <summary>
    ///   Sets the animator paramater of a game object to the boolean data value.
    ///   <para>Input: Boolean</para>
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Setters/[DB] Animator Boolean Setter")]
    public class AnimatorBooleanSetter : ComponentSingleSetter<Animator, bool>
    {
        #region Fields

        /// <summary>
        ///   Name of the animator parameter.
        /// </summary>
        [Tooltip("Name of an animator parameter.")]
        public string AnimatorParameterName;

        #endregion

        #region Methods

        protected override void OnValueChanged(bool newValue)
        {
            this.Target.SetBool(this.AnimatorParameterName, newValue);
        }

        #endregion
    }
}