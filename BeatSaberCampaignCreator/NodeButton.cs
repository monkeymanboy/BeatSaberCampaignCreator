using BeatSaberCustomCampaigns.campaign;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatSaberCampaignCreator
{
    public class NodeButton : Button
    {
        public CampainMapPosition mapPosition;
        public List<NodeButton> children;
        public NodeButton() : base() {
            mapPosition = new CampainMapPosition();
            children = new List<NodeButton>();
            Size = new Size(40,20);
        }

        public void SetPosition(Point point)
        {
            point.X = Math.Max(0, Math.Min(Parent.Bounds.Width-40, point.X));
            point.Y = Math.Max(0, Math.Min(Parent.Bounds.Height-40, point.Y));
            Location = point;
            mapPosition.x = point.X + Width / 2 - (Parent.Bounds.Width) / 2;
            mapPosition.y = point.Y + Height / 2;
        }

        public void UpdateChildren(List<NodeButton> nodes)
        {
            List<int> children = new List<int>();
            for(int i = 0; i < nodes.Count; i++)
            {
                if (this.children.Contains(nodes[i])) children.Add(i);
            }
            mapPosition.childNodes = children.ToArray();
        }
    }
}
