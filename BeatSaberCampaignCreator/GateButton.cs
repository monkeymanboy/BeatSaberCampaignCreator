using BeatSaberDailyChallenges.campaign;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatSaberCampaignCreator
{
    public class GateButton : Button
    {
        public CampaignUnlockGate unlockGate;
        public GateButton() : base()
        {
            unlockGate = new CampaignUnlockGate();
            Size = new Size(200, 20);
        }

        public void SetPosition(Point point)
        {
            point.X = Parent.Width / 2 - Width / 2;
            point.Y = Math.Max(0, Math.Min(Parent.Bounds.Height - 40, point.Y));
            Location = point;
            unlockGate.x = point.X + Width / 2 - (Parent.Bounds.Width) / 2;
            unlockGate.y = point.Y + Height / 2;
        }
    }
}
