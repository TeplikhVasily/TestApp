using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppW
{
    //Клас-родитель, расчет средней зарплаты
    abstract class AverageSum
    {

        protected decimal cash = 0.00m;
       

        public AverageSum(decimal cash)
        {
            this.cash = cash;

        }

        abstract public decimal AveragePay();

    }
}
