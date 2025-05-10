using HseBank.Domain;
using HseBank.Services;

namespace HseBank.Commands;

public class AddOperationCommand : ICommand
{
    private readonly Guid _accountId;
    private readonly decimal _amount;
    private readonly Guid _categoryId;
    private readonly string _description;
    private readonly FinanceFacade _facade;
    private readonly OperationType _type;

    public AddOperationCommand(FinanceFacade facade, OperationType type, Guid accountId, decimal amount,
        Guid categoryId, string description = "")
    {
        _facade = facade;
        _type = type;
        _accountId = accountId;
        _amount = amount;
        _categoryId = categoryId;
        _description = description;
    }

    public void Execute()
    {
        _facade.CreateOperation(_type, _accountId, _amount, _categoryId, _description);
    }
}