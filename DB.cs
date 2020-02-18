using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql; //Подключаем Sql
using System.Data.SqlClient;

namespace Diplom.Krasnov__WindowsForms
{
    class DB  //Класс быза данных
    {
        SqlConnection connection = new SqlConnection("Data Source = localhost; Integrated Security = SSPI; Initial Catalog = mylibrary;");  //Прописываем строку, которая будет подключать базу данных
       
       
        //Создаем метод, которая будет открывать работу с базой данных
        public void openConnection()
        {
            
        //Если состояние данных закрыто
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();  //Если закрыто - открываем соединение
            }
             
            
        }
        //Метод закрытия базы данных
        public void closeConnection()
        {
            //Если состояние данных открыто
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();  //Если открыто - закрываем соединение
            }
        }
        //Метод, которая будет возвращать само соединение с базой данных
        public SqlConnection getConnection()
        {
            return connection;
        }

    }
}
