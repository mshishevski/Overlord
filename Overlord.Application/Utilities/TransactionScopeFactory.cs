using System.Transactions;

namespace Overlord.Application.Utilities
{
    public static class TransactionScopeFactory
    {
        public static TransactionScope CreateReadCommittedTransactionScope()
        {
            return new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
