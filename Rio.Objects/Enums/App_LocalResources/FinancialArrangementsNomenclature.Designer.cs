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
    public class FinancialArrangementsNomenclature {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal FinancialArrangementsNomenclature() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Rio.Objects.Enums.App_LocalResources.FinancialArrangementsNomenclature", typeof(FinancialArrangementsNomenclature).Assembly);
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
        ///   Looks up a localized string similar to Лизинг на самолети, различен от условията на нормалната търговска експлоатация.
        /// </summary>
        public static string LeasingAircraft {
            get {
                return ResourceManager.GetString("LeasingAircraft", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Заеми, взети при условия, различни от тези на банка или финансова институция.
        /// </summary>
        public static string LoansTaken {
            get {
                return ResourceManager.GetString("LoansTaken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Експлоатация или търговски договори, включващи други авиокомпании.
        /// </summary>
        public static string OperationalContracts {
            get {
                return ResourceManager.GetString("OperationalContracts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Други, моля опишете.
        /// </summary>
        public static string Other {
            get {
                return ResourceManager.GetString("Other", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тежести или поети гаранции /обезпечения.
        /// </summary>
        public static string WeightsAssumed {
            get {
                return ResourceManager.GetString("WeightsAssumed", resourceCulture);
            }
        }
    }
}
