﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18046
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAssetBundler.TextResource {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Exceptions {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Exceptions() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebAssetBundler.TextResource.Exceptions", typeof(Exceptions).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A bundle with name &quot;{0}&quot; could not be loaded. No configuration found..
        /// </summary>
        internal static string BundleDoesNotExist {
            get {
                return ResourceManager.GetString("BundleDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The directory does not exist. Source: {0}.
        /// </summary>
        internal static string DirectoryNotFound {
            get {
                return ResourceManager.GetString("DirectoryNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The file does not exist. Source: {0}.
        /// </summary>
        internal static string FileNotFound {
            get {
                return ResourceManager.GetString("FileNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You must specify a relative directory only. Pattern:{0}.
        /// </summary>
        internal static string InvalidDirectorySearchOrderPattern {
            get {
                return ResourceManager.GetString("InvalidDirectorySearchOrderPattern", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Item with specified source already exists. Source: {0}.
        /// </summary>
        internal static string ItemWithSpecifiedSourceAlreadyExists {
            get {
                return ResourceManager.GetString("ItemWithSpecifiedSourceAlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Modifier &quot;&quot;{0}&quot;&quot; must return readable stream..
        /// </summary>
        internal static string ModifierNotReadable {
            get {
                return ResourceManager.GetString("ModifierNotReadable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The supplied path must be virtual. Source: {0}.
        /// </summary>
        internal static string PathMustBeVirtual {
            get {
                return ResourceManager.GetString("PathMustBeVirtual", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Referenced bundles have already been rendered for this request You cannot call RenderReferenced more than once per request per user..
        /// </summary>
        internal static string RenderReferencedCalledTooManyTimes {
            get {
                return ResourceManager.GetString("RenderReferencedCalledTooManyTimes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You can not call render more than once..
        /// </summary>
        internal static string YouCannotCallRenderMoreThanOnce {
            get {
                return ResourceManager.GetString("YouCannotCallRenderMoreThanOnce", resourceCulture);
            }
        }
    }
}
