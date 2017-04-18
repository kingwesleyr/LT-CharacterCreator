using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace CharacterCreator.Forms
{
    public partial class Dashboard : Form
    {
        private int _CharacterPoints;
        private int CharacterPoints
        {
            get
            {
                return _CharacterPoints;
            }
            set
            {
                _CharacterPoints = value;
                label2.Text = value.ToString();
            }
        }
        public static Dictionary<string, int> CS = new Dictionary<String, int>();
        public List<OccupationalSkill> OSkills { get; set; }
        Character Char = new Character();

        string Characterpath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Lorien Trust Characters\\";

        public Dashboard()
        {
            InitializeComponent();
            CharacterPoints = 16;
            Char.Skills = new List<string>();
            Char.OccupationalSkills = new List<OccupationalSkill>();
            comboBoxFaction.DataSource = Enum.GetValues(typeof(Faction));
            Sqlite sqlite = new Sqlite();
            OSkills = sqlite.GetSkills();
            multiColumnComboBoxOS.DataSource = OSkills;
            multiColumnComboBoxOS.DisplayMember = "Name";
            multiColumnComboBoxOS.ValueMember = "ID";
            multiColumnComboBoxOS.SelectedIndex = -1;
            comboBoxFaction.SelectedIndex = -1;
            GetCharacterList();
        }

        private void GetCharacterList()
        {
            if (Directory.Exists(Characterpath))
            {
                comboBoxCharacters.Visible = true;
                List<String> files = new List<string>();
                foreach (string str in Directory.GetFiles(Characterpath).Select(Path.GetFileName))
                    files.Add(str.Replace(".xml", ""));

                comboBoxCharacters.DataSource = files;
            }
            else
                comboBoxCharacters.Visible = false;

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void SkillChange()
        {
            int total = 16;
            foreach (KeyValuePair<string,int> cs in CS)
                total = total - (int)cs.Value;
            CharacterPoints = total;

            if (total < 8)
            {
                checkBoxBodyDev2.Enabled = checkBoxBodyDev2.Checked;
                checkBoxHealing2.Enabled = checkBoxHealing2.Checked;
                checkBoxSpell2.Enabled = checkBoxSpell2.Checked;
                checkBoxIncant2.Enabled = checkBoxIncant2.Checked;
                checkBoxPower4.Enabled = checkBoxPower4.Checked;
            }
            else
            {
                checkBoxBodyDev2.Enabled = !checkBoxBodyDev1.Checked;
                checkBoxHealing2.Enabled = !(checkBoxHealing1.Checked || checkBoxSpell2.Checked || checkBoxIncant2.Checked); 
                checkBoxIncant2.Enabled = !(checkBoxHealing2.Checked || checkBoxSpell2.Checked || checkBoxIncant1.Checked);
                checkBoxSpell2.Enabled = !(checkBoxHealing2.Checked || checkBoxSpell1.Checked || checkBoxIncant2.Checked);
                checkBoxPower4.Enabled = !(checkBoxPower2.Checked || checkBoxPower3.Checked || checkBoxPower1.Checked);
            }
            if (total < 6)
            {
                checkBoxPower3.Enabled = checkBoxPower3.Checked;
                checkBoxRitual3.Enabled = checkBoxRitual3.Checked;
            }
            else
            {
                checkBoxPower3.Enabled = !(checkBoxPower2.Checked || checkBoxPower1.Checked || checkBoxPower4.Checked);
                checkBoxRitual3.Enabled = !(checkBoxRitual2.Checked || checkBoxRitual1.Checked);
            }
            if (total < 4)
            {
                checkBoxBodyDev1.Enabled = checkBoxBodyDev1.Checked;
                checkBoxHeavyArmourUse.Enabled = checkBoxHeavyArmourUse.Checked;
                checkBoxProjectileWeapon.Enabled = checkBoxProjectileWeapon.Checked;
                checkBoxHealing1.Enabled = checkBoxHealing1.Checked;
                checkBoxIncant1.Enabled = checkBoxIncant1.Checked;
                checkBoxSpell1.Enabled = checkBoxSpell1.Checked;
                checkBoxRitual2.Enabled = checkBoxRitual2.Checked;
                checkBoxPower2.Enabled = checkBoxPower2.Checked;
                checkBoxPoisonLore.Enabled = checkBoxPoisonLore.Checked;
            }
            else
            {
                checkBoxBodyDev1.Enabled = !checkBoxBodyDev2.Checked;
                checkBoxHeavyArmourUse.Enabled = !(checkBoxLightArmourUse.Checked || checkBoxMediumArmourUse.Checked);
                checkBoxProjectileWeapon.Enabled = true;
                checkBoxHealing1.Enabled = !checkBoxHealing2.Checked;
                checkBoxIncant1.Enabled = !checkBoxIncant2.Checked;
                checkBoxSpell1.Enabled = !checkBoxSpell2.Checked;
                checkBoxRitual2.Enabled = !(checkBoxRitual1.Checked || checkBoxRitual3.Checked);
                checkBoxPower2.Enabled = !(checkBoxPower1.Checked || checkBoxPower3.Checked || checkBoxPower4.Checked);
                checkBoxPoisonLore.Enabled = true;
            }
            if (total < 3)
            {
                checkBoxMediumArmourUse.Enabled = checkBoxMediumArmourUse.Checked;
                checkBoxInvocation.Enabled = checkBoxInvocation.Checked;
                checkBoxPotionLore.Enabled = checkBoxPotionLore.Checked;
            }
            else
            {
                checkBoxMediumArmourUse.Enabled = !(checkBoxLightArmourUse.Checked || checkBoxHeavyArmourUse.Checked);
                checkBoxInvocation.Enabled = true;
                checkBoxPotionLore.Enabled = true;
            }
            if (total < 2)
            {
                checkBoxAmbidexterity.Enabled = checkBoxAmbidexterity.Checked;
                checkBoxLargeWeaponUse.Enabled = checkBoxLargeWeaponUse.Checked;
                checkBoxShieldUse.Enabled = checkBoxShieldUse.Checked;
                checkBoxLightArmourUse.Enabled = checkBoxLightArmourUse.Checked;
                checkBoxRitual1.Enabled = checkBoxRitual1.Checked;
                checkBoxPower1.Enabled = checkBoxPower1.Checked;
                checkBoxPhysician.Enabled = checkBoxPhysician.Checked;
            }
            else
            {
                checkBoxAmbidexterity.Enabled = true;
                checkBoxLargeWeaponUse.Enabled = true;
                checkBoxShieldUse.Enabled = true;
                checkBoxLightArmourUse.Enabled = !(checkBoxMediumArmourUse.Checked || checkBoxHeavyArmourUse.Checked);
                checkBoxRitual1.Enabled = !(checkBoxRitual2.Checked || checkBoxRitual3.Checked);
                checkBoxPower1.Enabled = !(checkBoxPower2.Checked || checkBoxPower3.Checked || checkBoxPower4.Checked);
                checkBoxPhysician.Enabled = true;
            }
            if (total < 1)
            {
                checkBoxThrownWeapon.Enabled = checkBoxThrownWeapon.Checked;
                checkBoxContribute.Enabled = checkBoxContribute.Checked;
                checkBoxBindWounds.Enabled = checkBoxBindWounds.Checked;
                checkBoxRecogniseForgery.Enabled = checkBoxRecogniseForgery.Checked;
                checkBoxEvaluate.Enabled = checkBoxEvaluate.Checked;
                checkBoxSenseMagic.Enabled = checkBoxSenseMagic.Checked;
                checkBoxCartography.Enabled = checkBoxCartography.Checked;
            }
            else
            {
                checkBoxThrownWeapon.Enabled = true;
                checkBoxContribute.Enabled = true;
                checkBoxBindWounds.Enabled = true;
                checkBoxRecogniseForgery.Enabled = true;
                checkBoxEvaluate.Enabled = true;
                checkBoxSenseMagic.Enabled = true;
                checkBoxCartography.Enabled = true;
            } 
        }

        #region Armour Skills

        private void checkBoxBodyDev1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBodyDev1.Checked)
                CS.Add("Body Development",Options.CharacterSkills["Body Development"]);
            else
                CS.Remove("Body Development");
            SkillChange();
        }

        private void checkBoxBodyDev2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBodyDev2.Checked)
                CS.Add("Body Development 2",Options.CharacterSkills["Body Development 2"]);
            else
                CS.Remove("Body Development 2");
            SkillChange();
        }

        private void checkBoxLightArmourUse_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLightArmourUse.Checked)
                CS.Add("Light Armour Use",Options.CharacterSkills["Light Armour Use"]);
            else
                CS.Remove("Light Armour Use");
            SkillChange();
        }

        private void checkBoxMediumArmourUse_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMediumArmourUse.Checked)
                CS.Add("Medium Armour Use",Options.CharacterSkills["Medium Armour Use"]);
            else
                CS.Remove("Medium Armour Use");
            SkillChange();
        }

        private void checkBoxHeavyArmourUse_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHeavyArmourUse.Checked)
                CS.Add("Heavy Armour Use",Options.CharacterSkills["Heavy Armour Use"]);
            else
                CS.Remove("Heavy Armour Use");
            SkillChange();
        }

        #endregion

        #region Weapon Skills

        private void checkBoxAmbidexterity_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAmbidexterity.Checked)
                CS.Add("Ambidexterity",Options.CharacterSkills["Amidexterity"]);
            else
                CS.Remove("Ambidexterity");
            SkillChange();
        }

        private void checkBoxLargeWeaponUse_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLargeWeaponUse.Checked)
                CS.Add("Large Weapon Use",Options.CharacterSkills["Large Weapon Use"]);
            else
                CS.Remove("Large Weapon Use");
            SkillChange();
        }

        private void checkBoxProjectileWeapon_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxProjectileWeapon.Checked)
                CS.Add("Projectile Weapon",Options.CharacterSkills["Projectile Weapon"]);
            else
                CS.Remove("Projectile Weapon");
            SkillChange();
        }

        private void checkBoxShieldUse_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShieldUse.Checked)
                CS.Add("Shield Use",Options.CharacterSkills["Shield Use"]);
            else
                CS.Remove("Shield Use");
            SkillChange();
        }

        private void checkBoxThrownWeapon_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxThrownWeapon.Checked)
                CS.Add("Thrown Weapon",Options.CharacterSkills["Thrown Weapon"]);
            else
                CS.Remove("Thrown Weapon");
            SkillChange();
        }

