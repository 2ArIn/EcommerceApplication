using System.Globalization;

namespace EcommerceApplication.Utilities.Convertor
{
    public static class ToShamsi
    {
        public static string MiladiToShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" + pc.GetDayOfMonth(value).ToString("00");
        }
    }
}
