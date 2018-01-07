using AdoNetTransaction.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetTransaction.services
{
    public abstract class EFTBaseTransferService
    {
        protected string _connectionString; //kalıtım ile değerleri aktar
        protected IMoneyTransfer _transferType; //kalıtım ile değerleri aktar

        public EFTBaseTransferService(string connectionString)
        {
            _connectionString = connectionString;
        }


        public abstract IMoneyTransfer RedirectTransfer(EFTType type);
    }
}
