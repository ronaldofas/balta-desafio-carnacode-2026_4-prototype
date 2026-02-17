using DesignPatternChallenge.Interfaces;

namespace DesignPatternChallenge.Models;

public class DocumentTemplate : IPrototype<DocumentTemplate>
{
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public List<Section> Sections { get; set; }
    public DocumentStyle Style { get; set; } = new();
    public List<string> RequiredFields { get; set; }
    public Dictionary<string, string> Metadata { get; set; }
    public ApprovalWorkflow Workflow { get; set; } = new();
    public List<string> Tags { get; set; }

    public DocumentTemplate()
    {
        Sections = new List<Section>();
        RequiredFields = new List<string>();
        Metadata = new Dictionary<string, string>();
        Tags = new List<string>();
    }

    /// <summary>
    /// Cria uma cópia profunda de DocumentTemplate.
    /// 
    /// Cada tipo de propriedade exige uma estratégia diferente:
    /// - string/int: cópia direta (imutáveis/valor)
    /// - List&lt;string&gt;: new List com construtor de cópia
    /// - List&lt;Section&gt;: nova lista + Clone() em cada item (objetos mutáveis)
    /// - Dictionary: new Dictionary com construtor de cópia
    /// - DocumentStyle / ApprovalWorkflow: Clone() (objetos mutáveis)
    /// </summary>
    public DocumentTemplate Clone()
    {
        return new DocumentTemplate
        {
            Title = Title,
            Category = Category,

            // Cada Section é um objeto mutável — precisamos clonar cada uma
            Sections = Sections.Select(s => s.Clone()).ToList(),

            // Objetos complexos — delegar para seus próprios Clone()
            Style = Style.Clone(),
            Workflow = Workflow.Clone(),

            // Listas de strings — construtor de cópia é suficiente
            RequiredFields = new List<string>(RequiredFields),
            Tags = new List<string>(Tags),

            // Dicionário de strings — construtor de cópia é suficiente
            Metadata = new Dictionary<string, string>(Metadata)
        };
    }
}
