//------------------------------------------------------------------------------
// <copyright file="GolangProvider.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Go_Extension.Classification
{
    using System.ComponentModel.Composition;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Utilities;

    [Export(typeof(IClassifierProvider))]
    [ContentType("Golang")]
    internal class GolangClassifierProvider : IClassifierProvider
    {
        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry = null;

        static GolangClassifier golangClassifier;

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            if (golangClassifier == null)
                golangClassifier = new GolangClassifier(ClassificationRegistry);

            return golangClassifier;
        }
    }
}