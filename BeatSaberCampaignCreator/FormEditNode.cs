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
    public partial class FormEditNode : Form
    {
        public CampainMapPosition mapPosition;
        public FormEditNode(CampainMapPosition mapPosition)
        {
            InitializeComponent();
            this.mapPosition = mapPosition;
        }

        private void FormEditNode_Load(object sender, EventArgs e)
        {
            nodeScale.Value = (decimal)mapPosition.scale;
            numberPortion.Value = mapPosition.numberPortion;
            letterPortion.Text = mapPosition.letterPortion;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mapPosition.scale = (float)nodeScale.Value;
            mapPosition.numberPortion = (int)numberPortion.Value;
            mapPosition.letterPortion = letterPortion.Text;
            Close();
        }
    }
}
