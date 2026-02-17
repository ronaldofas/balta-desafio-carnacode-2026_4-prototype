using DesignPatternChallenge.Interfaces;

namespace DesignPatternChallenge.Models;

public class DocumentStyle : IPrototype<DocumentStyle>
{
    public string FontFamily { get; set; }
    public int FontSize { get; set; }
    public string HeaderColor { get; set; }
    public string LogoUrl { get; set; }
    public Margins PageMargins { get; set; }

    /// <summary>
    /// Cria uma cópia profunda de DocumentStyle.
    /// Strings e int são copiados diretamente (tipos de valor / imutáveis).
    /// PageMargins é um tipo de referência, então usamos Clone()
    /// para garantir que o clone tenha sua própria instância de Margins.
    /// </summary>
    public DocumentStyle Clone()
    {
        return new DocumentStyle
        {
            FontFamily = FontFamily,
            FontSize = FontSize,
            HeaderColor = HeaderColor,
            LogoUrl = LogoUrl,
            PageMargins = PageMargins?.Clone()
        };
    }
}
