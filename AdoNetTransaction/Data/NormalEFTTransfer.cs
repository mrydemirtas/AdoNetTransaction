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
    public class NormalEFTTransfer:IMoneyTransfer
    {
        SqlConnection conn;
        private ProccessStatus _status = ProccessStatus.NotOK;

        public NormalEFTTransfer(string connectionString)
        {
            connectionString = ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
            conn = new SqlConnection(connectionString);
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

            SqlCommand cmd4 = new SqlCommand();
            cmd4.Connection = conn;
            cmd4.CommandText = "select Bakiye from MusteriHesap where MusteriHesap.HesapNo=@GondericiHesapNo";
            cmd4.CommandType = System.Data.CommandType.Text;
            cmd4.Parameters.AddWithValue("@GondericiHesapNo", senderAccountNo);

            decimal ballance = (decimal)cmd4.ExecuteScalar(); //count,sum veya tek bir alan

            SqlTransaction transaction = conn.BeginTransaction();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "update MusteriHesap set Bakiye -= @OdenecekMiktar where MusteriHesap.HesapNo = @GondericiHesapNo";
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@GondericiHesapNo", senderAccountNo);
            cmd.Parameters.AddWithValue("@OdenecekMiktar", amount);
            cmd.Transaction = transaction;

            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "update MusteriHesap set Bakiye += @OdenecekMiktar where MusteriHesap.HesapNo = @AliciHesapNo";
            cmd2.Parameters.AddWithValue("@AliciHesapNo", reciverAccountNo);
            cmd2.Parameters.AddWithValue("@OdenecekMiktar", amount);
            cmd2.Transaction = transaction;

            SqlCommand cmd3 = new SqlCommand();
            cmd3.Connection = conn;
            cmd3.CommandText = "insert into HesapIslemDetay(AliciHesapNo,GonderiHesapNo,GönderilenMiktar,İslemTarihi,İslemTipId,İslemYapanId) values(@AliciHesapNo, @GondericiHesapNo, @OdenecekMiktar, @now, @TransferTipId, @islemYapanId)";
            cmd3.Parameters.AddWithValue("@TransferTipId",1);
            cmd3.Parameters.AddWithValue("@AliciHesapNo", reciverAccountNo);
            cmd3.Parameters.AddWithValue("@OdenecekMiktar", amount);
            cmd3.Parameters.AddWithValue("@GondericiHesapNo", senderAccountNo);
            cmd3.Parameters.AddWithValue("@now", DateTime.Now);
            cmd3.Parameters.AddWithValue("@islemYapanId", 2);
            cmd3.Transaction = transaction;

            //queryleri çalıştır.
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd.ExecuteNonQuery();


            if (ballance < amount)
            {
                transaction.Rollback();
            }
            else
            {
                transaction.Commit();
                _status = ProccessStatus.OK;
            }

        }
    }
}
