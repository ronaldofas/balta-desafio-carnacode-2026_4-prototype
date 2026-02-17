![CR-4](https://github.com/user-attachments/assets/d96fdc78-1ca6-4bc0-afc6-eee0d583c796)

## 🥁 CarnaCode 2026 - Desafio 04 - Prototype

Oi, eu sou o Ronaldo e este é o espaço onde compartilho minha jornada de aprendizado durante o desafio **CarnaCode 2026**, realizado pelo [balta.io](https://balta.io). 👻

Aqui você vai encontrar projetos, exercícios e códigos que estou desenvolvendo durante o desafio. O objetivo é colocar a mão na massa, testar ideias e registrar minha evolução no mundo da tecnologia.

### Sobre este desafio
No desafio **Prototype** eu tive que resolver um problema real implementando o **Design Pattern** em questão.
Neste processo eu aprendi:
* ✅ Boas Práticas de Software
* ✅ Código Limpo
* ✅ SOLID
* ✅ Design Patterns (Padrões de Projeto)

## Problema
Um sistema de gerenciamento documental precisa criar novos documentos baseados em templates pré-configurados complexos (contratos, propostas, relatórios).
O código atual recria objetos do zero, perdendo muito tempo em inicializações.

### Problemas identificados no código original
1. **Recriação repetida de objetos complexos** — templates idênticos eram construídos do zero a cada chamada, incluindo um `Thread.Sleep(100)` simulando inicialização custosa
2. **Código duplicado** — `CreateServiceContract()` e `CreateConsultingContract()` compartilhavam ~90% do código
3. **Sem mecanismo de clonagem** — não existia forma de copiar um template e personalizar apenas o que mudava

---

## 🧬 O Padrão Prototype

O **Prototype** é um padrão de design *criacional* que permite criar novos objetos **clonando uma instância existente** (o "protótipo"), em vez de construí-los do zero.

### Quando usar
- Quando a **criação de objetos é custosa** (acesso a banco, configurações complexas, inicializações lentas)
- Quando objetos novos são **variações pequenas** de objetos já existentes
- Quando se quer evitar subclasses de fábricas para cada variação

### Shallow Copy vs Deep Copy

| Tipo | O que copia | Risco |
|------|-------------|-------|
| **Shallow Copy** | Referências dos objetos internos | Alterar o clone altera o original |
| **Deep Copy** | Novos objetos para cada referência | Clone e original são independentes |

Neste projeto utilizamos **deep copy**, pois `DocumentTemplate` contém listas, dicionários e objetos complexos — shallow copy causaria compartilhamento indesejado de dados entre clones.

---

## 🛠️ Processo de Refatoração

### Parte 1 — Interface e Clone nos modelos

Criamos a interface genérica `IPrototype<T>` e implementamos `Clone()` em todas as classes de modelo, da mais simples à mais complexa:

```
src/
├── Interfaces/
│   └── IPrototype.cs          ← Interface com método Clone()
├── Models/
│   ├── Margins.cs             ← Apenas tipos de valor (int)
│   ├── DocumentStyle.cs       ← Contém referência a Margins
│   ├── Section.cs             ← Contém List<string>
│   ├── ApprovalWorkflow.cs    ← Contém List<string>
│   └── DocumentTemplate.cs    ← Compõe todos os modelos acima
├── Services/
│   └── DocumentService.cs     ← Registro de protótipos
└── Program.cs                 ← Demonstração
```

Cada tipo de propriedade exigiu uma estratégia de clone diferente:

| Tipo da Propriedade | Estratégia |
|---|---|
| `string`, `int`, `bool` | Cópia direta (imutáveis/valor) |
| `List<string>` | `new List<string>(original)` |
| `Dictionary<string, string>` | `new Dictionary<string, string>(original)` |
| Objetos mutáveis (`Style`, `Workflow`) | `objeto.Clone()` |
| `List<Section>` (objetos mutáveis) | `lista.Select(s => s.Clone()).ToList()` |

### Parte 2 — Refatoração do DocumentService

O `DocumentService` foi refatorado para usar um **registro de protótipos**:

- **Antes**: cada chamada a `CreateServiceContract()` recriava todo o template do zero (~100ms cada)
- **Depois**: templates são criados **uma única vez** no construtor e armazenados em um `Dictionary<string, DocumentTemplate>`. Os métodos públicos apenas **clonam** o protótipo

O contrato de consultoria, que antes duplicava ~50 linhas de código, passou a ser criado clonando o contrato de serviço e ajustando apenas o que difere:

```csharp
var consultingContract = serviceContract.Clone();
consultingContract.Title = "Contrato de Consultoria";
consultingContract.Sections.RemoveAt(2);
consultingContract.Tags.Remove("servicos");
consultingContract.Tags.Add("consultoria");
```

### Parte 3 — Demonstração e Resultados

O `Program.cs` demonstra três cenários:

1. **Performance**: 5 contratos clonados em ~0,5ms (antes ~500ms)
2. **Independência dos clones**: modificar um clone não afeta o outro (deep copy funciona)
3. **Derivação**: contrato de consultoria derivado do de serviço com mínimas alterações

---

## ▶️ Como executar

```bash
dotnet run --project src/DesignPatternChallenge.csproj
```

---

## Sobre o CarnaCode 2026
O desafio **CarnaCode 2026** consiste em implementar todos os 23 padrões de projeto (Design Patterns) em cenários reais. Durante os 23 desafios desta jornada, os participantes são submetidos ao aprendizado e prática na idetinficação de códigos não escaláveis e na solução de problemas utilizando padrões de mercado.

### eBook - Fundamentos dos Design Patterns
Minha principal fonte de conhecimento durante o desafio foi o eBook gratuito [Fundamentos dos Design Patterns](https://lp.balta.io/ebook-fundamentos-design-patterns).

### Veja meu progresso no desafio
[Repositório Central](https://github.com/ronaldofas/balta-desafio-carnacode-2026-central)
