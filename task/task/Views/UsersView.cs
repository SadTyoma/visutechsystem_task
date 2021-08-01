using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using task.Controllers;
using task.Models;

namespace task.Views
{
    public partial class UsersView : Form, IUsersView
    {
        public UsersView()
        {
            InitializeComponent();
            radioButtonsave1.Checked = true;
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.DefaultExt = "xml";
            openFileDialog.Filter = "XML Files (*.xml)|*.xml";
            openFileDialog.FilterIndex = 0;
            openFileDialog.DefaultExt = "xml";
        }

        private ListView listView;
        private GroupBox Registration;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private RadioButton radioButtonfemale;
        private RadioButton radioButtonmale;
        private TextBox age;
        private TextBox lastname;
        private TextBox firstname;
        private TextBox id;
        private Label label5;
        private Button addchange;
        private Button remove;
        private SaveFileDialog saveFileDialog;
        private OpenFileDialog openFileDialog;
        private Button savetable;
        private Button opentable;
        private RadioButton radioButtonsave1;
        private RadioButton radioButtonsave2;
        private Button clearregistration;
        UsersController _controller;
        public void SetController(UsersController controller)
        {
            _controller = controller;
        }
        public void AddUserToGrid(User user)
        {
            ListViewItem newuser;
            newuser = listView.Items.Add(user.ID);
            newuser.SubItems.Add(user.FirstName);
            newuser.SubItems.Add(user.LastName);
            newuser.SubItems.Add(user.Age);
            newuser.SubItems.Add(Enum.GetName(typeof(User.SexOfPerson), user.Sexofperson));
        }
        public ListViewItem SearchRow(User user)
        {
            foreach (ListViewItem row in listView.Items)
                if (row.Text == user.ID)
                    return row;
            return null;
        }
        public void UpdateGridWithChangedUser(User user)
        {
            ListViewItem rowToUpdate = SearchRow(user);

            if (rowToUpdate != null)
            {
                rowToUpdate.Text = user.ID;
                rowToUpdate.SubItems[1].Text = user.FirstName;
                rowToUpdate.SubItems[2].Text = user.LastName;
                rowToUpdate.SubItems[3].Text = user.Age;
                rowToUpdate.SubItems[4].Text = Enum.GetName(typeof(User.SexOfPerson), user.Sexofperson);
            }
        }

        public void RemoveUserFromGrid(User user)
        {
            ListViewItem rowToRemove = SearchRow(user);

            if (rowToRemove != null)
            {
                listView.Items.Remove(rowToRemove);
                listView.Focus();
            }
        }

        public string GetIdOfSelectedUserInGrid()
        {
            if (listView.SelectedItems.Count > 0)
                return listView.SelectedItems[0].Text;
            else
                return "";
        }

        public void SetSelectedUserInGrid(User user)
        {
            ListViewItem row = SearchRow(user);
            if (row != null)
                row.Selected = true;
        }

        public string FirstName
        {
            get { return firstname.Text; }
            set { firstname.Text = value; }
        }

        public string LastName
        {
            get { return lastname.Text; }
            set { lastname.Text = value; }
        }

        public string ID
        {
            get { return id.Text; }
            set { id.Text = value; }
        }


        public string Age
        {
            get { return age.Text; }
            set { age.Text = value; }
        }

        public User.SexOfPerson Sexofperson
        {
            get
            {
                if (radioButtonmale.Checked)
                    return User.SexOfPerson.Male;
                else
                    return User.SexOfPerson.Female;
            }
            set
            {
                if (value == User.SexOfPerson.Male)
                    radioButtonmale.Checked = true;
                else
                    radioButtonfemale.Checked = true;
            }
        }
        public void ClearGrid()
        {
            listView.Columns.Clear();

            listView.Columns.Add("Id", 300, HorizontalAlignment.Left);
            listView.Columns.Add("First Name", 300, HorizontalAlignment.Left);
            listView.Columns.Add("Lastst Name", 300, HorizontalAlignment.Left);
            listView.Columns.Add("Age", 150, HorizontalAlignment.Left);
            listView.Columns.Add("Sex", 150, HorizontalAlignment.Left);

            listView.Items.Clear();
        }

