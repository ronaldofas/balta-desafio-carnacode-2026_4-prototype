using DesignPatternChallenge.Models;
using DesignPatternChallenge.Services;

namespace DesignPatternChallenge;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Sistema de Templates de Documentos (Prototype) ===\n");

        // O serviço inicializa os protótipos UMA vez no construtor
        var service = new DocumentService();

        // --- Demonstração de Performance ---
        Console.WriteLine("\n--- Criando 5 contratos de serviço via Clone ---");
        var startTime = DateTime.Now;

        for (int i = 1; i <= 5; i++)
        {
            var contract = service.CreateServiceContract();
            // Personalizamos apenas o que muda — o resto veio do protótipo
            contract.Title = $"Contrato #{i} - Cliente {i}";
        }

        var elapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Tempo total (5 clones): {elapsed}ms\n");

        // --- Demonstração de Independência dos Clones ---
        Console.WriteLine("--- Verificando independência dos clones ---");

        var contrato1 = service.CreateServiceContract();
        var contrato2 = service.CreateServiceContract();

        // Modificamos o clone 1 — o clone 2 NÃO deve ser afetado
        contrato1.Title = "Contrato Modificado";
        contrato1.Sections[0].Content = "Conteúdo alterado no clone 1";
        contrato1.Tags.Add("modificado");

        Console.WriteLine($"Contrato 1 - Título: {contrato1.Title}");
        Console.WriteLine($"Contrato 2 - Título: {contrato2.Title}");
        Console.WriteLine($"Contrato 1 - Seção 1: {contrato1.Sections[0].Content}");
        Console.WriteLine($"Contrato 2 - Seção 1: {contrato2.Sections[0].Content}");
        Console.WriteLine($"Contrato 1 - Tags: {string.Join(", ", contrato1.Tags)}");
        Console.WriteLine($"Contrato 2 - Tags: {string.Join(", ", contrato2.Tags)}");

        // --- Demonstração do Contrato de Consultoria ---
        Console.WriteLine("\n--- Contrato de Consultoria (derivado do de Serviço) ---");
        var consultingContract = service.CreateConsultingContract();
        service.DisplayTemplate(consultingContract);
    }
}
