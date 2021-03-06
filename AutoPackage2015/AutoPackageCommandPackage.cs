﻿//------------------------------------------------------------------------------
// <copyright file="AutoPackageCommandPackage.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;
using System.ComponentModel;

namespace AutoPackage2015 {
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideOptionPage(typeof(OptionPageGrid),
    "Auto package", "Auto package option", 0, 0, true)]
    [Guid(AutoPackageCommandPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class AutoPackageCommandPackage : Package {
        /// <summary>
        /// AutoPackageCommandPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "4b529191-6f7f-4a40-bdd2-cbff281f78e4";

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoPackageCommand"/> class.
        /// </summary>
        public AutoPackageCommandPackage() {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        #region Package Members
        public string SignToolPath {
            get {
                OptionPageGrid page = (OptionPageGrid)GetDialogPage(typeof(OptionPageGrid));
                return page.SignToolPath;
            }
        }
        public bool DrumpToWeb {
            get {
                OptionPageGrid page = (OptionPageGrid)GetDialogPage(typeof(OptionPageGrid));
                return page.DrumpToWeb;
            }
        }
        public bool DrumpPackedFiles {
            get {
                OptionPageGrid page = (OptionPageGrid)GetDialogPage(typeof(OptionPageGrid));
                return page.DrumpPackedFiles;
            }
        }

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize() {
            AutoPackageCommand.Initialize(this);
            base.Initialize();
        }

        #endregion
    }

    public class OptionPageGrid : DialogPage {
        [Category("打包工具")]
        [DisplayName("SignServer位置")]
        [Description("SignServer.exe的绝对路径，不填则不调用")]
        public string SignToolPath { get; set; }

        [Category("打包工具")]
        [DisplayName("是否自动跳转发布系统")]
        [Description("https://dist.wangxutech.com/admin")]
        public bool DrumpToWeb { get; set; } = true;

        [Category("打包工具")]
        [DisplayName("是否自动打开文件夹")]
        [Description("")]
        public bool DrumpPackedFiles { get; set; } = true;
    }
}
