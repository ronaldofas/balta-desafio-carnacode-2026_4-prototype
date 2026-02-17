using DesignPatternChallenge.Models;

namespace DesignPatternChallenge.Services;

public class DocumentService
{
    // Registro de protótipos: templates criados UMA vez e reutilizados via Clone()
    private readonly Dictionary<string, DocumentTemplate> _prototypes = new();

    public DocumentService()
    {
        // Inicializa os protótipos — processo custoso acontece apenas UMA vez
        InitializePrototypes();
    }

    /// <summary>
    /// Registra todos os templates base (protótipos).
    /// Este é o único momento em que os objetos complexos são criados do zero.
    /// </summary>
    private void InitializePrototypes()
    {
        Console.WriteLine("Inicializando protótipos (apenas uma vez)...");

        // Simulando processo custoso de inicialização
        System.Threading.Thread.Sleep(100);

        var serviceContract = new DocumentTemplate
        {
            Title = "Contrato de Prestação de Serviços",
            Category = "Contratos",
            Style = new DocumentStyle
            {
                FontFamily = "Arial",
                FontSize = 12,
                HeaderColor = "#003366",
                LogoUrl = "https://company.com/logo.png",
                PageMargins = new Margins { Top = 2, Bottom = 2, Left = 3, Right = 3 }
            },
            Workflow = new ApprovalWorkflow
            {
                RequiredApprovals = 2,
                TimeoutDays = 5
            }
        };

        serviceContract.Workflow.Approvers.Add("gerente@empresa.com");
        serviceContract.Workflow.Approvers.Add("juridico@empresa.com");

        serviceContract.Sections.Add(new Section
        {
            Name = "Cláusula 1 - Objeto",
            Content = "O presente contrato tem por objeto...",
            IsEditable = true
        });
        serviceContract.Sections.Add(new Section
        {
            Name = "Cláusula 2 - Prazo",
            Content = "O prazo de vigência será de...",
            IsEditable = true
        });
        serviceContract.Sections.Add(new Section
        {
            Name = "Cláusula 3 - Valor",
            Content = "O valor total do contrato é de...",
            IsEditable = true
        });

        serviceContract.RequiredFields.Add("NomeCliente");
        serviceContract.RequiredFields.Add("CPF");
        serviceContract.RequiredFields.Add("Endereco");

        serviceContract.Tags.Add("contrato");
        serviceContract.Tags.Add("servicos");

        serviceContract.Metadata["Versao"] = "1.0";
        serviceContract.Metadata["Departamento"] = "Comercial";
        serviceContract.Metadata["UltimaRevisao"] = DateTime.Now.ToString();

        _prototypes["contrato-servico"] = serviceContract;

        // Contrato de consultoria: CLONA o de serviço e ajusta apenas o que difere
        var consultingContract = serviceContract.Clone();
        consultingContract.Title = "Contrato de Consultoria";
        consultingContract.Sections.RemoveAt(2); // Remove "Cláusula 3 - Valor"
        consultingContract.Sections[0].Content = "O presente contrato de consultoria tem por objeto...";
        consultingContract.Tags.Remove("servicos");
        consultingContract.Tags.Add("consultoria");

        _prototypes["contrato-consultoria"] = consultingContract;
    }

    /// <summary>
    /// Cria um contrato de serviço clonando o protótipo.
    /// Instantâneo — sem Thread.Sleep, sem recriação.
    /// </summary>
    public DocumentTemplate CreateServiceContract()
    {
        Console.WriteLine("Clonando protótipo de Contrato de Serviço...");
        return _prototypes["contrato-servico"].Clone();
    }

    /// <summary>
    /// Cria um contrato de consultoria clonando o protótipo.
    /// Reutiliza a estrutura do contrato de serviço com pequenas variações.
    /// </summary>
    public DocumentTemplate CreateConsultingContract()
    {
        Console.WriteLine("Clonando protótipo de Contrato de Consultoria...");
        return _prototypes["contrato-consultoria"].Clone();
    }

    public void DisplayTemplate(DocumentTemplate template)
    {
        Console.WriteLine($"\n=== {template.Title} ===");
        Console.WriteLine($"Categoria: {template.Category}");
        Console.WriteLine($"Seções: {template.Sections.Count}");
        Console.WriteLine($"Campos obrigatórios: {string.Join(", ", template.RequiredFields)}");
        Console.WriteLine($"Aprovadores: {string.Join(", ", template.Workflow.Approvers)}");
    }
}
