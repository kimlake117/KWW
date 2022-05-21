using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KimsWoodWorking.Models;

namespace KimsWoodWorking.BusinessLogic
{
    public class MonthYearManager
    {
        public static List<MonthModel> fillMonthList()
        {
            List<MonthModel> list = new List<MonthModel>();

            MonthModel Jan = new MonthModel();
            Jan.month_id = 1;
            Jan.month_name = "01 - January";

            list.Add(Jan);

            MonthModel feb = new MonthModel();

            feb.month_id = 2;
            feb.month_name = "02 - February";

            list.Add(feb);

            MonthModel march = new MonthModel();

            march.month_id = 3;
            march.month_name = "03 - March";

            list.Add(march);

            MonthModel april = new MonthModel();

            april.month_id = 4;
            april.month_name = "04 - April";

            list.Add(april);

            MonthModel may = new MonthModel();

            may.month_id = 5;
            may.month_name = "05 - May";

            list.Add(may);

            MonthModel june = new MonthModel();

            june.month_id = 6;
            june.month_name = "06 - June";

            MonthModel july = new MonthModel();

            july.month_id = 7;
            july.month_name = "07 - July";

            list.Add(july);

            MonthModel august = new MonthModel();

            august.month_id = 8;
            august.month_name = "08 - August";

            list.Add(august);

            MonthModel sept = new MonthModel();

            sept.month_id = 9;
            sept.month_name = "09 - September";

            list.Add(sept);

            MonthModel oct = new MonthModel();

            oct.month_id = 10;
            oct.month_name = "10 - October";

            list.Add(oct);

            MonthModel nov = new MonthModel();

            nov.month_id = 11;
            nov.month_name = "11 - November";

            list.Add(nov);

            MonthModel dec = new MonthModel();

            dec.month_id = 12;
            dec.month_name = "12 - December";

            list.Add(dec);

            return list;

        }

        public static List<int> getYearList() { 
            List<int> list = new List<int>();

            int year = DateTime.Now.Year;

            list.Add(year);

            for (int i = 0; i < 6; i++) {
                year++;
                list.Add(year);
            }

            return list;
        }
    }
}