#endregion

        #region Magic Skills

        private void checkBoxHealing2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHealing2.Checked)
                CS.Add("Healing 2",Options.CharacterSkills["Healing 2"]);
            else
                CS.Remove("Healing 2");
            SkillChange();
        }

        private void checkBoxHealing1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHealing1.Checked)
                CS.Add("Healing", Options.CharacterSkills["Healing"]);
            else
                CS.Remove("Healing");
            SkillChange();
        }

        private void checkBoxIncant2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIncant2.Checked)
                CS.Add("Incantation 2",Options.CharacterSkills["Incantation 2"]);
            else
                CS.Remove("Incantation 2");
            SkillChange();
        }

        private void checkBoxIncant1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIncant1.Checked)
                CS.Add("Incantation",Options.CharacterSkills["Incantation"]);
            else
                CS.Remove("Incantation");
            SkillChange();
        }

        private void checkBoxSpell2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSpell2.Checked)
                CS.Add("Spellcasting 2",Options.CharacterSkills["Spellcasting 2"]);
            else
                CS.Remove("Spellcasting 2");
            SkillChange();
        }

        private void checkBoxSpell1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSpell1.Checked)
                CS.Add("Spellcasting",Options.CharacterSkills["Spellcasting"]);
            else
                CS.Remove("Spellcasting");
            SkillChange();
        }

        private void checkBoxRitual1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRitual1.Checked)
                CS.Add("Ritual",Options.CharacterSkills["Ritual"]);
            else
                CS.Remove("Ritual");
            SkillChange();
        }

        private void checkBoxRitual2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRitual2.Checked)
                CS.Add("Ritual 2",Options.CharacterSkills["Ritual 2"]);
            else
                CS.Remove("Ritual 2");
            SkillChange();
        }

        private void checkBoxRitual3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRitual3.Checked)
                CS.Add("Ritual 3",Options.CharacterSkills["Ritual 3"]);
            else
                CS.Remove("Ritual 3");
            SkillChange();
        }

        private void checkBoxContribute_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxContribute.Checked)
                CS.Add("Contribute",Options.CharacterSkills["Contribute"]);
            else
                CS.Remove("Contribute");
            SkillChange();
        }

        private void checkBoxPower1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPower1.Checked)
                CS.Add("Power 4",Options.CharacterSkills["Power 4"]);
            else
                CS.Remove("Power 4");
            SkillChange();
        }

        private void checkBoxPower2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPower2.Checked)
                CS.Add("Power 8",Options.CharacterSkills["Power 8"]);
            else
                CS.Remove("Power 8");
            SkillChange();
        }

        private void checkBoxPower3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPower3.Checked)
                CS.Add("Power 12",Options.CharacterSkills["Power 12"]);
            else
                CS.Remove("Power 12");
            SkillChange();
        }

        private void checkBoxPower4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPower4.Checked)
                CS.Add("Power 16",Options.CharacterSkills["Power 16"]);
            else
                CS.Remove("Power 16");
            SkillChange();
        }

        private void checkBoxInvocation_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxInvocation.Checked)
                CS.Add("Invocation",Options.CharacterSkills["Invocation"]);
            else
                CS.Remove("Invocation");
            SkillChange();
        }

