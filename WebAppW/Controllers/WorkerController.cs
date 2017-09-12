using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.IO;

namespace WebAppW.Controllers
{
    
    public class WorkerController : Controller
    {
        protected string connectionString = "Server= .\\SQLEXPRESS;Database=workersdb;User ID= 1 ;Trusted_Connection=True;MultipleActiveResultSets=true";


        //Получение выборки рабочих с укачанием промежутка по idStart/idEnd. Сортировка.
        [HttpGet]
        [Route("api/getrange/{idStart}/{idEnd}")]
        public IEnumerable<Result> GetRange(int idStart, int idEnd)
        {
            //Подключение к БД.
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connectionString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            //Команда выборки.
            sqlCmd.CommandText = "SELECT * FROM Workers WHERE WorkerID BETWEEN " + idStart.ToString() + " AND " + idEnd.ToString();
            sqlCmd.Connection = myConnection;

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            List<Result> result = new List<Result> { };

            while (reader.Read())
            {

                if (String.Compare(reader.GetValue(3).ToString(), "time", true) == 0)
                {
                    //Расчет средней зарплаты у рабочих с повременной ставкой
                    TimePayWorker tp = new TimePayWorker(Convert.ToDecimal(reader.GetValue(4)));
                    decimal f = tp.AveragePay();

                    //Запись в коллекцию.
                    result.Add(new Result(Convert.ToInt32(reader.GetValue(0)),
                               reader.GetValue(1).ToString(),
                               reader.GetValue(2).ToString(),
                               reader.GetValue(3).ToString(),
                               f.ToString()));
                }
                else
                {
                    result.Add(new Result(Convert.ToInt32(reader.GetValue(0)),
                                       reader.GetValue(1).ToString(),
                                       reader.GetValue(2).ToString(),
                                       reader.GetValue(3).ToString(),
                                       Convert.ToDecimal(reader.GetValue(4)).ToString()));

                }

                //Сортировка коллекции по заданному критерию
                result.Sort(delegate (Result result1, Result result2)
                {
                    int l = result2.Sum.CompareTo(result1.Sum);
                    if (l == 0)
                    { l = result1.WorkerName.CompareTo(result2.WorkerName); }
                    return l;
                });
            }

            return result;

        }

        //Получение всего списка рабочих с сортировкой по з/п и имени. 
        //Сохранение 3х самых высокооплачиваемых в XML файл.
        [Route("api/getworkers")]
        public IEnumerable<Result> GetWorkers()
        {

            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connectionString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = "SELECT * FROM Workers";
            sqlCmd.Connection = myConnection;

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            List<Result> result = new List<Result> { };

            while (reader.Read())
            {

                if (String.Compare(reader.GetValue(3).ToString(), "time", true) == 0)
                {
                    //Расчет средней зарплаты у рабочих с повременной ставкой
                    TimePayWorker tp = new TimePayWorker(Convert.ToDecimal(reader.GetValue(4)));
                    decimal f = tp.AveragePay();
                    //Запись в коллекцию.
                    result.Add(new Result(Convert.ToInt32(reader.GetValue(0)),
                               reader.GetValue(1).ToString(),
                               reader.GetValue(2).ToString(),
                               reader.GetValue(3).ToString(),
                               f.ToString()));
                }
                else
                {

                    result.Add(new Result(Convert.ToInt32(reader.GetValue(0)),
                                       reader.GetValue(1).ToString(),
                                       reader.GetValue(2).ToString(),
                                       reader.GetValue(3).ToString(),
                                       Convert.ToDecimal(reader.GetValue(4)).ToString()));

                }

                //Сортировка коллекции по заданному критерию
                result.Sort(delegate (Result result1, Result result2)
                {
                    int l = result2.Sum.CompareTo(result1.Sum);
                    if (l == 0)
                    { l = result1.WorkerName.CompareTo(result2.WorkerName); }
                    return l;
                });

            }
            //Сохранение 3х самых высокооплачиваемых работников к XML файл.
            //Файл разсположен в каталоге программы.        

                    Result[] workers = new Result[] { result[0], result[1], result[2] };
                    XmlSerializer ser = new XmlSerializer(typeof(Result[]));
                    FileStream file = new FileStream("Workers.xml", FileMode.Create, FileAccess.Write, FileShare.None);
                    ser.Serialize(file, workers);
                    file.Close();

            return result;
          
        }

