using BeatSaberCustomCampaigns;
using BeatSaberCustomCampaigns.campaign;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatSaberCampaignCreator
{
    public partial class Form1 : Form
    {
        Ookii.Dialogs.Wpf.VistaFolderBrowserDialog folderBrowserDialog1;
        Campaign campaign = null;
        Bitmap backgroundBitmap = null;
        string currentDirectory;
        int currentChallenge = 0;
        bool isLoading = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddChallenge();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= campaign.challenges.Count() || campaign.challenges.Count() < 0) return;
            currentChallenge = listBox1.SelectedIndex;
            SetDataToCurrentSelected();
        }
        private void AddChallenge()
        {
            listBox1.Items.Add(listBox1.Items.Count);
            Challenge challenge = new Challenge();
            challenge.modifiers = new ChallengeModifiers();
            challenge.modifiers.speedMul = 1;
            challenge.requirements = new ChallengeRequirement[0];
            campaign.challenges.Add(challenge);
        }
        public void SetDataToCurrentSelected()
        {
            if (campaign == null) return;
            isLoading = true;
            Challenge challenge = campaign.challenges[currentChallenge];
            challengeName.Text = challenge.name;
            beatmapCharacteristic.Text = challenge.characteristic;
            songID.Text = challenge.songid;
            customDownloadURL.Text = challenge.customDownloadURL;
            difficulty.SelectedIndex = (int)challenge.difficulty;
            disappearingArrows.Checked = challenge.modifiers.disappearingArrows;
            strictAngles.Checked = challenge.modifiers.strictAngles;
            fastNotes.Checked = challenge.modifiers.fastNotes;
            noBombs.Checked = challenge.modifiers.noBombs;
            failOnSaberClash.Checked = challenge.modifiers.failOnSaberClash;
            instaFail.Checked = challenge.modifiers.instaFail;
            noFail.Checked = challenge.modifiers.noFail;
            batteryEnergy.Checked = challenge.modifiers.batteryEnergy;
            ghostNotes.Checked = challenge.modifiers.ghostNotes;
            noArrows.Checked = challenge.modifiers.noArrows;
            speedMul.Value = (decimal)challenge.modifiers.speedMul;
            energyType.SelectedIndex = (int)challenge.modifiers.energyType;
            enabledObstacles.SelectedIndex = (int)challenge.modifiers.enabledObstacleType;
            songUnlockable.Checked = challenge.unlockMap;

            reqList.Items.Clear();
            foreach(ChallengeRequirement req in challenge.requirements)
            {
                reqList.Items.Add(reqList.Items.Count);
            }
            if (reqList.Items.Count > 0) reqList.SelectedIndex = 0;
            externalModsList.Items.Clear();
            foreach(KeyValuePair<string, string[]> pair in challenge.externalModifiers)
            {
                externalModsList.Items.Add(pair.Key);
            }
            if (externalModsList.Items.Count > 0) externalModsList.SelectedIndex = 0;

            unlockableListBox.Items.Clear();
            foreach (UnlockableItem req in challenge.unlockableItems)
            {
                unlockableListBox.Items.Add(unlockableListBox.Items.Count);
            }
            if (unlockableListBox.Items.Count > 0) unlockableListBox.SelectedIndex = 0;
            isExtLoading = true;
            textBox1.Text = "";
            isExtLoading = false;
            isLoading = false;
        }
        private void ChallengeDataValueChange(object sender, EventArgs e)
        {
            if (isLoading) return;
            UpdateChallengeInfo();
        }
        private void CampaignDataValueChange(object sender, EventArgs e)
        {
            if (updatingCampaign) return;
            UpdateCampaignInfo();
        }
        public void UpdateChallengeInfo()
        {
            if (campaign == null) return;
            Challenge challenge = campaign.challenges[currentChallenge];
            challenge.name = challengeName.Text;
            challenge.characteristic = beatmapCharacteristic.Text;
            challenge.songid = songID.Text;
            challenge.customDownloadURL = customDownloadURL.Text;
            challenge.difficulty = (BeatmapDifficulty)difficulty.SelectedIndex;
            challenge.modifiers.disappearingArrows = disappearingArrows.Checked;
            challenge.modifiers.strictAngles = strictAngles.Checked;
            challenge.modifiers.fastNotes = fastNotes.Checked;
            challenge.modifiers.noBombs = noBombs.Checked;
            challenge.modifiers.failOnSaberClash = failOnSaberClash.Checked;
            challenge.modifiers.instaFail = instaFail.Checked;
            challenge.modifiers.noFail = noFail.Checked;
            challenge.modifiers.batteryEnergy = batteryEnergy.Checked;
            challenge.modifiers.ghostNotes = ghostNotes.Checked;
            challenge.modifiers.noArrows = noArrows.Checked;
            challenge.modifiers.speedMul = (float)speedMul.Value;
            challenge.modifiers.energyType = (GameplayModifiers.EnergyType)energyType.SelectedIndex;
            challenge.modifiers.enabledObstacleType = (GameplayModifiers.EnabledObstacleType)enabledObstacles.SelectedIndex;
            challenge.unlockMap = songUnlockable.Checked;
        }
        bool updatingCampaign = false;
        public void SetCampaignInfoData()
        {
            updatingCampaign = true;
            campaignName.Text = campaign.info.name;
            campaignDesc.Text = campaign.info.desc;
            allUnlocked.Checked = campaign.info.allUnlocked;
            numericUpDown1.Value = campaign.info.mapHeight;
            backgroundAlpha.Value = (decimal)campaign.info.backgroundAlpha;
            updatingCampaign = false;
        }
        public void UpdateCampaignInfo()
        {
            campaign.info.name = campaignName.Text;
            campaign.info.desc = campaignDesc.Text;
            campaign.info.allUnlocked = allUnlocked.Checked;
            campaign.info.backgroundAlpha = (float)backgroundAlpha.Value;
        }

        //Header
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog().Value)
            {
                Location = new Point(Location.X+1, Location.Y);
                string path = folderBrowserDialog1.SelectedPath;
                currentDirectory = path;
                campaign = new Campaign();
                campaign.info = new CampaignInfo();
                campaign.challenges = new List<Challenge>();
                listBox1.Items.Clear();
                AddChallenge();
                currentChallenge = 0;
                tabControl1.Enabled = true;
                SetCampaignInfoData();
                SetDataToCurrentSelected();
                PrepareMap();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog().Value)
            {
                Location = new Point(Location.X + 1, Location.Y);
                string path = folderBrowserDialog1.SelectedPath;
                currentDirectory = path;
                campaign = new Campaign();
                campaign.info = JsonConvert.DeserializeObject<CampaignInfo>(File.ReadAllText(path + "/info.json"));
                campaign.challenges = new List<Challenge>();
                currentChallenge = 0;
                int i = 0;
                listBox1.Items.Clear();
                while (File.Exists(path + "/" + i + ".json"))
                {
                    campaign.challenges.Add(JsonConvert.DeserializeObject<Challenge>(File.ReadAllText(path + "/" + i + ".json").Replace("\n", "")));
                    listBox1.Items.Add(listBox1.Items.Count);
                    i++;
                }
                SetCampaignInfoData();
                SetDataToCurrentSelected();
                SetRequirementToSelected();
                PrepareMap();
                tabControl1.Enabled = true;
                if (File.Exists(path + "/map background.png"))
                {
                    backgroundBitmap = new Bitmap(path + "/map background.png");
                } else
                {
                    backgroundBitmap = null;
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (campaign == null) return;
            File.WriteAllText(currentDirectory + "/info.json", JsonConvert.SerializeObject(campaign.info));
            for(int i = 0; i < campaign.challenges.Count; i++)
            {
                File.WriteAllText(currentDirectory + "/" + i + ".json", JsonConvert.SerializeObject(campaign.challenges[i]));
            }
        }

        //requirement stuff
        int curRequirementIndex;
        bool reqUpdating = false;
        private void addReq_Click(object sender, EventArgs e)
        {
            reqList.Items.Add(reqList.Items.Count);
            Challenge challenge = campaign.challenges[currentChallenge];
            List<ChallengeRequirement> tempList = new List<ChallengeRequirement>();
            foreach(ChallengeRequirement req in challenge.requirements)
            {
                tempList.Add(req);
            }
            tempList.Add(new ChallengeRequirement());
            challenge.requirements = tempList.ToArray();
        }

        private void remReq_Click(object sender, EventArgs e)
        {
            Challenge challenge = campaign.challenges[currentChallenge];
            List<ChallengeRequirement> tempList = new List<ChallengeRequirement>();
            foreach (ChallengeRequirement req in challenge.requirements)
            {
                tempList.Add(req);
            }
            tempList.RemoveAt(curRequirementIndex);
            challenge.requirements = tempList.ToArray();
            reqList.Items.Clear();
            foreach (ChallengeRequirement req in challenge.requirements)
            {
                reqList.Items.Add(reqList.Items.Count);
            }
            if (reqList.Items.Count > 0) reqList.SelectedIndex = 0;
        }

        private void reqList_SelectedIndexChanged(object sender, EventArgs e)
        {
            curRequirementIndex = reqList.SelectedIndex;
            SetRequirementToSelected();
        }
        private void RequirementValueChanged(object sender, EventArgs e)
        {
            if (reqUpdating) return;
            UpdateRequirementInfo();
        }
        public void UpdateRequirementInfo()
        {
            if (campaign == null) return;
            Challenge challenge = campaign.challenges[currentChallenge];
            if (challenge.requirements.Count() == 0 || curRequirementIndex >= challenge.requirements.Count()) return;
            ChallengeRequirement requirement = challenge.requirements[curRequirementIndex];
            requirement.type = requirementType.Text;
            requirement.count = (int)requirementValue.Value;
            requirement.isMax = requirementIsMax.Checked;
        }
        public void SetRequirementToSelected()
        {
            if (campaign == null) return;
            Challenge challenge = campaign.challenges[currentChallenge];
            if (challenge.requirements.Count() == 0 || curRequirementIndex >= challenge.requirements.Count()) return;
            reqUpdating = true;
            ChallengeRequirement requirement = challenge.requirements[curRequirementIndex];
            requirementType.Text = requirement.type;
            requirementValue.Value = requirement.count;
            requirementIsMax.Checked = requirement.isMax;
            reqUpdating = false;
        }


        //External Modifier stuff
        private void addExtMod_Click(object sender, EventArgs e)
        {
            if (campaign == null) return;
            Challenge challenge = campaign.challenges[currentChallenge];
            string modName = ShowDialog("Mod Name (ex GameplayModifiersPlus)", "External Modifier Mod Name");
            challenge.externalModifiers.Add(modName, new string[0]);
            externalModsList.Items.Add(modName);
            externalModsList.SelectedIndex = externalModsList.Items.Count - 1;
        }

        private void remExtMod_Click(object sender, EventArgs e)
        {
            if (campaign == null) return;
            Challenge challenge = campaign.challenges[currentChallenge];
            challenge.externalModifiers.Remove((string)externalModsList.SelectedItem);
            externalModsList.Items.Clear();
            foreach (KeyValuePair<string, string[]> pair in challenge.externalModifiers)
            {
                externalModsList.Items.Add(pair.Key);
            }
            if (externalModsList.Items.Count > 0) externalModsList.SelectedIndex = 0;
        }
        bool isExtLoading = false;
        private void externalModsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (campaign == null) return;
            Challenge challenge = campaign.challenges[currentChallenge];
            isExtLoading = true;
            textBox1.Text = "";
            foreach (string line in challenge.externalModifiers[(string)externalModsList.SelectedItem])
            {
                textBox1.Text += line + "\r\n";
            }
            textBox1.Text = textBox1.Text.TrimEnd('\n');
            textBox1.Text = textBox1.Text.TrimEnd('\r');
            isExtLoading = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (isExtLoading) return;
            if (campaign == null) return;
            Challenge challenge = campaign.challenges[currentChallenge];
            challenge.externalModifiers.Remove((string)externalModsList.SelectedItem);
            string[] lines = textBox1.Text.Split('\n');
            for(int i = 0; i < lines.Count(); i++)
            {
                lines[i] = lines[i].Trim('\r');
            }
            challenge.externalModifiers.Add((string)externalModsList.SelectedItem,lines);
        }


        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Width = 1000, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }


        //MAP STUFF
        public enum MapState
        {
            ADD,CONNECT,CONNECTING,DISCONNECT,DISCONNECTING,MOVE,MOVING,EDIT,ADD_GATE,REMOVE_GATES,EDIT_GATES,MOVE_GATES,MOVING_GATE
        }
        private bool CanSwitchState()
        {
            return currentState != MapState.MOVING && currentState != MapState.ADD && currentState != MapState.CONNECTING && currentState != MapState.DISCONNECTING && currentState != MapState.ADD_GATE && currentState != MapState.MOVING_GATE;
        }
        MapState currentState = MapState.EDIT;
        private void setState(MapState newState)
        {
            currentState = newState;
            mapState.Text = "State: " + currentState.ToString();
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (updatingCampaign) return;
            UpdateMapHeight((int)numericUpDown1.Value);
        }
        public void UpdateMapHeight(int height)
        {
            campaign.info.mapHeight = height;
            mapArea.Height = height;
        }

        Pen pen = new Pen(Brushes.Red, 5);

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (backgroundBitmap != null)
            {
                int renderedHeight = mapArea.Width * backgroundBitmap.Height / backgroundBitmap.Width;
                g.DrawImage(backgroundBitmap, 0, mapArea.Height-renderedHeight, mapArea.Width, renderedHeight);
            }
            pen.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            foreach(NodeButton node in nodes)
            {
                foreach(NodeButton childNode in node.children)
                {
                    int closestPointX = Math.Max(childNode.Location.X - childNode.Width / 2, Math.Min(node.Location.X, childNode.Location.X + childNode.Width / 2));
                    int closestPointY = Math.Max(childNode.Location.Y - childNode.Height / 2, Math.Min(node.Location.Y, childNode.Location.Y + childNode.Height / 2));
                    g.DrawLine(pen, closestPointX+childNode.Width/2, closestPointY + childNode.Height / 2, node.Location.X + node.Width / 2, node.Location.Y + node.Height / 2);
                }
            }
        }

        NodeButton currentNode;
        List<NodeButton> nodes = new List<NodeButton>();
        public void PrepareMap()
        {
            foreach (NodeButton node in nodes)
            {
                node.Parent.Controls.Remove(node);
            }
            foreach (GateButton gate in gates)
            {
                gate.Parent.Controls.Remove(gate);
            }
            nodes.Clear();
            gates.Clear();
            UpdateMapHeight(campaign.info.mapHeight);
            foreach (CampainMapPosition mapPos in campaign.info.mapPositions)
            {
                currentNode = new NodeButton();
                mapArea.Controls.Add(currentNode);
                currentNode.mapPosition = mapPos;
                currentNode.Location = new Point((int)mapPos.x - currentNode.Width / 2 + (currentNode.Parent.Bounds.Width) / 2, (int)mapPos.y - currentNode.Height / 2);
                currentNode.Text = nodes.Count + "";
                currentNode.Click += clickNode;
                nodes.Add(currentNode);
            }
            foreach (CampaignUnlockGate gate in campaign.info.unlockGate)
            {
                currentGate = new GateButton();
                mapArea.Controls.Add(currentGate);
                currentGate.unlockGate = gate;
                currentGate.Location = new Point((int)gate.x - currentGate.Width / 2 + (currentGate.Parent.Bounds.Width) / 2, (int)gate.y - currentGate.Height / 2);
                currentGate.Text = "UNLOCK GATE";
                currentGate.Click += clickGate;
                gates.Add(currentGate);
            }
            foreach (NodeButton node in nodes)
            {
                List<NodeButton> children = new List<NodeButton>();
                foreach(int i in node.mapPosition.childNodes)
                {
                    children.Add(nodes[i]);
                }
                node.children = children;
            }
            mapArea.Refresh();
        }

        private void addNode_Click(object sender, EventArgs e)
        {
            if (!CanSwitchState()) return;
            if (campaign.info.mapPositions.Count >= campaign.challenges.Count)
            {
                ShowDialog("You cannot have more nodes than challenges, if you wish to add more ndoes, add the challenges for those ndoes first." , "Error");
                return;
            }
            setState(MapState.ADD);
            currentNode = new NodeButton();
            currentNode.Text = nodes.Count+"";
            mapArea.Controls.Add(currentNode);
            currentNode.Click += placeNode;
            //this.Controls.Add(button);
        }

        private void placeNode(object sender, EventArgs e)
        {
            if (currentState != MapState.ADD) return;
            currentNode.Click -= placeNode;
            currentNode.Click += clickNode;
            nodes.Add(currentNode);
            campaign.info.mapPositions.Add(currentNode.mapPosition);
            setState(MapState.EDIT);
            mapArea.Refresh();
        }
        private void clickNode(object sender, EventArgs e)
        {
            switch (currentState)
            {
                case MapState.CONNECT:
                    currentNode = (NodeButton)sender;
                    setState(MapState.CONNECTING);
                    break;
                case MapState.CONNECTING:
                    if (sender == currentNode) return;
                    currentNode.children.Add((NodeButton)sender);
                    currentNode.UpdateChildren(nodes);
                    setState(MapState.CONNECT);
                    mapArea.Refresh();
                    break;
                case MapState.DISCONNECT:
                    currentNode = (NodeButton)sender;
                    setState(MapState.DISCONNECTING);
                    break;
                case MapState.DISCONNECTING:
                    currentNode.children.Remove((NodeButton)sender);
                    currentNode.UpdateChildren(nodes);
                    setState(MapState.DISCONNECT);
                    mapArea.Refresh();
                    break;
                case MapState.MOVE:
                    currentNode = (NodeButton)sender;
                    setState(MapState.MOVING);
                    break;
                case MapState.MOVING:
                    setState(MapState.MOVE);
                    break;
                case MapState.EDIT:
                    new FormEditNode(((NodeButton)sender)).ShowDialog();
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (currentState == MapState.ADD || currentState == MapState.MOVING) currentNode.SetPosition(new Point(MousePosition.X - Bounds.Location.X - 10 - mapArea.Bounds.Location.X - panel1.Location.X - groupBox2.Location.X - tabControl1.Location.X - currentNode.Width / 2, MousePosition.Y - Bounds.Location.Y - 70 - mapArea.Bounds.Location.Y - panel1.Location.Y - groupBox2.Location.Y - tabControl1.Location.Y + currentNode.Height / 2));
            if (currentState == MapState.ADD_GATE || currentState == MapState.MOVING_GATE) currentGate.SetPosition(new Point(MousePosition.X - Bounds.Location.X - 10 - mapArea.Bounds.Location.X - panel1.Location.X - groupBox2.Location.X - tabControl1.Location.X - currentGate.Width / 2, MousePosition.Y - Bounds.Location.Y - 70 - mapArea.Bounds.Location.Y - panel1.Location.Y - groupBox2.Location.Y - tabControl1.Location.Y + currentGate.Height / 2));
            if (currentState == MapState.MOVING) mapArea.Refresh();
        }

        private void connectNodes_Click(object sender, EventArgs e)
        {
            if (!CanSwitchState()) return;
            setState(MapState.CONNECT);
        }

        private void disconnectNodes_Click(object sender, EventArgs e)
        {
            if (!CanSwitchState()) return;
            setState(MapState.DISCONNECT);
        }

        private void moveNodes_Click(object sender, EventArgs e)
        {
            if (!CanSwitchState()) return;
            setState(MapState.MOVE);
        }

        private void editNode_Click(object sender, EventArgs e)
        {
            if (!CanSwitchState()) return;
            setState(MapState.EDIT);
        }
        GateButton currentGate = new GateButton();
        List<GateButton> gates = new List<GateButton>();
        private void addGate_Click(object sender, EventArgs e)
        {
            if (!CanSwitchState()) return;
            setState(MapState.ADD_GATE);
            currentGate = new GateButton();
            currentGate.Text = "UNLOCK GATE";
            mapArea.Controls.Add(currentGate);
            currentGate.Click += placeGate;
        }

        private void placeGate(object sender, EventArgs e)
        {
            if (currentState != MapState.ADD_GATE) return;
            currentGate.Click -= placeGate;
            currentGate.Click += clickGate;
            gates.Add(currentGate);
            campaign.info.unlockGate.Add(currentGate.unlockGate);
            setState(MapState.EDIT_GATES);
            mapArea.Refresh();
        }
        private void clickGate(object sender, EventArgs e)
        {
            switch (currentState)
            {
                case MapState.REMOVE_GATES:
                    gates.Remove((GateButton)sender);
                    mapArea.Controls.Remove((GateButton)sender);
                    campaign.info.unlockGate.Remove(((GateButton)sender).unlockGate);
                    break;
                case MapState.MOVE_GATES:
                    currentGate = (GateButton)sender;
                    setState(MapState.MOVING_GATE);
                    break;
                case MapState.MOVING_GATE:
                    setState(MapState.MOVE_GATES);
                    break;
                case MapState.EDIT_GATES:
                    new FormEditGate(((GateButton)sender).unlockGate).ShowDialog();
                    break;
            }
        }

        private void removeGates_Click(object sender, EventArgs e)
        {
            if (!CanSwitchState()) return;
            setState(MapState.REMOVE_GATES);
        }

        private void editGates_Click(object sender, EventArgs e)
        {
            if (!CanSwitchState()) return;
            setState(MapState.EDIT_GATES);
        }

        private void moveGates_Click(object sender, EventArgs e)
        {
            if (!CanSwitchState()) return;
            setState(MapState.MOVE_GATES);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        
            folderBrowserDialog1 = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, mapArea, new object[] { true });
        }
        //Unlockable stuff
        int curUnlockableIndex;
        bool unlockableUpdating = false;
        private void addUnlockable_Click(object sender, EventArgs e)
        {
            unlockableListBox.Items.Add(unlockableListBox.Items.Count);
            Challenge challenge = campaign.challenges[currentChallenge];
            challenge.unlockableItems.Add(new UnlockableItem());
        }

        private void removeUnlockable_Click(object sender, EventArgs e)
        {
            Challenge challenge = campaign.challenges[currentChallenge];
            challenge.unlockableItems.RemoveAt(curUnlockableIndex);
            unlockableListBox.Items.Clear();
            foreach (UnlockableItem req in challenge.unlockableItems)
            {
                unlockableListBox.Items.Add(unlockableListBox.Items.Count);
            }
            if (unlockableListBox.Items.Count > 0) unlockableListBox.SelectedIndex = 0;
        }

        private void unlockableListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            curUnlockableIndex = unlockableListBox.SelectedIndex;
            SetUnlockableToSelected();
        }
        private void UnlockableValueChanged(object sender, EventArgs e)
        {
            if (unlockableUpdating) return;
            UpdateUnlockableInfo();
        }
        public void UpdateUnlockableInfo()
        {
            if (campaign == null) return;
            Challenge challenge = campaign.challenges[currentChallenge];
            if (challenge.unlockableItems.Count() == 0 || curUnlockableIndex >= challenge.unlockableItems.Count()) return;
            UnlockableItem unlockable = challenge.unlockableItems[curUnlockableIndex];
            unlockable.fileName = unlockableFile.Text;
            unlockable.name = unlockableName.Text;
            unlockable.type = (UnlockableType)unlockableType.SelectedIndex;
        }
        public void SetUnlockableToSelected()
        {
            if (campaign == null) return;
            Challenge challenge = campaign.challenges[currentChallenge];
            if (challenge.unlockableItems.Count() == 0 || curUnlockableIndex >= challenge.unlockableItems.Count()) return;
            unlockableUpdating = true;
            UnlockableItem unlockable = challenge.unlockableItems[curUnlockableIndex];
            unlockableFile.Text = unlockable.fileName;
            unlockableName.Text = unlockable.name;
            unlockableType.SelectedIndex = (int)unlockable.type;
            unlockableUpdating = false;
        }
    }
}
