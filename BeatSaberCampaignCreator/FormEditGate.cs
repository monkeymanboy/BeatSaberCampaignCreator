using BeatSaberDailyChallenges.campaign;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatSaberCampaignCreator
{
    public partial class FormEditGate : Form
    {
        public CampaignUnlockGate unlockGate;
        public FormEditGate(CampaignUnlockGate unlockGate)
        {
            InitializeComponent();
            this.unlockGate = unlockGate;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            unlockGate.clearsToPass = (int)clearsToPass.Value;
            Close();
        }

        private void FormEditGate_Load(object sender, EventArgs e)
        {
            clearsToPass.Value = unlockGate.clearsToPass;
        }
    }
}
