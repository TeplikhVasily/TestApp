# TestApp
Test work for work
Все выборки реализованны согласно заданию. Вызов происходит по средствам адресной строки браузера:

1.Выборка части данных api/getrange/itStart/itEnd где itStart - начальный id и itEnd - конечный id.

2.Выборка всего перечня записей api/getworkers . Так же выполняется сохранение 3х самых высокооплачеваемых в файл XML в корне проекта.

3.Выборка рабочего с самой высокой повременной оплатой api/getrich

4.Запрос суммарной месячной платы api/getsum

5.Выполнение процедуры в бд по добавлению записи  api/addworker/Имя/Фамилия/ставка(fix or time)/зарплата.
Для ставки предусмотренны только 2 идентификатора.

6. Выполнение процедуры в бд по поиску api/find/Имя/Фамилия.

7. Переход по адресу localhost:port  выведет всю таблицу, отсортированную согласно заданию.

Расчет производится в классах, согласно заданию.
REST cервис реализован на базе .Net Core приложения Web API. Публикуется на сервере Kestrel.
База данных и скрипт развертывания расположенны в папке DB.

REST API клиент реализовать в полной мере не получилось.