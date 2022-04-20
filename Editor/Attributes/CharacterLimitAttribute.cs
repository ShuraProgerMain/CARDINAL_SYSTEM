using System;
using UnityEngine;

namespace CardinalSystem.Cardinal.Editor.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class CharacterLimitAttribute : Attribute
    {
        public readonly int Value;

        public CharacterLimitAttribute(int value)
        {
            Debug.LogWarning(",kz");
            this.Value = value;
        }
    }
}