using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql; //Подключили библиотеку для SqlDataAdapter
using System.Data.SqlClient;

namespace Diplom.Krasnov__WindowsForms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.PassField.AutoSize = false; //Отключили авторазмер
            this.PassField.Size = new Size(this.PassField.Size.Width, 64); //Установили ширину и высоту поля пароль (64 взяли с поля user)
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit(); //метод закрытия приложения авторизации
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.MediumVioletRed;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Red;
        }
        Point lastPoint; //Специальный класс для задания координат

        private void Logo_MouseMove(object sender, MouseEventArgs e)
        {
            //Делаем проверку, если нажали на кнопку - двигаем наше окно (Для верхней части, где авторизация)
            if (e.Button == MouseButtons.Left)
            {
                //обратились к координате X, сможем определиться, где находится наш курсор
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void Logo_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
        private void MainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            //Делаем проверку, если нажали на кнопку - двигаем наше окно
            if (e.Button == MouseButtons.Left)
            {
                //обратились к координате X, сможем определиться, где находится наш курсор
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void MainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            //Устанавливаем новые координаты
            lastPoint = new Point(e.X, e.Y);
        }

        // Функция Обработки кнопки "Войти", будет срабатывать каждый раз, когда мы будем нажимать на кнопку
        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            /*Эта функция, при нажатии на кнопку должна брать данные, которые вводит пользователь их обрабатывать и проверять с базой данных в MySQL и вытаскивать их оттуда 
             Если пользователь найдет - авторизация успешна*/

            String LoginUser = LoginField.Text;
            String PasswordUser = PassField.Text;
           
        
            //Теперь можем сверить те данные, что получили от пользователя
            //Создадим объект, который будет идти от класса DB (DataBase)
            DB db = new DB(); //для подключения к базе данных

            // Создаем объект на основе клвсса DataTable
            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter();

            //Команда для выборки данных из базы данных
            SqlCommand command = new SqlCommand("SELECT * FROM users WHERE login = @uL AND password = @uP", db.getConnection()); //@uL - user Login, @uP - user Password (заглушка, для безопасности)
            command.Parameters.Add("@uL", SqlDbType.VarChar).Value = LoginUser; //Меняем заглушки на наши переменные
            command.Parameters.Add("@uP", SqlDbType.VarChar).Value = PasswordUser; //тоже самое для пароля

            //Указываем, какую команду будем выполнять
            adapter.SelectCommand = command;

            //Теперь указываем, что заполняем такой объект как table, при помощи тех данных, которые мы получили
            adapter.Fill(table);
            //Обращаемся к объекту talbe, к его количеству рядов
            if(table.Rows.Count > 0)
            {
                this.Hide(); //Скрываем окно авторизации
                MainForm mainForm = new MainForm(); //создаем оюъект главная форма, выделяем память под нее
                mainForm.Show(); //Показываем это окно Главное окно
                
            }
            else
            {
                MessageBox.Show("Не удалось авторизоваться, проверьте правильно ли написаны логин и пароль.");
            }
        }

        private void RegisterLabel_Click(object sender, EventArgs e)
        {
            this.Hide(); //Прячем окно, при нажатии на кнопку зарегистрироваться в панели Авторизация
            RegisterForm registerForm = new RegisterForm(); //Создали объект и выделели под него память
            registerForm.Show(); //Открываем это окно

        }

        private void RegisterLabel_MouseEnter(object sender, EventArgs e)
        {
            RegisterLabel.ForeColor = Color.Orange; //При наведении на поле Зарегистрироваться, она меняет цвет
        }

        private void RegisterLabel_MouseLeave(object sender, EventArgs e)
        {
            RegisterLabel.ForeColor = Color.White; //При отведени мыши от надписи зарегистрироваться 
        }
    }
}
