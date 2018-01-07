using AdoNetTransaction.interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetTransaction.Data
{
    public class StoreProcEFTTransfer : IMoneyTransfer
    {
        SqlConnection conn;
        private ProccessStatus _status = ProccessStatus.NotOK;

        public StoreProcEFTTransfer(string connString)
        {
            connString = ConfigurationManager.ConnectionStrings[connString].ConnectionString;
            conn = new SqlConnection(connString);
            conn.Open();
        }

        public ProccessStatus Status
        {
            get
            {
                return _status;
            }
        }

        public void TransferMoney(Guid senderAccountNo, Guid reciverAccountNo, decimal amount)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "ParaTransferiYap";

            cmd.Parameters.AddWithValue("@TransferTipId", 1);
            cmd.Parameters.AddWithValue("@GondericiHesapNo", senderAccountNo);
            cmd.Parameters.AddWithValue("@AliciHesapNo", reciverAccountNo);
            cmd.Parameters.AddWithValue("@OdenecekMiktar", amount);

            int r = cmd.ExecuteNonQuery();

            _status = r > 0 ? ProccessStatus.OK : ProccessStatus.NotOK;
        }
    }
}