#endregion

        #region Knowledge Skills

        private void checkBoxPotionLore_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPotionLore.Checked)
                CS.Add("Potion Lore",Options.CharacterSkills["Potion Lore"]);
            else
                CS.Remove("Potion Lore");
            SkillChange();
        }

        private void checkBoxPoisonLore_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPoisonLore.Checked)
                CS.Add("Poison Lore",Options.CharacterSkills["Poison Lore"]);
            else
                CS.Remove("Poison Lore");
            SkillChange();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBoxCartography_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCartography.Checked)
                CS.Add("Cartography",Options.CharacterSkills["Cartography"]);
            else
                CS.Remove("Cartography");
            SkillChange();
        }

        private void checkBoxSenseMagic_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSenseMagic.Checked)
                CS.Add("Sense Magic",Options.CharacterSkills["Sense Magic"]);
            else
                CS.Remove("Sense Magic");
            SkillChange();
        }

        private void checkBoxEvaluate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEvaluate.Checked)
                CS.Add("Evaluate",Options.CharacterSkills["Evaluate"]);
            else
                CS.Remove("Evaluate");
            SkillChange();
        }

        private void checkBoxRecogniseForgery_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRecogniseForgery.Checked)
                CS.Add("Recognise Forgery",Options.CharacterSkills["Recognise Forgery"]);
            else
                CS.Remove("Recognise Forgery");
            SkillChange();
        }

        private void checkBoxPhysician_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPhysician.Checked)
                CS.Add("Physician",Options.CharacterSkills["Physician"]);
            else
                CS.Remove("Physician");
            SkillChange();
        }

        private void checkBoxBindWounds_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBindWounds.Checked)
                CS.Add("Bind Wounds",Options.CharacterSkills["Bind Wounds"]);
            else
                CS.Remove("Bind Wounds");
            SkillChange();
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in groupBox1.Controls)
                if (ctrl is CheckBox)
                    ((CheckBox)ctrl).Checked = false;
            foreach (DataGridViewRow dgv in dataGridView1.Rows)
                dataGridView1.Rows.Remove(dgv);
            textBoxName.Text = string.Empty;
            comboBoxFaction.SelectedIndex = -1;
            textBoxRace.Text = string.Empty;
            textBoxSpecialNotes.Text = string.Empty;
            multiColumnComboBoxOS.SelectedIndex = -1;
        }

        private void buttonXML_Click(object sender, EventArgs e)
        {
            Char.Name = textBoxName.Text;
            Char.SpecialNotes = textBoxSpecialNotes.Text;
            Char.Race = textBoxRace.Text;
            Char.Faction = (Faction)comboBoxFaction.SelectedItem;
            Char.Skills = new List<string>();
            foreach (KeyValuePair<string, int> key in CS)
                Char.Skills.Add(key.Key);
            if (!Directory.Exists(Characterpath))
                Directory.CreateDirectory(Characterpath);
            XmlSerializer ser = new XmlSerializer(typeof(Character));
            using (TextWriter writer = new StreamWriter(Characterpath + Char.Name + ".xml"))
            {
                ser.Serialize(writer,Char);
            }
            GetCharacterList();
        }

        private void buttonAddOS_Click(object sender, EventArgs e)
        {
            OccupationalSkill os = OSkills.FirstOrDefault(o => o.ID == (int)multiColumnComboBoxOS.SelectedValue);
            Char.OccupationalSkills.Add(os);
            dataGridView1.Rows.Insert(dataGridView1.Rows.Count, os.Name,string.Empty);
            multiColumnComboBoxOS.SelectedIndex = -1;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                Char.OccupationalSkills.RemoveAll(o => o.Name == dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                dataGridView1.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Character));
            Char = (Character)xml.Deserialize(new StreamReader(Characterpath + comboBoxCharacters.SelectedValue.ToString() + ".xml"));
            button1.PerformClick();
            textBoxName.Text = Char.Name;
            comboBoxFaction.SelectedItem = Char.Faction;
            textBoxSpecialNotes.Text = Char.SpecialNotes;
            textBoxRace.Text = Char.Race;
            foreach (string str in Char.Skills)
                switch (str)
                {
                    case ("Body Development"):
                        checkBoxBodyDev1.Checked = true;
                        break;
                    case ("Body Development 2"):
                        checkBoxBodyDev2.Checked = true;
                        break;
                    case ("Light Armour Use"):
                        checkBoxLightArmourUse.Checked = true;
                        break;
                    case ("Medium Armour Use"):
                        checkBoxMediumArmourUse.Checked = true;
                        break;
                    case ("Heavy Armour Use"):
                        checkBoxHeavyArmourUse.Checked = true;
                        break;
                    case ("Shield Use"):
                        checkBoxShieldUse.Checked = true;
                        break;
                    case ("Large Weapon Use"):
                        checkBoxLargeWeaponUse.Checked = true;
                        break;
                    case ("Projectile Weapon"):
                        checkBoxProjectileWeapon.Checked = true;
                        break;
                    case ("Thrown Weapon"):
                        checkBoxThrownWeapon.Checked = true;
                        break;
                    case ("Ambidexterity"):
                        checkBoxAmbidexterity.Checked = true;
                        break;
                    case ("Healing"):
                        checkBoxHealing1.Checked = true;
                        break;
                    case ("Healing 2"):
                        checkBoxHealing2.Checked = true;
                        break;
                    case ("Incantation"):
                        checkBoxIncant1.Checked = true;
                        break;
                    case ("Incantation 2"):
                        checkBoxIncant2.Checked = true;
                        break;
                    case ("Spellcasting"):
                        checkBoxSpell1.Checked = true;
                        break;
                    case ("Spellcasting 2"):
                        checkBoxSpell2.Checked = true;
                        break;
                    case ("Ritual"):
                        checkBoxRitual1.Checked = true;
                        break;
                    case ("Ritual 2"):
                        checkBoxRitual2.Checked = true;
                        break;
                    case ("Ritual 3"):
                        checkBoxRitual3.Checked = true;
                        break;
                    case ("Contribute"):
                        checkBoxContribute.Checked = true;
                        break;
                    case ("Power 4"):
                        checkBoxPower1.Checked = true;
                        break;
                    case ("Power 8"):
                        checkBoxPower2.Checked = true;
                        break;
                    case ("Power 12"):
                        checkBoxPower3.Checked = true;
                        break;
                    case ("Power 16"):
                        checkBoxPower4.Checked = true;
                        break;
                    case ("Invocation"):
                        checkBoxInvocation.Checked = true;
                        break;
                    case ("Potion Lore"):
                        checkBoxPotionLore.Checked = true;
                        break;
                    case ("Poison Lore"):
                        checkBoxPoisonLore.Checked = true;
                        break;
                    case ("Cartography"):
                        checkBoxCartography.Checked = true;
                        break;
                    case ("Sense Magic"):
                        checkBoxSenseMagic.Checked = true;
                        break;
                    case ("Evaluate"):
                        checkBoxEvaluate.Checked = true;
                        break;
                    case ("Recognise Forgery"):
                        checkBoxRecogniseForgery.Checked = true;
                        break;
                    case ("Physician"):
                        checkBoxPhysician.Checked = true;
                        break;
                    case ("Bind Wounds"):
                        checkBoxBindWounds.Checked = true;
                        break;
                }
            foreach (OccupationalSkill os in Char.OccupationalSkills) dataGridView1.Rows.Insert(dataGridView1.Rows.Count, os.Name, string.Empty);
        }

        private void moreInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OccupationalSkill os = OSkills.FirstOrDefault(o => o.Name == dataGridView1.Rows[selectedrow].Cells[0].Value.ToString());
            string text = string.Format("Cost: {0}{1}{2}{3}",os.Cost,Environment.NewLine, Environment.NewLine, Regex.Replace(os.Description, @"\t|\n|\r", ""));
            MessageBox.Show(text,os.Name,MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        int selectedrow = 0;

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            var dgv_rel_mousePos = dataGridView1.PointToClient(Control.MousePosition);
            var hti = dataGridView1.HitTest(dgv_rel_mousePos.X, dgv_rel_mousePos.Y);
            selectedrow = hti.RowIndex;
            if (hti.RowIndex == -1)
                e.Cancel = true;
        }
    }
}
