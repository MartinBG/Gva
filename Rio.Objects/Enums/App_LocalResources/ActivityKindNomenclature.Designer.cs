﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rio.Objects.Enums.App_LocalResources {
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
    public class ActivityKindNomenclature {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ActivityKindNomenclature() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Rio.Objects.Enums.App_LocalResources.ActivityKindNomenclature", typeof(ActivityKindNomenclature).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Пътници и товари.
        /// </summary>
        public static string Cargo {
            get {
                return ResourceManager.GetString("Cargo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Спешни медицински услуги.
        /// </summary>
        public static string Emergency {
            get {
                return ResourceManager.GetString("Emergency", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to летищен оператор на гражданско летище за обществено ползване за обслужване на вътрешни превози.
        /// </summary>
        public static string Internal {
            get {
                return ResourceManager.GetString("Internal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to летищен оператор на гражданско летище за обществено ползване за обслужване на международни превози.
        /// </summary>
        public static string International {
            get {
                return ResourceManager.GetString("International", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to летищен оператор на гражданско летище за дейности, различни от вътрешни и международни превози.
        /// </summary>
        public static string Other {
            get {
                return ResourceManager.GetString("Other", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Само пътници.
        /// </summary>
        public static string Passengers {
            get {
                return ResourceManager.GetString("Passengers", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Само товарни.
        /// </summary>
        public static string Trucks {
            get {
                return ResourceManager.GetString("Trucks", resourceCulture);
            }
        }
    }
}
