using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Diplom.Krasnov__WindowsForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e) 
        {
            CloseButton.ForeColor = Color.MediumVioletRed;  //При наведении на закрыть, меняется цвет Х
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Red; //Убираем мышь со значка закрыть, меняем на другой цает
        }

        //Подключаем msSQL
     //   DB db = new DB();
     //   SqlCommand command = new SqlCommand("SELECT title, FIO, year, genre FROM Books INNER JOIN Authorship ON Books.id = Authorship.id_books JOIN Authors ON Authors.id = Authorship.id_authors", db.getConnection()); 


    }
}
