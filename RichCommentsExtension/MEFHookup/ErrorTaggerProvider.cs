﻿namespace LM.RichComments.EditorComponent.MEFHookup
{
    using System.ComponentModel.Composition;
    using Microsoft.VisualStudio.Utilities;
    using Microsoft.VisualStudio.Text.Tagging;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Text;
    using System.Diagnostics;

    [Export(typeof(IViewTaggerProvider))]
    [ContentType("CSharp"), ContentType("C/C++"), ContentType("Basic")]
    [TagType(typeof(ErrorTag))]
    internal class ErrorTaggerProvider : IViewTaggerProvider
    {
        public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag
        {
            if (textView == null)
            {
                return null;
            }

            if (textView.TextBuffer != buffer)
            {
                return null;
            }

            Trace.Assert(textView is IWpfTextView);

            RichCommentManager imageAdornmentManager = textView.Properties.GetOrCreateSingletonProperty<RichCommentManager>(() => new RichCommentManager((IWpfTextView)textView));
            return imageAdornmentManager as ITagger<T>;
        }
    }
}