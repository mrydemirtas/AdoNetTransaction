using AdoNetTransaction.Data;
using AdoNetTransaction.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetTransaction.services
{
    public enum EFTType
    {
        NormalEFT,
        StoreProcEFT
    }


    public class EFTService : EFTBaseTransferService
    {

        public EFTService(string connectionString) : base(connectionString)
        {

        }

        public override IMoneyTransfer RedirectTransfer(EFTType type)
        {
            if (type == EFTType.NormalEFT)
            {
                _transferType = new NormalEFTTransfer(_connectionString);
                return _transferType;
            }

            _transferType = new StoreProcEFTTransfer(_connectionString);

            return _transferType;

        }
    }
}
