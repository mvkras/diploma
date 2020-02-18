using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;   //Подключаем mssql
using System.Data.SqlClient;

namespace Diplom.Krasnov__WindowsForms
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            UserNameField.Text = "Введите имя";//Говорим, что работаем с текстом внутри него
            UserNameField.ForeColor = Color.Gray; //Меняем цвет надписи
            UserSurnameField.Text = "Введите фамилию";
            UserSurnameField.ForeColor = Color.Gray;
            LoginField.Text = "Введите логин";
            LoginField.ForeColor = Color.Gray;
            PassField.Text = "Введите пароль";
            PassField.ForeColor = Color.Gray;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit(); //Закрываем приложение
        }


        Point lastPoint; //Специальный класс для задания координат
        private void Register_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //обратились к координате X, сможем определиться, где находится наш курсор
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void Register_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void MainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //обратились к координате X, сможем определиться, где находится наш курсор
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void MainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.MediumVioletRed;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Red;
        }

        private void UserNameField_Enter(object sender, EventArgs e)
        {
            //Вводим проверку, чтобы поле можно было отчистить
            if (UserNameField.Text == "Введите имя")
            {
                UserNameField.Text = ""; //Текст будет пустым, очистим текст
                UserNameField.ForeColor = Color.Black; //текст будет черным, что вводит пользователь
            }
        }

        private void UserNameField_Leave(object sender, EventArgs e) //Когда будем выходить из формочки
        {
            if (UserNameField.Text == "") //Если пользователь ничего не ввел, тогда появится текст введите имя
            {
                UserNameField.Text = "Введите имя"; //Текст будет пустым, очистим текст
                UserNameField.ForeColor = Color.Gray; //Текст будет становиться другого цвета
            }
        }

        private void UserSurnameField_Enter(object sender, EventArgs e)
        {
            if(UserSurnameField.Text == "Введите фамилию")
            {
                UserSurnameField.Text = "";
                UserSurnameField.ForeColor = Color.Black;
            }    
        }

        private void UserSurnameField_Leave(object sender, EventArgs e)
        {
            if (UserSurnameField.Text == "")
            {
                UserSurnameField.Text = "Введите фамилию";
                UserSurnameField.ForeColor = Color.Gray;
            }
        }

        private void LoginField_Enter(object sender, EventArgs e)
        {
            if(LoginField.Text == "Введите логин")
            {
                LoginField.Text = "";
                LoginField.ForeColor = Color.Black;
            }
        }

        private void LoginField_Leave(object sender, EventArgs e)
        {
            if(LoginField.Text == "")
            {
                LoginField.Text = "Введите логин";
                LoginField.ForeColor = Color.Gray;
            }
        }

        private void PassField_Enter(object sender, EventArgs e)
        {
            if(PassField.Text == "Введите пароль")
            {
                PassField.Text = "";
                PassField.ForeColor = Color.Black;
            }
        }

        private void PassField_Leave(object sender, EventArgs e)
        {
            if(PassField.Text == "")
            {
                PassField.Text = "Введите пароль";
                PassField.ForeColor = Color.Gray;
            }
        }

        //ОБРАБОТКА ПОЛЯ РЕГИСТРАЦИИ
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            //Делаем проверку, если пользователь ничего не ввел

            if(PassField.Text == "Введите пароль") //означает, что пользователь ничего не ввел
            {
                MessageBox.Show("Укажите свой пароль");
                return; //выходим из функции
            }

            if (LoginField.Text == "Введите логин")
            {
                MessageBox.Show("Напишите пожалуйста свой логин");
                return;
            }

            //Проверка, если пользователь существует выведется окно с сообщением
            if(isUserExists() == true)
            {
                return;
            }

            //Подключаем msSQL
            DB db = new DB();
            SqlCommand command = new SqlCommand("INSERT INTO users (login, password, name, surname) VALUES (@login, @password, @name, @surname)", db.getConnection()); //Данная команда должна встраивать данные в определенную таблицу
            command.Parameters.Add("@login", SqlDbType.VarChar).Value = LoginField.Text;  //Установим значения, вместо заглушек
            command.Parameters.Add("@password", SqlDbType.VarChar).Value = PassField.Text;
            command.Parameters.Add("@name", SqlDbType.VarChar).Value = UserNameField.Text;
            command.Parameters.Add("@surname", SqlDbType.VarChar).Value = UserSurnameField.Text;

            //Выполняем сам запрос
            
            db.openConnection(); //Для добавления базы данных, необходимо к ней подключиться
            if(command.ExecuteNonQuery() == 1) //Если равно 1, запрос обработался нормально
            {
                MessageBox.Show("Вы зарегистрировались!");            
            }
            else
            {
                MessageBox.Show("Аккаунт не был создан, проверьте данные");
            }
            
            db.closeConnection(); //После добавления, необходимо отключмться от базы данных
        }

        //Проверка, если имеются уже такой логин, выдаем сообщение, что такой пользователь уже есть и не будем его регистрировать
        public Boolean isUserExists() //true - если пользователь уже есть в базе данных, false - если его еще нет
        {
            DB db = new DB(); //для подключения к базе данных

            // Создаем объект на основе клвсса DataTable
            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter();

            //Команда для выборки данных из базы данных
            SqlCommand command = new SqlCommand("SELECT * FROM users WHERE login = @uL", db.getConnection()); //@uL - user Login, @uP - user Password (заглушка, для безопасности)
            command.Parameters.Add("@uL", SqlDbType.VarChar).Value = LoginField.Text; //Меняем заглушки на наши переменные


            //Указываем, какую команду будем выполнять
            adapter.SelectCommand = command;

            //Теперь указываем, что заполняем такой объект как table, при помощи тех данных, которые мы получили
            adapter.Fill(table);
            //Обращаемся к объекту talbe, к его количеству рядов
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже существует! Введите другой.");
                return true;
            }
            else
            {              
                return false;
            }
        }

        private void AutorizationLabel_Click(object sender, EventArgs e)
        {
            this.Hide(); //Прячем это окно
            LoginForm loginForm = new LoginForm(); //Создаем объект, освобождаем под него память
            loginForm.Show(); //открываем это окно
        }

        private void AutorizationLabel_MouseEnter(object sender, EventArgs e)
        {
            AutorizationLabel.ForeColor = Color.Orange; //При наведении на надпись Авторизоваться, меняется цвет
        }

        private void AutorizationLabel_MouseLeave(object sender, EventArgs e)
        {
            AutorizationLabel.ForeColor = Color.White; //При отведении от надписи Авторизоваться - цвет становится белым
        }
    }
}
