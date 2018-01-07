using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetTransaction.interfaces
{
    public enum TransferType
    {
        EFT,
        Havale,
        Virman
    }

    public enum ProccessStatus
    {
        OK,
        NotOK
    }

    public interface IMoneyTransfer
    {
        ProccessStatus Status { get; }
        void TransferMoney(Guid senderAccountNo,Guid reciverAccountNo,decimal amount);
    }
}
