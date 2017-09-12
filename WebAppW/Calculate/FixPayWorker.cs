using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppW
{

    //Класс-потомок, расчет срердней зарплаты рабочих с фиксированной ставкой
    class FixPayWorker : AverageSum
    {
        

        public FixPayWorker(decimal cash):base(cash)
        { }

        public override decimal AveragePay()
        {
            return cash;
        }
    }
}
