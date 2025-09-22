namespace DaysBetweenCounter
{
    class DaysBetweenCounter
    {
        private bool isLeapYear(int year)
        {
            if (year % 4 == 0) return true;
            if (year % 100 == 0) return false;
            return (year % 400 == 0);
        }

        private int daysInMonth(int month, int year)
        {
            switch (month)
            {
                case 2: return isLeapYear(year) ? 29 : 28;
                case 4: case 6: case 9: case 11: return 30;
                default: return 31;
            }
        }

        private int dateToDays(Date date)
        {
            int totalDays = 0;

            for (int year = 1; year < date.Year; year++)
                totalDays += isLeapYear(year) ? 366 : 365;

            for (int month = 1; month < date.Month; month++)
                totalDays += daysInMonth(month, date.Year);

            totalDays += date.Day - 1;
            return totalDays;
        }

        public int daysBetweenDates(Date date1, Date date2)
        {
            int days1 = dateToDays(date1);
            int days2 = dateToDays(date2);

            return days2 - days1;
        }
    }
}
