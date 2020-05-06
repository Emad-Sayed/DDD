using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Application.Common.TransactionManagement
{
    public class UnitOfWork : IDisposable
    {
        private TransactionScope TransactionScope { get; set; }

        public UnitOfWork()
        {
        }

        public bool IsTransactionDriver => ConsumersCount == 1;


        private int ConsumersCount { get; set; }



        public void StartWork()
        {
            if (TransactionScope == null)
                TransactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            ConsumersCount++;
        }

        public void FinishWork()
        {
            if (IsTransactionDriver)
            {
                TransactionScope.Complete();
                TransactionScope.Dispose();
            }

            ConsumersCount--;
        }


        public void Dispose()
        {
            TransactionScope?.Dispose();
        }
    }
}
