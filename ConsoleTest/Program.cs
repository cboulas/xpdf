using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://www.legifrance.gouv.fr/jorf/id/JORFTEXT000041746694/";

            Xpdf.XPdf.FromURL(url, "D:\\test.pdf");

            while (Xpdf.XPdf.InProgress)
                Thread.Sleep(1);

            //Xpdf.XPdf.FromURL(url, "D:\\test2.pdf");

            //Environment.Exit(-1);
        }
    }
}