        private void InitializeComponent()
        {
            this.listView = new System.Windows.Forms.ListView();
            this.Registration = new System.Windows.Forms.GroupBox();
            this.id = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonfemale = new System.Windows.Forms.RadioButton();
            this.radioButtonmale = new System.Windows.Forms.RadioButton();
            this.age = new System.Windows.Forms.TextBox();
            this.lastname = new System.Windows.Forms.TextBox();
            this.firstname = new System.Windows.Forms.TextBox();
            this.addchange = new System.Windows.Forms.Button();
            this.remove = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.savetable = new System.Windows.Forms.Button();
            this.opentable = new System.Windows.Forms.Button();
            this.radioButtonsave1 = new System.Windows.Forms.RadioButton();
            this.radioButtonsave2 = new System.Windows.Forms.RadioButton();
            this.clearregistration = new System.Windows.Forms.Button();
            this.Registration.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(13, 599);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(1529, 400);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // Registration
            // 
            this.Registration.Controls.Add(this.id);
            this.Registration.Controls.Add(this.label5);
            this.Registration.Controls.Add(this.label4);
            this.Registration.Controls.Add(this.label3);
            this.Registration.Controls.Add(this.label2);
            this.Registration.Controls.Add(this.label1);
            this.Registration.Controls.Add(this.radioButtonfemale);
            this.Registration.Controls.Add(this.radioButtonmale);
            this.Registration.Controls.Add(this.age);
            this.Registration.Controls.Add(this.lastname);
            this.Registration.Controls.Add(this.firstname);
            this.Registration.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.Registration.Location = new System.Drawing.Point(46, 31);
            this.Registration.Name = "Registration";
            this.Registration.Size = new System.Drawing.Size(818, 456);
            this.Registration.TabIndex = 1;
            this.Registration.TabStop = false;
            this.Registration.Text = "Registration";
            // 
            // id
            // 
            this.id.Location = new System.Drawing.Point(208, 390);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(604, 44);
            this.id.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 390);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 37);
            this.label5.TabIndex = 9;
            this.label5.Text = "Id:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 277);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 37);
            this.label4.TabIndex = 8;
            this.label4.Text = "Sex:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 37);
            this.label3.TabIndex = 7;
            this.label3.Text = "Age:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 37);
            this.label2.TabIndex = 6;
            this.label2.Text = "Last Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 37);
            this.label1.TabIndex = 5;
            this.label1.Text = "First Name:";
            // 
            // radioButtonfemale
            // 
            this.radioButtonfemale.AutoSize = true;
            this.radioButtonfemale.Location = new System.Drawing.Point(208, 319);
            this.radioButtonfemale.Name = "radioButtonfemale";
            this.radioButtonfemale.Size = new System.Drawing.Size(148, 41);
            this.radioButtonfemale.TabIndex = 4;
            this.radioButtonfemale.TabStop = true;
            this.radioButtonfemale.Text = "Female";
            this.radioButtonfemale.UseVisualStyleBackColor = true;
            // 
            // radioButtonmale
            // 
            this.radioButtonmale.AutoSize = true;
            this.radioButtonmale.Location = new System.Drawing.Point(208, 277);
            this.radioButtonmale.Name = "radioButtonmale";
            this.radioButtonmale.Size = new System.Drawing.Size(110, 41);
            this.radioButtonmale.TabIndex = 3;
            this.radioButtonmale.TabStop = true;
            this.radioButtonmale.Text = "Male";
            this.radioButtonmale.UseVisualStyleBackColor = true;
            // 
            // age
            // 
            this.age.Location = new System.Drawing.Point(208, 204);
            this.age.Name = "age";
            this.age.Size = new System.Drawing.Size(604, 44);
            this.age.TabIndex = 2;
            // 
            // lastname
            // 
            this.lastname.Location = new System.Drawing.Point(208, 129);
            this.lastname.Name = "lastname";
            this.lastname.Size = new System.Drawing.Size(604, 44);
            this.lastname.TabIndex = 1;
            // 
            // firstname
            // 
            this.firstname.Location = new System.Drawing.Point(208, 51);
            this.firstname.Name = "firstname";
            this.firstname.Size = new System.Drawing.Size(604, 44);
            this.firstname.TabIndex = 0;
            // 
            // addchange
            // 
            this.addchange.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.addchange.Location = new System.Drawing.Point(935, 67);
            this.addchange.Name = "addchange";
            this.addchange.Size = new System.Drawing.Size(241, 55);
            this.addchange.TabIndex = 2;
            this.addchange.Text = "Add/Change";
            this.addchange.UseVisualStyleBackColor = true;
            this.addchange.Click += new System.EventHandler(this.addchange_Click);
            // 
            // remove
            // 
            this.remove.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.remove.Location = new System.Drawing.Point(935, 145);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(241, 55);
            this.remove.TabIndex = 3;
            this.remove.Text = "Remove";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // savetable
            // 
            this.savetable.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.savetable.Location = new System.Drawing.Point(1301, 518);
            this.savetable.Name = "savetable";
            this.savetable.Size = new System.Drawing.Size(241, 55);
            this.savetable.TabIndex = 4;
            this.savetable.Text = "Save Table";
            this.savetable.UseVisualStyleBackColor = true;
            this.savetable.Click += new System.EventHandler(this.savetable_Click);
            // 
            // opentable
            // 
            this.opentable.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.opentable.Location = new System.Drawing.Point(1035, 518);
            this.opentable.Name = "opentable";
            this.opentable.Size = new System.Drawing.Size(241, 55);
            this.opentable.TabIndex = 5;
            this.opentable.Text = "Open Table";
            this.opentable.UseVisualStyleBackColor = true;
            this.opentable.Click += new System.EventHandler(this.opentable_Click);
            // 
            // radioButtonsave1
            // 
            this.radioButtonsave1.AutoSize = true;
            this.radioButtonsave1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.radioButtonsave1.Location = new System.Drawing.Point(1301, 394);
            this.radioButtonsave1.Name = "radioButtonsave1";
            this.radioButtonsave1.Size = new System.Drawing.Size(263, 41);
            this.radioButtonsave1.TabIndex = 6;
            this.radioButtonsave1.TabStop = true;
            this.radioButtonsave1.Text = "Save Scheme 1";
            this.radioButtonsave1.UseVisualStyleBackColor = true;
            // 
            // radioButtonsave2
            // 
            this.radioButtonsave2.AutoSize = true;
            this.radioButtonsave2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.radioButtonsave2.Location = new System.Drawing.Point(1301, 452);
            this.radioButtonsave2.Name = "radioButtonsave2";
            this.radioButtonsave2.Size = new System.Drawing.Size(265, 41);
            this.radioButtonsave2.TabIndex = 7;
            this.radioButtonsave2.TabStop = true;
            this.radioButtonsave2.Text = "Save Scheme 2";
            this.radioButtonsave2.UseVisualStyleBackColor = true;
            // 
            // clearregistration
            // 
            this.clearregistration.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clearregistration.Location = new System.Drawing.Point(935, 235);
            this.clearregistration.Name = "clearregistration";
            this.clearregistration.Size = new System.Drawing.Size(241, 93);
            this.clearregistration.TabIndex = 8;
            this.clearregistration.Text = "Clear Registration";
            this.clearregistration.UseVisualStyleBackColor = true;
            this.clearregistration.Click += new System.EventHandler(this.clearregistration_Click);
            // 
            // UsersView
            // 
            this.ClientSize = new System.Drawing.Size(1898, 1024);
            this.Controls.Add(this.clearregistration);
            this.Controls.Add(this.radioButtonsave2);
            this.Controls.Add(this.radioButtonsave1);
            this.Controls.Add(this.opentable);
            this.Controls.Add(this.savetable);
            this.Controls.Add(this.remove);
            this.Controls.Add(this.addchange);
            this.Controls.Add(this.Registration);
            this.Controls.Add(this.listView);
            this.Name = "UsersView";
            this.Text = "New User Registration";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Registration.ResumeLayout(false);
            this.Registration.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #region Events
        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
                _controller.SelectedUserChanged(listView.SelectedItems[0].Text);
        }

        private void addchange_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^(?!\s|\d*$).+");
            Regex regex2 = new Regex(@"^(?!\s|\D*$).+");
            if (regex2.Match(age.Text).Length != 0 && regex.Match(firstname.Text).Length != 0 && regex.Match(lastname.Text).Length != 0 && regex2.Match(id.Text).Length != 0)
                _controller.Save();
            else
                MessageBox.Show("Неверный ввод", "Ошибка.");
        }
        private void remove_Click(object sender, EventArgs e)
        {
            _controller.RemoveUser();
        }
        private void savetable_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog.FileName;
            try
            {
                if (radioButtonsave1.Checked == true)
                    SerializeDataSet1(filename);
                else
                    SerializeDataSet2(filename);
            }
            catch
            {
                MessageBox.Show("Невозможно сохранить XML файл.", "Ошибка.");
            }

        }
        private void clearregistration_Click(object sender, EventArgs e)
        {
            _controller.ClearView();
        }
        public bool CheckIDForOpen(string id)
        {
            foreach (User _user in _controller.Users)
            {
                if (_user.ID == id)
                {
                    MessageBox.Show($"ID {_user.ID} уже есть в списке", "Ошибка.");
                    return true;
                }
            }
            return false;
        }
        private void opentable_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog.FileName;
            if (File.Exists(filename))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(filename);
                var path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))+"\\Schemes\\XMLSchema1.xsd";
                XmlSchemaSet schema = new XmlSchemaSet();
                schema.Add("", path);
                XmlReader rd = XmlReader.Create(filename);
                XDocument doc = XDocument.Load(rd);
                bool errors = false;
                doc.Validate(schema, (o, ev) => { errors = true; });
                if (!errors)
                {
                    foreach (DataRow item in ds.Tables["User"].Rows)
                    {
                        if (!CheckIDForOpen((string)item["Id"]))
                        {
                            User user = new User(FirstName = (string)item["First_Name"], LastName = (string)item["Last_Name"], ID = (string)item["Id"], Age = (string)item["Age"], Sexofperson = (User.SexOfPerson)Enum.Parse(typeof(User.SexOfPerson), (string)item["Sexofperson"]));
                            _controller.Users.Add(user);
                            AddUserToGrid(user);
                        }
                    }
                }
                else
                {
                    try
                    {
                        foreach (DataTable dt in ds.Tables)
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                if (!CheckIDForOpen((string)item["Id"]))
                                {
                                    User user = new User(FirstName = (string)item["First_Name"], LastName = dt.TableName, ID = (string)item["Id"], Age = (string)item["Age"], Sexofperson = (User.SexOfPerson)Enum.Parse(typeof(User.SexOfPerson), (string)item["Sexofperson"]));
                                    _controller.Users.Add(user);
                                    AddUserToGrid(user);
                                }
                            }
                        }
                    }
                    catch(Exception ev)
                    {
                        MessageBox.Show(ev.ToString(), "Ошибка.");
                    }
                }
                _controller.LoadView();
            }
            else
            {
                MessageBox.Show("XML файл не найден.", "Ошибка.");
            }
        }
        private void SerializeDataSet1(string filename)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.TableName = "User";
            dt.Columns.Add("Id");
            dt.Columns.Add("First_Name");
            dt.Columns.Add("Last_Name");
            dt.Columns.Add("Age");
            dt.Columns.Add("Sexofperson");
            ds.Tables.Add(dt);
            foreach (ListViewItem r in listView.Items)
            {
                DataRow row = ds.Tables["User"].NewRow();
                row["Id"] = r.Text;
                row["First_Name"] = r.SubItems[1].Text;
                row["Last_Name"] = r.SubItems[2].Text;
                row["Age"] = r.SubItems[3].Text;
                row["Sexofperson"] = r.SubItems[4].Text;
                ds.Tables["User"].Rows.Add(row);
            }

            ds.WriteXml(filename, XmlWriteMode.IgnoreSchema);
        }
        private void SerializeDataSet2(string filename)
        {
            DataSet ds = new DataSet();
            foreach(ListViewItem r in listView.Items)
            {
                if (!ds.Tables.Contains(r.SubItems[2].Text))
                {
                    DataTable dt = new DataTable();
                    dt.TableName = r.SubItems[2].Text;
                    dt.Columns.Add("Id");
                    dt.Columns.Add("First_Name");
                    dt.Columns.Add("Age");
                    dt.Columns.Add("Sexofperson");
                    ds.Tables.Add(dt);

                    foreach (ListViewItem l in listView.Items)
                    {
                        if (l.SubItems[2].Text == r.SubItems[2].Text)
                        {
                            DataRow row = ds.Tables[r.SubItems[2].Text].NewRow();
                            row["Id"] = l.Text;
                            row["First_Name"] = l.SubItems[1].Text;
                            row["Age"] = l.SubItems[3].Text;
                            row["Sexofperson"] = l.SubItems[4].Text;
                            ds.Tables[r.SubItems[2].Text].Rows.Add(row);
                        }
                    }
                }
            }
            ds.WriteXml(filename, XmlWriteMode.IgnoreSchema);
        }
        #endregion
    }
}