        //Запрос на самого высокооплачиваемого рабочего с повременной ставкой
        [Route("api/getrich")]
        public IEnumerable<Result> GetRich()
        {

            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connectionString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = "SELECT * FROM Workers";
            sqlCmd.Connection = myConnection;

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            List<Result> result = new List<Result> { };

            while (reader.Read())
            {

                if (String.Compare(reader.GetValue(3).ToString(), "time", true) == 0)
                {


                    result.Add(new Result(Convert.ToInt32(reader.GetValue(0)),
                               reader.GetValue(1).ToString(),
                               reader.GetValue(2).ToString(),
                               reader.GetValue(3).ToString(),
                               Convert.ToDecimal(reader.GetValue(4)).ToString()));
                }

                //Сортировка коллекции по заданному критерию
                result.Sort(delegate (Result teacher1, Result teacher2)
                { return teacher2.Sum.CompareTo(teacher1.Sum); });
            }

            yield return result[0];
        }

        //Запрос насуммарную месячную плату по всем сотрудникам
        [Route("api/getsum")]
        public IEnumerable<string> GetSum()
        {

            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connectionString;

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = "SELECT * FROM Workers";
            sqlCmd.Connection = myConnection;

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            decimal sum = 0.00m;

            while (reader.Read())
            {

                if (String.Compare(reader.GetValue(3).ToString(), "time", true) == 0)
                {
                    TimePayWorker tp = new TimePayWorker(Convert.ToDecimal(reader.GetValue(4)));
                    decimal t = tp.AveragePay();

                    sum = sum + t;

                }

                else
                {

                    sum = sum + Convert.ToDecimal(reader.GetValue(4));
                }

            }

            return new string[] { sum.ToString() };

        }

        //Запрос на добавление новой записи, обращением к хранимой процедуре БД.
        [Route("api/addworker/{workerName}/{workerLastName}/{payType}/{sum}")]
        public void AddWorker(string workerName, string workerLastName, string payType, string sum)
        {

            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connectionString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            //Имя хранимой процедуры
            sqlCmd.CommandText = "[AddWorker]";

            sqlCmd.Connection = myConnection;
            //Перечень необходимых параметров
            SqlParameter p1 = sqlCmd.Parameters.Add("@name", SqlDbType.NVarChar);
            SqlParameter p2 = sqlCmd.Parameters.Add("@lastname", SqlDbType.NVarChar);
            SqlParameter p3 = sqlCmd.Parameters.Add("@paytype", SqlDbType.NVarChar);
            SqlParameter p4 = sqlCmd.Parameters.Add("@sum", SqlDbType.Decimal);

            p1.Value = workerName;
            p2.Value = workerLastName;
            p3.Value = payType;
            p4.Value = sum;

            myConnection.Open();
            sqlCmd.ExecuteReader();
            myConnection.Close();
        }

        //Запрос на выбор работника по имени и фамилии, обращением к хранимой процедуре БД.
        [Route("api/find/{workerName}/{workerLastName}")]
        public IEnumerable<Result> FindWorker(string workerName, string workerLastName)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connectionString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            //Имя хранимой процедуры
            sqlCmd.CommandText = "[FindeWorkers]";

            sqlCmd.Connection = myConnection;
            //Перечень необходимых параметров
            SqlParameter p1 = sqlCmd.Parameters.Add("@name", SqlDbType.NVarChar);
            SqlParameter p2 = sqlCmd.Parameters.Add("@lastname", SqlDbType.NVarChar); ;

            p1.Value = workerName;
            p2.Value = workerLastName;

            myConnection.Open();
            reader = sqlCmd.ExecuteReader();

            Result worker = new Result { };

            while (reader.Read())
            {

               worker = new Result(Convert.ToInt32(reader.GetValue(0)),
                               reader.GetValue(1).ToString(),
                               reader.GetValue(2).ToString(),
                               reader.GetValue(3).ToString(),
                               Convert.ToDecimal(reader.GetValue(4)).ToString());
            }

            yield return worker;
        }
    }
}