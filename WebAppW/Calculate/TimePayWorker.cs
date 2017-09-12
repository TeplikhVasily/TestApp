using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppW
{

    //Класс-потомок, расчет средней зарплаты рабочих с повременной ставкой
    class TimePayWorker : AverageSum
    {
        protected decimal n = 20.80m;
        protected decimal d = 8.00m;

        public TimePayWorker(decimal cash):base(cash)
        { }


        public override decimal AveragePay()
        {
            return n  * d * cash;
        }

    }
}
