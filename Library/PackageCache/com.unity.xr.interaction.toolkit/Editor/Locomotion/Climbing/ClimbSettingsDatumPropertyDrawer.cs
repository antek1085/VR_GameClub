using Unity.XR.CoreUtils.Datums.Editor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Climbing;

namespace UnityEditor.XR.Interaction.Toolkit.Locomotion.Climbing
{
    /// <summary>
    /// Property drawer for the serializable container class that holds a <see cref="ClimbSettings"/> value or container asset reference.
    /// </summary>
    /// <seealso cref="ClimbSettingsDatumProperty"/>
    /// <seealso cref="DatumPropertyDrawer"/>
    [CustomPropertyDrawer(typeof(ClimbSettingsDatumProperty))]
    [MovedFrom("UnityEditor.XR.Interaction.Toolkit")]
    public class ClimbSettingsDatumPropertyDrawer : DatumPropertyDrawer
    {
        /// <inheritdoc />
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Need to override GetPropertyHeight since the implementation in DatumPropertyDrawer doesn't
            // take into account custom property drawers
            var selectedValue = GetSelectedProperty(property);
            if (selectedValue.hasVisibleChildren)
            {
                return EditorGUI.GetPropertyHeight(selectedValue, true);
            }

            return base.GetPropertyHeight(property, label);
        }
    }
}
