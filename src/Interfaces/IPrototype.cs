namespace DesignPatternChallenge.Interfaces;

/// <summary>
/// Interface que define o contrato do padrão Prototype.
/// Qualquer classe que implemente esta interface pode ser clonada,
/// criando uma cópia profunda (deep copy) de si mesma.
/// </summary>
/// <typeparam name="T">O tipo do objeto a ser clonado</typeparam>
public interface IPrototype<T>
{
    /// <summary>
    /// Cria e retorna uma cópia profunda (deep copy) do objeto atual.
    /// A cópia deve ser completamente independente do original,
    /// ou seja, alterar o clone não deve afetar o original e vice-versa.
    /// </summary>
    T Clone();
}
