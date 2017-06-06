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
using Microsoft.Win32;

namespace CopyLetterhead
{
    public partial class Form1 : Form
    {
        string sWFX32;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sWFX32 = GetWFX32();
            lbWFX32.Text = sWFX32;
        }

        //Get WFX32 location
        private string GetWFX32()
        {
            string strWFX32;
            strWFX32 = Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\CCHWinFx", "NetIniLocation", "none").ToString();
            if (strWFX32 == "none")
                strWFX32 = Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\CCHWinFx", "Dir", "none").ToString();
            strWFX32 = strWFX32.TrimEnd('\0');
            if (strWFX32 == "none")
            {
                MessageBox.Show("Please Run Workstation Setup before running this program");
                Application.Exit();
            }
            return strWFX32;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //File Dialog to select file
            OpenFileDialog BrowseFile = new OpenFileDialog();
            string sFile;
            
            BrowseFile.Filter = "Letterhead Form (*.HP)|*.hp";
            BrowseFile.RestoreDirectory = true;
            if (BrowseFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //Filter out Invalid files
                    if ((sFile = BrowseFile.FileName) != null)
                    {
                        sFile = sFile.ToUpper();
                        if (sFile.EndsWith(".HP"))
                        {
                            sFile = sFile.Substring(sFile.LastIndexOf("\\") + 1);
                            if (!sFile.StartsWith("LW"))
                            {
                                MessageBox.Show("Selected file is not a valid Letterhead file");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Selected file is not a Laser Form file");
                            return;
                        }
                    }
                    CopyLetterhead(BrowseFile.FileName);
                    MessageBox.Show("Letterhead Setup Completed");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening file \r\n " + ex.Message);
                }
            }
        }

        private void CopyLetterhead(string File)
        {
            CopyLaserForm(File);
            //Create a TRANSMIT.xml file referencing updated letterhead number
            if (chkOfficeGroup.Checked)
            {
                string FormNumber = File.Substring(File.LastIndexOf("LW") + 2, 4);
                string OfficeGR = sWFX32 + "\\OFFICEGR";
                string Working;
                List<string> Groups = new List<string>();
                foreach (string d in Directory.GetDirectories(OfficeGR))
                {
                    Working = d.Substring(d.LastIndexOf('\\') + 1);
                    Groups.Add(Working);
                }
                //If no directories were found in OFFICEGR something has gone wrong, and exit
                if (Groups.Count == 0)
                {
                    return;
                }
                else
                {
                    //If only 1 Office Group folder, assume correct
                    if (Groups.Count == 1)
                    {
                        Working = Groups[0];
                        Working = OfficeGR + "\\" + Working + "\\TRANSMIT.xml";
                        EditTransmitXML(Working, FormNumber);
                    }
                    else
                    {
                        //Create Dialog prompt to select which office groups to update
                        List<int> Selected = Prompt.GetItem(Groups);
                        if (Selected[0] == -1)
                            return;
                        else
                        {
                            foreach (int i in Selected)
                            {
                                Working = Groups[i];
                                Working = OfficeGR + "\\" + Working + "\\TRANSMIT.xml";
                                EditTransmitXML(Working, FormNumber);
                            }
                        }
                    }
                }
                
            }
        }


        //Get all subdirectories of WFX32, and if Subdirectory contains a LASER folder, as only Product folders should, copy LW file
        private void CopyLaserForm(string FilePath)
        {
            string Destination = FilePath.Substring(FilePath.LastIndexOf('\\'));
            foreach (string d in Directory.GetDirectories(sWFX32))
            {
                foreach (string s in Directory.GetDirectories(d))
                    if (s.EndsWith("\\LASER"))
                    {
                        try
                        {
                            File.Copy(FilePath, s + Destination, chkOverwrite.Checked);
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }                        
                    }
            }
        }

