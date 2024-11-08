using JetBrains.Annotations;

namespace Elsa.Api.Client.Shared.UIHints.CodeEditor;

/// <summary>
/// Height options for the code editor component.
/// </summary>
[PublicAPI]
public enum EditorHeight
{
    /// <summary>
    /// The default height.
    /// </summary>
    Default,
    
    /// <summary>
    /// A large height.
    /// </summary>
    Large
}