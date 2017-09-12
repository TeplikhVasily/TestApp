using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppW
{
    [Serializable]
    public class Result
    {
        public int WorkerID { get; set; }
        public string WorkerName { get; set; }
        public string WorkerLastName { get; set; }
        public string PayType { get; set; }
        public string Sum { get; set; }

        public Result(int workerID, string workerName, 
            string workerLastName, string payType, string sum)
        {
            WorkerID = workerID;
            WorkerName = workerName;
            WorkerLastName = workerLastName;
            PayType = payType;
            Sum = sum;
        }
        public Result()
        { }

    }
}
