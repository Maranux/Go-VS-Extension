//------------------------------------------------------------------------------
// <copyright file="GolangClassificationDefinition.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Go_Extension.Classification
{
    using System.ComponentModel.Composition;
    using System.Windows.Media;
    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Utilities;

    internal static class GoClassificationDefinitions
    {
        #region Content Type and File Extension Definitions

        [Export]
        [Name("Golang")]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition goContentTypeDefinition = null;

        [Export]
        [FileExtension(".go")]
        [ContentType("Golang")]
        internal static FileExtensionToContentTypeDefinition goFileExtensionDefinition = null;

        #endregion

        #region Classification Type Definitions

        [Export]
        [Name("go")]
        internal static ClassificationTypeDefinition goClassificationDefinition = null;

        [Export]
        [Name("go.variable")]
        [BaseDefinition("go")]
        internal static ClassificationTypeDefinition goVariableDefinition = null;

        [Export]
        [Name("go.import")]
        [BaseDefinition("go")]
        internal static ClassificationTypeDefinition goImportDefinition = null;

        [Export]
        [Name("go.package")]
        [BaseDefinition("go")]
        internal static ClassificationTypeDefinition goPackageDefinition = null;

        [Export]
        [Name("go.func")]
        [BaseDefinition("go")]
        internal static ClassificationTypeDefinition goFuncDefinition = null;

        #endregion

        #region Classification Format Productions

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "go.variable")]
        [Name("go.variable")]
        internal sealed class GoVariableFormat : ClassificationFormatDefinition
        {
            public GoVariableFormat()
            {
                ForegroundColor = Colors.Blue;
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "go.import")]
        [Name("go.import")]
        internal sealed class GoImportFormat : ClassificationFormatDefinition
        {
            public GoImportFormat()
            {
                ForegroundColor = Colors.Red;
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "go.package")]
        [Name("go.package")]
        internal sealed class GoPackageFormat : ClassificationFormatDefinition
        {
            public GoPackageFormat()
            {
                ForegroundColor = Color.FromRgb(0xCC, 0x60, 0x10);
            }
        }


        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "go.func")]
        [Name("go.func")]
        internal sealed class GoFuncFormat : ClassificationFormatDefinition
        {
            public GoFuncFormat()
            {
                ForegroundColor = Colors.Goldenrod;
            }
        }

        #endregion
    }
}
