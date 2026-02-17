using DesignPatternChallenge.Interfaces;

namespace DesignPatternChallenge.Models;

public class ApprovalWorkflow : IPrototype<ApprovalWorkflow>
{
    public List<string> Approvers { get; set; }
    public int RequiredApprovals { get; set; }
    public int TimeoutDays { get; set; }

    public ApprovalWorkflow()
    {
        Approvers = new List<string>();
    }

    /// <summary>
    /// Cria uma cópia profunda de ApprovalWorkflow.
    /// Approvers é uma List (tipo de referência) — criamos uma nova lista.
    /// RequiredApprovals e TimeoutDays são int (tipo de valor) — cópia direta.
    /// </summary>
    public ApprovalWorkflow Clone()
    {
        return new ApprovalWorkflow
        {
            Approvers = new List<string>(Approvers),
            RequiredApprovals = RequiredApprovals,
            TimeoutDays = TimeoutDays
        };
    }
}
