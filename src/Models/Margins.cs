using DesignPatternChallenge.Interfaces;

namespace DesignPatternChallenge.Models;

public class Margins : IPrototype<Margins>
{
    public int Top { get; set; }
    public int Bottom { get; set; }
    public int Left { get; set; }
    public int Right { get; set; }

    /// <summary>
    /// Cria uma cópia profunda de Margins.
    /// Como todos os campos são tipos de valor (int),
    /// a cópia é direta — não há referências para se preocupar.
    /// </summary>
    public Margins Clone()
    {
        return new Margins
        {
            Top = Top,
            Bottom = Bottom,
            Left = Left,
            Right = Right
        };
    }
}
