using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ADO_NET_ДЗ_Модуль_01_часть_01
{
    public partial class Form1 : Form
    {
        string connString = "";
        public Form1()
        {
            InitializeComponent();
            textBox_host.Text = "localhost";
            textBox_username.Text = "postgres";
            textBox_password.Text = "123";
            textBox_db.Text = "products";
            textBox_table.Text = "public.vegetables_and_fruits";
            MessageBox.Show("Fill in the connection information and click the 'LOAD' button", "Attention",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button_loadDB_Click(object sender, EventArgs e)
        {
            connString = "Host=" + textBox_host.Text + ';' + "Username=" + textBox_username.Text + ';' +
              "Password=" + textBox_password.Text + ";" + "Database=" + textBox_db.Text;
            using (NpgsqlConnection connection = new NpgsqlConnection(connString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Database '" + textBox_db.Text + "' connection successful!", "Connection");
                    NpgsqlCommand npgsqlCommand = connection.CreateCommand();
                    npgsqlCommand.CommandText = "select * from " + textBox_table.Text;
                    NpgsqlDataReader reader = npgsqlCommand.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataSet1.Tables.Add(dt);
                    dataGridView1.AutoGenerateColumns = true;
                    dataGridView1.DataSource = dataSet1.Tables[dataSet1.Tables.Count - 1];
                    npgsqlCommand.Cancel();
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + '\n' + "\tCheck the entered data!", "Error!");
                }
            }
        }

        private void commandDB(string command)
        {
            using (DbDataAdapter dbDataAdapter = new NpgsqlDataAdapter(command, connString))
            {
                try
                {
                    dbDataAdapter.Fill(dataSet1.Tables.Add());
                    dataGridView1.DataSource = dataSet1.Tables[dataSet1.Tables.Count - 1];

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!");
                }
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                commandDB("select _name from vegetables_and_fruits order by _name");
            }
            if (radioButton2.Checked)
            {
                commandDB("select distinct _color from vegetables_and_fruits order by _color");
            }
            if (radioButton3.Checked)
            {
                commandDB("select max(_calorific_value)as \"Maximum calorific value\"  from vegetables_and_fruits");
            }
            if (radioButton4.Checked)
            {
                commandDB("select min(_calorific_value)as \"Minimum calorific value\"  from vegetables_and_fruits");
            }
            if (radioButton5.Checked)
            {
                commandDB("select avg(_calorific_value)as \"Averege calorific value\"  from vegetables_and_fruits");
            }
            if (radioButton6.Checked)
            {
                commandDB("select count(*) as \"Quantity of vegetables\" from vegetables_and_fruits where _type = 'v'");
            }
            if (radioButton7.Checked)
            {
                commandDB("select count(*) as \"Quantity of fruits\" from vegetables_and_fruits where _type = 'f'");
            }
            if (radioButton8.Checked)
            {
                commandDB($"select count(*) as \"Number of fruits and vegetables of the selected color\"" +
                    $" from vegetables_and_fruits where _color = '{textBox_color.Text}'");
            }
            if (radioButton9.Checked)
            {
                commandDB("select count(*)as \"Quantity\", _color" +
                    " from vegetables_and_fruits group by _color");
            }
            if (radioButton10.Checked)
            {
                commandDB("select _name, _calorific_value from vegetables_and_fruits" +
                    $" where _calorific_value < {textBox_calorieBelow.Text}");
            }
            if (radioButton11.Checked)
            {
                commandDB("select _name, _calorific_value from vegetables_and_fruits" +
                    $" where _calorific_value > {textBox_calorieAbove.Text}");
            }
            if (radioButton12.Checked)
            {
                commandDB("select _name, _calorific_value from vegetables_and_fruits" +
                    $" where _calorific_value between {textBox_from.Text} and {textBox_to.Text}");
            }
            if (radioButton13.Checked)
            {
                commandDB("select _name, _color from vegetables_and_fruits where _color = 'red' or _color = 'yellow'");
            }
        }
    }
}
 