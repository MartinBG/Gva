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
    public class ApplicantTypeNomenclature {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ApplicantTypeNomenclature() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Rio.Objects.Enums.App_LocalResources.ApplicantTypeNomenclature", typeof(ApplicantTypeNomenclature).Assembly);
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
        ///   Looks up a localized string similar to Лице, регистрирано по Търговския закон.
        /// </summary>
        public static string CommercialRegistered {
            get {
                return ResourceManager.GetString("CommercialRegistered", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Лице, регистрирано като търговец по законодателството на държава - членка на Европейския съюз, или на друга държава - страна по Споразумението за Европейското икономическо пространство.
        /// </summary>
        public static string DealerRegistered {
            get {
                return ResourceManager.GetString("DealerRegistered", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Юридическо лице, регистрирано по българското законодателство.
        /// </summary>
        public static string Entity {
            get {
                return ResourceManager.GetString("Entity", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Чуждестранно юридическо лице.
        /// </summary>
        public static string ForeignEntity {
            get {
                return ResourceManager.GetString("ForeignEntity", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Чужденец.
        /// </summary>
        public static string ForeignPhysical {
            get {
                return ResourceManager.GetString("ForeignPhysical", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Физическо лице, вписано в регистъра на населението на РБ.
        /// </summary>
        public static string Physical {
            get {
                return ResourceManager.GetString("Physical", resourceCulture);
            }
        }
    }
}
