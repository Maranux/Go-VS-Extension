//------------------------------------------------------------------------------
// <copyright file="Golang.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Go_Extension.Classification
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Classification;
    using System.Text.RegularExpressions;
    using Irony.Parsing.Construction;

    public class GolangClassifier : IClassifier
    {
        IClassificationTypeRegistryService _classificationTypeRegistry;

        internal GolangClassifier(IClassificationTypeRegistryService registry)
        {
            this._classificationTypeRegistry = registry;
        }

        #region Public Events
#pragma warning disable 67
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore 67
        #endregion // Public Events

        #region Public Methods
        /// <summary>
        /// Classify the given spans, which, for go files, classifies
        /// a line at a time.
        /// </summary>
        /// <param name="span">The span of interest in this projection buffer.</param>
        /// <returns>The list of <see cref="ClassificationSpan"/> as contributed by the source buffers.</returns>
        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            ITextSnapshot snapshot = span.Snapshot;
            SnapshotSpan tSpan = new SnapshotSpan();

            List<ClassificationSpan> spans = new List<ClassificationSpan>();

            if (snapshot.Length == 0)
                return spans;

            int startno = span.Start.GetContainingLine().LineNumber;
            int endno = (span.End - 1).GetContainingLine().LineNumber;

            for (int i = startno; i <= endno; i++)
            {
                ITextSnapshotLine line = snapshot.GetLineFromLineNumber(i);

                IClassificationType type = null;
                string text = line.Snapshot.GetText(
                        new SnapshotSpan(line.Start, Math.Min(4, line.Length))); // We only need the first 4 


                if (getSpan(new Regex("\\svar\\s", RegexOptions.IgnoreCase), 3, ref line, ref tSpan))
                {
                    type = _classificationTypeRegistry.GetClassificationType("go.variable");
                    spans.Add(new ClassificationSpan(tSpan, type));
                }
                else if (getSpan(new Regex("\\import\\s", RegexOptions.IgnoreCase), 6, ref line, ref tSpan))
                {
                    type = _classificationTypeRegistry.GetClassificationType("go.import");
                    spans.Add(new ClassificationSpan(tSpan, type));
                }
                else if (getSpan(new Regex("\\package\\s", RegexOptions.IgnoreCase), 7, ref line, ref tSpan))
                {
                    type = _classificationTypeRegistry.GetClassificationType("go.package");
                    spans.Add(new ClassificationSpan(tSpan, type));
                }
                else if (getSpan(new Regex("\\func\\s", RegexOptions.IgnoreCase), 4, ref line, ref tSpan))
                {
                    type = _classificationTypeRegistry.GetClassificationType("go.func");
                    spans.Add(new ClassificationSpan(tSpan, type));
                }
            }

            return spans;
        }

        public bool getSpan(Regex regex, int length, ref ITextSnapshotLine line, ref SnapshotSpan span)
        {
            int loc = -1;
                if (regex.IsMatch(line.GetText()))
                {
                    loc = 0; // TODO: Replace with regex for all references
                }
            if (loc > -1)
            {
                span = new SnapshotSpan(line.Snapshot, new Span(line.Start + loc, length));
                return true;
            } else
            return false;
        }

        #endregion // Public Methods

    }
}
