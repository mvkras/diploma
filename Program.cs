using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;  //Подключили SQL
using System.Data.SqlClient;

namespace Diplom.Krasnov__WindowsForms
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new LoginForm()); // запуск Окна авторизации
            Application.Run(new RegisterForm());  //Запуск окна регистрации
        }
    }
}
