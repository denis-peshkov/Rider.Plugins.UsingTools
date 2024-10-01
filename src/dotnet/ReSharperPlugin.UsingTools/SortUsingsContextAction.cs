using ReSharperPlugin.UsingTools.Helpers;

namespace ReSharperPlugin.UsingTools;

[ContextAction(
    Group = CSharpContextActions.GroupID,
    Name = nameof(SortUsingsContextAction),
    Description = nameof(SortUsingsContextAction),
    Priority = -10)]
public class SortUsingsContextAction : ContextActionBase
{
    private readonly ICSharpContextActionDataProvider _provider;

    public SortUsingsContextAction(ICSharpContextActionDataProvider provider)
    {
        _provider = provider;
    }

    public override string Text => Constants.SortContextActionText;

    public override bool IsAvailable(IUserDataHolder cache)
        => _provider.SourceFile.Name.Equals(Constants.FileName, StringComparison.OrdinalIgnoreCase);

    protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
    {
        ICSharpFile typeDeclaration = _provider.GetSelectedElement<ICSharpFile>();
        if (typeDeclaration is null)
            return null;

        return textControl =>
        {
            using var cookie = WriteLockCookie.Create();

            IDocument document = textControl.Document;

            TextHandler.SortAndRemoveDuplicates2(document);
        };
    }
}
