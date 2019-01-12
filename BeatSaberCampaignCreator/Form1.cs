using BeatSaberDailyChallenges;
using BeatSaberDailyChallenges.campaign;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PlayerDataModelSaveData.GameplayModifiers;

namespace BeatSaberCampaignCreator
{
    public partial class Form1 : Form
    {
        Campaign campaign = null;
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
            challengeDesc.Text = challenge.desc;
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
            speedMul.Value = (decimal)challenge.modifiers.speedMul;
            energyType.SelectedIndex = (int)challenge.modifiers.energyType;
            enabledObstacles.SelectedIndex = (int)challenge.modifiers.enabledObstacleType;


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
            challenge.desc = challengeDesc.Text;
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
            challenge.modifiers.speedMul = (float)speedMul.Value;
            challenge.modifiers.energyType = (GameplayModifiers.EnergyType)energyType.SelectedIndex;
            challenge.modifiers.enabledObstacleType = (GameplayModifiers.EnabledObstacleType)enabledObstacles.SelectedIndex;
        }
        bool updatingCampaign = false;
        public void SetCampaignInfoData()
        {
            updatingCampaign = true;
            campaignName.Text = campaign.info.name;
            campaignDesc.Text = campaign.info.desc;
            allUnlocked.Checked = campaign.info.allUnlocked;
            updatingCampaign = false;
        }
        public void UpdateCampaignInfo()
        {
            campaign.info.name = campaignName.Text;
            campaign.info.desc = campaignDesc.Text;
            campaign.info.allUnlocked = allUnlocked.Checked;
        }

        //Header
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowserDialog1.SelectedPath;
                currentDirectory = path;
                campaign = new Campaign();
                campaign.info = new CampaignInfo();
                campaign.info.version = "0.1";
                campaign.challenges = new List<BeatSaberDailyChallenges.Challenge>();
                listBox1.Items.Clear();
                AddChallenge();
                currentChallenge = 0;
                tabControl1.Enabled = true;
                SetCampaignInfoData();
                SetDataToCurrentSelected();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowserDialog1.SelectedPath;
                currentDirectory = path;
                campaign = new Campaign();
                campaign.info = JsonConvert.DeserializeObject<CampaignInfo>(File.ReadAllText(path + "/info.json"));
                campaign.challenges = new List<BeatSaberDailyChallenges.Challenge>();
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
                tabControl1.Enabled = true;
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
    }
}
