using System;
using UnityEngine;

namespace Cardinal.Editor.CARDINAL_ATTRIBUTES
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class CharacterLimitAttribute : Attribute
    {
        public int value;

        public CharacterLimitAttribute(int value)
        {
            Debug.LogWarning(",kz");
            this.value = value;
        }
    }
}