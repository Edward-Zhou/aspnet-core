using Abp.Localization;
using Abp.UI.Inputs;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace EdwardAbp.Features
{
    public class Claim
    {
        /// <summary>
        /// Gets/sets the arbitrary objects related to this object.
        /// Gets null if a given key does not exist.
        /// This is a shortcut for the <see cref="Attributes"/> dictionary.
        /// </summary>
        /// <param name="key">Key</param>
        public object this[string key]
        {
            //get => Attributes.GetOrDefault(key);
            set => Attributes[key] = value;
        }

        /// <summary>
        /// Arbitrary objects related to this object.
        /// These objects must be serializable.
        /// </summary>
        public IDictionary<string, object> Attributes { get; private set; }

        /// <summary>
        /// Parent of this feature, if one exists.
        /// If set, this feature can be enabled only if the parent is enabled.
        /// </summary>
        public Claim Parent { get; private set; }

        /// <summary>
        /// Unique name of the feature.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Display name of this feature.
        /// This can be used to show features on the UI.
        /// </summary>
        public ILocalizableString DisplayName { get; set; }

        /// <summary>
        /// A brief description for this feature.
        /// This can be used to show this feature's description on the UI. 
        /// </summary>
        public ILocalizableString Description { get; set; }

        /// <summary>
        /// Input type.
        /// This can be used to prepare an input for changing this feature's value.
        /// Default: <see cref="CheckboxInputType"/>.
        /// </summary>
        public IInputType InputType { get; set; }

        /// <summary>
        /// Default value of this feature.
        /// This value is used if this feature's value is not defined for the current edition or tenant.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Feature's scope.
        /// </summary>
        public ClaimScopes Scope { get; set; }

        /// <summary>
        /// List of child features.
        /// </summary>
        public IReadOnlyList<Claim> Children => _children.ToImmutableList();

        private readonly List<Claim> _children;

        /// <summary>
        /// Creates a new feature.
        /// </summary>
        /// <param name="name">Unique name of the feature</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="displayName">Display name of the feature</param>
        /// <param name="description">A brief description for the feature</param>
        /// <param name="scope">Feature scope</param>
        /// <param name="inputType">Input type</param>
        public Claim(string name, string defaultValue, ILocalizableString displayName = null, ILocalizableString description = null, ClaimScopes scope = ClaimScopes.All, IInputType inputType = null)
        {
            Name = name ?? throw new ArgumentNullException("name");
            DisplayName = displayName;
            Description = description;
            Scope = scope;
            DefaultValue = defaultValue;
            InputType = inputType ?? new CheckboxInputType();

            _children = new List<Claim>();
            Attributes = new Dictionary<string, object>();
        }

        /// <summary>
        /// Adds a child feature.
        /// </summary>
        /// <returns>Returns a newly created child feature</returns>
        public Claim CreateChildFeature(string name, string defaultValue, ILocalizableString displayName = null, ILocalizableString description = null, ClaimScopes scope = ClaimScopes.All, IInputType inputType = null)
        {
            var feature = new Claim(name, defaultValue, displayName, description, scope, inputType) { Parent = this };
            _children.Add(feature);
            return feature;
        }

        public override string ToString()
        {
            return string.Format("[Feature: {0}]", Name);
        }
    }
}