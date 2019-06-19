using BeatSaberCustomCampaigns.campaign;
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
        const int CENTER_OFFSET = 121;
        public CampainMapPosition mapPosition;
        public NodeButton nodeButton;
        public FormEditNode(NodeButton nodeButton)
        {
            InitializeComponent();
            this.mapPosition = nodeButton.mapPosition;
            this.nodeButton = nodeButton;
        }

        private void FormEditNode_Load(object sender, EventArgs e)
        {
            nodeScale.Value = (decimal)mapPosition.scale;
            numberPortion.Value = mapPosition.numberPortion;
            letterPortion.Text = mapPosition.letterPortion;
            xPositon.Value = nodeButton.Location.X- CENTER_OFFSET;
            yPosition.Value = nodeButton.Location.Y;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mapPosition.scale = (float)nodeScale.Value;
            mapPosition.numberPortion = (int)numberPortion.Value;
            mapPosition.letterPortion = letterPortion.Text;
            nodeButton.SetPosition(new Point((int)xPositon.Value+CENTER_OFFSET, (int)yPosition.Value));
            Close();
        }
    }
}
