using AdoNetTransaction.interfaces;
using AdoNetTransaction.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetTransaction.Data
{
    public class MoneyTransferAggregateService 
    {

        EFTBaseTransferService moneyTransferService;

        public EFTBaseTransferService ServiceInstance(TransferType type,string connectionString)
        {
            
            switch (type)
            {
                case TransferType.EFT:
                    moneyTransferService = new EFTService(connectionString);
                    break;
                case TransferType.Havale:
                    break;
                case TransferType.Virman:
                    break;
                default:
                    break;
            }

            return moneyTransferService;
        }
        
    }
}
