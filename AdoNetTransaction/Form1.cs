using AdoNetTransaction.Data;
using AdoNetTransaction.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoNetTransaction
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnMoneyTransfer_Click(object sender, EventArgs e)
        {
            Guid senderNo = Guid.Parse(txtSenderNo.Text);
            Guid recieverNo = Guid.Parse(txtReceiverNo.Text);
            decimal amount = decimal.Parse(txtMoney.Text);

            MoneyTransferAggregateService service = new MoneyTransferAggregateService();
            var connector = service.ServiceInstance(TransferType.EFT, "baglantim");

            var transferService = connector.RedirectTransfer(services.EFTType.StoreProcEFT);
            transferService.TransferMoney(senderNo, recieverNo, amount);

            if (transferService.Status == ProccessStatus.OK)
            {
                MessageBox.Show("Paranız gönderildi");
            }
            else
            {
                MessageBox.Show("İşlem iptal edildi Tekrar Deneyiniz");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
