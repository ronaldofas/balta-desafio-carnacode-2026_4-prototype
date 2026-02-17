using DesignPatternChallenge.Interfaces;

namespace DesignPatternChallenge.Models;

public class Section : IPrototype<Section>
{
    public string Name { get; set; }
    public string Content { get; set; }
    public bool IsEditable { get; set; }
    public List<string> Placeholders { get; set; }

    public Section()
    {
        Placeholders = new List<string>();
    }

    /// <summary>
    /// Cria uma cópia profunda de Section.
    /// Strings e bool são copiados diretamente.
    /// Placeholders é uma List (tipo de referência), então criamos
    /// uma nova lista com os mesmos elementos. Como os elementos
    /// são strings (imutáveis em C#), não precisamos cloná-las individualmente.
    /// </summary>
    public Section Clone()
    {
        return new Section
        {
            Name = Name,
            Content = Content,
            IsEditable = IsEditable,
            Placeholders = new List<string>(Placeholders)
        };
    }
}