        private void EditTransmitXML(string FilePath, string FormNum)
        {
            try
            {
                //We are only editing one value of the XML file, no need for a full XML parse
                //Read file into output string
                string output;
                if (File.Exists(FilePath))
                {
                    FileStream file = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                    using (StreamReader reader = new StreamReader(file, Encoding.ASCII))
                    {
                        output = reader.ReadToEnd();
                        reader.Close();
                        file.Close();
                    }
                }
                else
                {
                    //Current default value of a brand new TRANSMIT.XML, possibly throw error instead
                    output = "<officeGroupOptions><transmit ogid=\"000\"><headLine1></headLine1><headLine2></headLine2><headLine3></headLine3><headLine4></headLine4><close></close><laserFormNum></laserFormNum><printAllFlag>0</printAllFlag><sepFedStFlag>0</sepFedStFlag><k1LetterCode></k1LetterCode><firmNameFlag>0</firmNameFlag><instrCode></instrCode><instrCodePy></instrCodePy><transLtrReqCode></transLtrReqCode><dba>0</dba><useI>0</useI><dateBlnk>0</dateBlnk><transLetterInGovtCopy>0</transLetterInGovtCopy><prnExtDueDate>0</prnExtDueDate><extPost95></extPost95><year>a</year><instrCode96></instrCode96><coverLetter></coverLetter><filingInstr></filingInstr><dueDateELFFormCode></dueDateELFFormCode><LettersReflectPaperFileWhenEfileDisqualified>0</LettersReflectPaperFileWhenEfileDisqualified><UsePreparedBySectionForCustomFiling>0</UsePreparedBySectionForCustomFiling><IncludeParagraphRefIRSRevenueProcedure2012_17>0</IncludeParagraphRefIRSRevenueProcedure2012_17><SuppressPhoneNumberInK1Letter>0</SuppressPhoneNumberInK1Letter><MovedFromReserved2012></MovedFromReserved2012><DoNotPrepareSeparateLettersForFedForms>0</DoNotPrepareSeparateLettersForFedForms></transmit></officeGroupOptions>";
                }
                //find the one section we care about and add/change the new value
                int index, endindex;
                string line = "<laserFormNum>";
                if (output.Contains(line))
                {
                    index = output.IndexOf(line);
                    endindex = output.IndexOf("</", index + 1);
                    string beginning = output.Substring(0, index);
                    string ending = output.Substring(endindex);
                    line = line + FormNum;
                    output = beginning + line + ending;
                }
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    writer.Write(output);
                    writer.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
    }

    class Prompt
    {
        public static List<int> GetItem(List<string> Groups)
        {
            Form prompt = new Form()
            {
                Width = 198,
                Height = 147,
                FormBorderStyle = FormBorderStyle.FixedToolWindow,
                StartPosition = FormStartPosition.CenterParent
            };
            Label lbApp = new Label()
            {
                Left = 10,
                Top = 14,
                Text = "Select Office Groups to update :",
                AutoSize = true
            };
            Button btAccept = new Button()
            {
                Left = 16,
                Width = 68,
                Top = 50 + (Groups.Count * 20),
                Text = "OK",
            };
            Button btCancel = new Button()
            {
                Left = 96,
                Width = 75,
                Top = 50 + (Groups.Count * 20),
                Text = "Cancel",
            };
            List<CheckBox> chkOfficeGroup = new List<CheckBox>();
            for (int i = 0; i < Groups.Count; i++)
            {
                chkOfficeGroup.Add(new CheckBox()
                {
                    Left = 16,
                    Top = 36 + (i * 20),
                    Text = Groups[i]
                });
                prompt.Controls.Add(chkOfficeGroup[i]);
            }
            prompt.Height = 130 + (Groups.Count * 20);
            btCancel.DialogResult = DialogResult.Cancel;
            btAccept.DialogResult = DialogResult.OK;
            prompt.Controls.Add(lbApp);
            prompt.Controls.Add(btAccept);
            prompt.Controls.Add(btCancel);

            prompt.CancelButton = btCancel;
            prompt.AcceptButton = btAccept;

            DialogResult dgResult = prompt.ShowDialog();

            List<int> GroupsChecked = new List<int>();

            if (dgResult == DialogResult.OK)
            {
                GroupsChecked.Clear();
                for(int i = 0; i < chkOfficeGroup.Count; i++)
                {
                    if (chkOfficeGroup[i].Checked)
                        GroupsChecked.Add(i);
                }
                if (GroupsChecked.Count == 0)
                    GroupsChecked.Add(-1);
                return GroupsChecked;
            }
            else
            {
                GroupsChecked.Clear();
                GroupsChecked.Add(-1);
                return GroupsChecked;
            }
        }
    }
}
